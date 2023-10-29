using HrHarmony.Configuration.Database;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Configuration.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HrHarmony.Configuration.Dependencies;

public static class DependencyInjection
{
    public static void RegisterDependenciesByConvention(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        Register(services, assembly, typeof(ITransientDependency));
        Register(services, assembly, typeof(IPerWebRequestDependency));
        Register(services, assembly, typeof(ISingletonDependency));

        foreach (var item in CustomDependencies.ScopedGenericClasses)
            services.AddScoped(item.Key, item.Value);

        services.AddSingleton(MapperConfigurationFactory.Configure().CreateMapper());

        LogRegisteredServicesByConvention(services);
    }

    public static void RegisterTestsDependencies(this IServiceCollection services)
    {
        services.AddLogging(); // w testach logger nie loguje

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

        foreach (var item in CustomDependencies.TransientClasses)
            services.AddTransient(item);

        foreach (var item in CustomDependencies.PerWebRequestClasses)
            services.AddScoped(item);

        foreach (var item in CustomDependencies.SingletonClasses)
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
        var directlyImplementedInterfaces = type.GetInterfaces()
            .Where(i => i != dependencyType && ImplementsDependency(i, dependencyType));

        if (dependencyType == typeof(IPerWebRequestDependency))
            services.AddScoped(type);

        if (dependencyType == typeof(ISingletonDependency))
            services.AddSingleton(type);

        foreach (var interfaceType in directlyImplementedInterfaces)
        {
            if (dependencyType == typeof(ITransientDependency))
                services.AddTransient(interfaceType, type);

            if (dependencyType == typeof(IPerWebRequestDependency))
                services.AddScoped(interfaceType, serviceProvider => serviceProvider.GetRequiredService(type));

            if (dependencyType == typeof(ISingletonDependency))
                services.AddSingleton(interfaceType, serviceProvider => serviceProvider.GetRequiredService(type));
        }
    }

    private static void LogRegisteredServicesByConvention(IServiceCollection services)
    {
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

        foreach (var service in services)
        {
            var associatedWithInjectionConvention = ImplementsDependency(service.ServiceType, typeof(ITransientDependency))
                                                    || ImplementsDependency(service.ServiceType, typeof(IPerWebRequestDependency))
                                                    || ImplementsDependency(service.ServiceType, typeof(ISingletonDependency));

            if (associatedWithInjectionConvention)
                logger.LogDebug($"Zarejestrowano usługę w kontenerze DI: {service.Lifetime} => {service.ServiceType} => {service.ImplementationType}");
        }
    }
}