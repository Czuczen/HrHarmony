using HrHarmony.Attributes;
using HrHarmony.Configuration.Database;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Configuration.Logging;
using HrHarmony.Configuration.Mapper;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;
using HrHarmony.Repositories.EntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

namespace HrHarmony.Configuration.Dependencies;

public static class DependencyInjection
{
    private static ServiceProvider ServiceProvider;

    private static readonly Type TransientType = typeof(ITransientDependency);
    private static readonly Type ScopedType = typeof(IPerWebRequestDependency);
    private static readonly Type SingletonType = typeof(ISingletonDependency);

    private static ILogger Logger
    {
        get
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json") // Plik główny
               .AddJsonFile($"appsettings.Development.json", optional: true) // Plik środowiskowy
               .Build();

            var fileLoggingConfig = configuration.GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();
            var dfg = new FileLoggerProvider(fileLoggingConfig);
            var logger = dfg.CreateLogger("");
            return logger;
        }
    }

    public static void RegisterDependenciesByConvention(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        ServiceProvider = services.BuildServiceProvider();

        Register(services, assembly, TransientType);
        Register(services, assembly, ScopedType);
        Register(services, assembly, SingletonType);

        services.AddSingleton(MapperConfigurationFactory.Configure().CreateMapper());

        foreach (var type in TestsCustomDependencies.ScopedGenericClasses)
            services.AddSingleton(type.Key, type.Value);
    
        LogRegisteredServicesByConvention(services);
    }

    public static void RegisterTestsDependencies(this IServiceCollection services)
    {
        services.AddLogging(); // w testach logger nie loguje działa tylko dla poprawnego działania iniekcji zależności

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json") // Plik główny
                .AddJsonFile($"appsettings.Development.json", optional: true) // Plik środowiskowy
                .Build();

            var testConnectionString = configuration.GetConnectionString("TestConnection");
            options.UseSqlServer(testConnectionString);
        });

        foreach (var item in TestsCustomDependencies.TransientClasses)
            services.AddTransient(item);

        foreach (var item in TestsCustomDependencies.PerWebRequestClasses)
            services.AddScoped(item);

        foreach (var item in TestsCustomDependencies.SingletonClasses)
            services.AddSingleton(item);

        services.RegisterDependenciesByConvention();
    }

    private static void Register(IServiceCollection services, Assembly assembly, Type dependencyType)
    {
        var types = assembly.GetTypes()
            .Where(type =>
                !type.IsInterface &&
                !type.IsAbstract &&
                ImplementsDependency(type, dependencyType));

        foreach (var type in types)
            RegisterImplementations(services, type, dependencyType);
    }

    private static bool ImplementsDependency(Type type, Type dependencyType) =>
        GetInterfacesInHierarchy(type).Any(i => i == dependencyType);

    private static IEnumerable<Type> GetInterfacesInHierarchy(Type type)
    {
        var interfaces = type.GetInterfaces().ToList();
        foreach (var baseInterface in type.GetInterfaces())
            interfaces.AddRange(GetInterfacesInHierarchy(baseInterface));

        return interfaces;
    }

    private static void RegisterImplementations(IServiceCollection services, Type type, Type dependencyType)
    {
        var currType = GetOpenGenericClassTypeIfExist(type);
        var directlyImplementedInterfaces = currType.GetInterfaces()
            .Where(i => i != dependencyType && ImplementsDependency(i, dependencyType));

        foreach (var interfaceType in directlyImplementedInterfaces)
        {
            var currInterfaceType = GetOpenGenericInterfaceTypeIfExist(interfaceType);
            var provider = services.BuildServiceProvider();

            if (dependencyType == TransientType)
                services.AddTransient(currInterfaceType, currType);

            if (dependencyType == ScopedType)
            {
                var implementation = provider.GetService(type);
                if (implementation != null)
                    services.AddScoped(currInterfaceType, _ => implementation);
                else
                    services.AddScoped(currInterfaceType, currType);
            }

            if (dependencyType == SingletonType)
            {
                //var dgfdg = services.Single(item => item.ImplementationType == currType);

                //var instance = dgfdg.ImplementationFactory.Invoke(provider);

                var implementation = provider.GetService(type);
                if (implementation != null)
                    services.AddSingleton(currInterfaceType, _ => implementation);
                else
                    services.AddSingleton(currInterfaceType, currType);
            }
        }
    }

    private static Type GetOpenGenericClassTypeIfExist(Type type)
    {
        var genericClassRegistrationAttribute = typeof(RegisterOpenGenericClassInDIAttribute);
        if (type.IsDefined(genericClassRegistrationAttribute, false))
        {
            var attribute = (RegisterOpenGenericClassInDIAttribute)type.GetCustomAttributes(genericClassRegistrationAttribute, false).First();
            return attribute.ImplementationType;
        }
        else
            return type;
    }

    private static Type GetOpenGenericInterfaceTypeIfExist(Type type)
    {
        var genericInterfaceRegistrationAttribute = typeof(RegisterOpenGenericInterfaceInDIAttribute);
        if (type.IsDefined(genericInterfaceRegistrationAttribute, false))
        {
            var attribute = (RegisterOpenGenericInterfaceInDIAttribute) type.GetCustomAttributes(genericInterfaceRegistrationAttribute, false).First();
            return attribute.InterfaceType;
        }
        else
            return type;
    }

    private static void LogRegisteredServicesByConvention(IServiceCollection services)
    {
        var provid = services.BuildServiceProvider();
        var logger = ServiceProvider.GetRequiredService<ILogger<Program>>();
        var hrHarmonyServices = services.Where(item => item.ServiceType.FullName.StartsWith("HrHarmony"));

        var employeeRepo = provid.GetRequiredService<IRepository<Employee, int>>();
        var employeeRepo2 = provid.GetRequiredService<IRepository<Employee, int>>();

        var absenceRepo = provid.GetRequiredService<IRepository<Absence, int>>();
        var absenceRepoTest = provid.GetRequiredService<ITestedDependency<Absence, int>>();

        var employmentContractRepoTest = provid.GetRequiredService<ITestedDependency<EmploymentContract, int>>();
        var employmentContractRepoTest2 = provid.GetRequiredService<ITestedDependency<EmploymentContract, int>>();
        var employmentContractRepo = provid.GetRequiredService<IRepository<EmploymentContract, int>>();



        logger.LogDebug("employeeRepo guid               - " + employeeRepo.InstanceGuid);
        logger.LogDebug("employeeRepo2 guid              - " + employeeRepo2.InstanceGuid);

        logger.LogDebug("--");

        logger.LogDebug("absenceRepo guid                - " + absenceRepo.InstanceGuid);
        logger.LogDebug("absenceRepoTest guid            - " + absenceRepoTest.InstanceGuid);

        logger.LogDebug("--");

        logger.LogDebug("employmentContractRepoTest guid  - " + employmentContractRepoTest.InstanceGuid);
        logger.LogDebug("employmentContractRepoTest2 guid - " + employmentContractRepoTest2.InstanceGuid);
        logger.LogDebug("employmentContractRepo guid      - " + employmentContractRepo.InstanceGuid);

        logger.LogDebug("--");


        var sdfsdf = new List<string>();
        foreach (var service in services)
        {
            var associatedWithInjectionConvention = ImplementsDependency(service.ServiceType, TransientType)
                                                    || ImplementsDependency(service.ServiceType, ScopedType)
                                                    || ImplementsDependency(service.ServiceType, SingletonType);

            if (associatedWithInjectionConvention)
            {
                Console.WriteLine($"Zarejestrowano usługę w kontenerze DI: {service.Lifetime} => {service.ServiceType} => {service.ImplementationType}");
                sdfsdf.Add($"{service.Lifetime} => {service.ServiceType} => {service.ImplementationType}");
                Logger.LogDebug($"Zarejestrowano usługę w kontenerze DI: {service.Lifetime} => {service.ServiceType} => {service.ImplementationType}");
            }
                
        }
    }
}