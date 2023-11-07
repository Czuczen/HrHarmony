using HrHarmony.Attributes;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Configuration.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using HrHarmony.Data.Database;
using HrHarmony.Exceptions;
using HrHarmony.Logging;

namespace HrHarmony.Configuration.Dependencies;

public static class DependencyInjection
{
    private static readonly Type _transientType = typeof(ITransientDependency);
    private static readonly Type _scopedType = typeof(IPerWebRequestDependency);
    private static readonly Type _singletonType = typeof(ISingletonDependency);

    public static void RegisterDependenciesByConvention(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        Register(services, assembly, _transientType);
        Register(services, assembly, _scopedType);
        Register(services, assembly, _singletonType);

        services.AddSingleton(MapperConfigurationFactory.Configure().CreateMapper());

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

    private static bool ImplementsDependency(Type type, Type dependencyType) => type.GetInterfaces().Any(i => i == dependencyType);

    private static void RegisterImplementations(IServiceCollection services, Type type, Type dependencyType)
    {
        var currType = GetOpenGenericClassTypeIfExist(type, out var isSameType);
        var allInterfacesInHierarchy = currType.GetInterfaces();

        var classInterfaces = allInterfacesInHierarchy.Where(interfaceType => !allInterfacesInHierarchy.Any(parentInterfaceType => parentInterfaceType != interfaceType && interfaceType.IsAssignableFrom(parentInterfaceType))).ToList();
        var classInterfacesWithRegistration = allInterfacesInHierarchy.Where(i => i != dependencyType && classInterfaces.Any(item => item == i) && ImplementsDependency(i, dependencyType)).ToList();
        var registerInterfaceIsOnClass = classInterfaces.FirstOrDefault(i => i == dependencyType) != null;

        // jeśli jest typem generycznym i nie jest TransientType to sprawdź ilość interfejsów dziedziczących od interfejsów IPerWebRequestDependency, ISingletonDependency
        // jeśli typ generyczny nie jest TransientType to nie ma możliwości rejestrowania wielu interfejsów bo np. singleton będzie osobnym obiektem dla każdego interfejsu
        if (!isSameType && dependencyType != _transientType && classInterfacesWithRegistration.Count > 1)
            throw new MultipleInterfacesForGenericTypeException($"Type {currType.FullName} has multiple registering interfaces. This is not allowed for IPerWebRequestDependency and ISingletonDependency.");

        // jeśli interfejs rejestracyjny jest bezpośrednio na klasie to dodajemy samą klasę
        if (registerInterfaceIsOnClass)
        {
            if (dependencyType == _transientType)
                services.AddTransient(currType);

            if (dependencyType == _scopedType)
                services.AddScoped(currType);

            if (dependencyType == _singletonType)
                services.AddSingleton(currType);
        }
        else
        {
            // jeśli typ nie jest generyczny dodajemy jego implementację klasy
            if (dependencyType == _scopedType && isSameType)
                services.AddScoped(currType);

            if (dependencyType == _singletonType && isSameType)
                services.AddSingleton(currType);
        }
        
        foreach (var interfaceType in classInterfacesWithRegistration)
        {
            var currInterfaceType = GetOpenGenericInterfaceTypeIfExist(interfaceType);
            
            if (dependencyType == _transientType)
                services.AddTransient(currInterfaceType, currType);

            // jeśli typ nie jest generyczny dodajemy interfejs do wcześniej dodanej implementacji klasy
            if (dependencyType == _scopedType)
                if (isSameType)
                    services.AddScoped(currInterfaceType, provider => provider.GetRequiredService(currType));
                else
                    services.AddScoped(currInterfaceType, currType);

            if (dependencyType == _singletonType)
                if (isSameType)
                    services.AddSingleton(currInterfaceType, provider => provider.GetRequiredService(currType));
                else
                    services.AddSingleton(currInterfaceType, currType);
        }
    }

    private static Type GetOpenGenericClassTypeIfExist(Type type, out bool isSameType)
    {
        var genericClassRegistrationAttribute = typeof(RegisterOpenGenericClassInDIAttribute);
        if (type.IsDefined(genericClassRegistrationAttribute, false))
        {
            var attribute = (RegisterOpenGenericClassInDIAttribute)type.GetCustomAttributes(genericClassRegistrationAttribute, false).First();
            isSameType = false;
            return attribute.ImplementationType;
        }
        else
        {
            isSameType = true;
            return type;
        }
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
        var fileLoggingConfig = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json") // Plik główny
              .AddJsonFile($"appsettings.Development.json", optional: true) // Plik środowiskowy
              .Build().GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();

        var logger = new FileLoggerProvider(fileLoggingConfig).CreateLogger("");
        
        foreach (var service in services)
        {
            var associatedWithInjectionConvention = ImplementsDependency(service.ServiceType, _transientType)
                                                    || ImplementsDependency(service.ServiceType, _scopedType)
                                                    || ImplementsDependency(service.ServiceType, _singletonType);

            if (associatedWithInjectionConvention)
                logger.LogDebug($"Zarejestrowano usługę w kontenerze DI: {service.Lifetime} => {service.ServiceType} => {service.ImplementationType}");
        }
    }
}