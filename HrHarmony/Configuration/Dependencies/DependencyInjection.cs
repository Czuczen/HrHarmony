using HrHarmony.Configuration.Database;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Configuration.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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

        services.PrintRegisteredServicesByConvention();
    }

    public static void RegisterTestsDependencies(this IServiceCollection services)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json")
                               .Build();

            var testConnectionString = configuration.GetConnectionString("TestConnection");
            options.UseSqlServer(testConnectionString);
        });

        services.RegisterDependenciesByConvention();

        foreach (var item in CustomDependencies.TransientClasses)
            services.AddTransient(item);

        foreach (var item in CustomDependencies.PerWebRequestClasses)
            services.AddScoped(item);

        foreach (var item in CustomDependencies.SingletonClasses)
            services.AddSingleton(item);

        /// TODO: wrócić do tego jeszcze. Trzeba znaleźć sposób na działanie logera w iniekcji zależności dla testów.
        //ConfigureLogging(services); 
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
        {
            interfaces.AddRange(GetInterfacesInHierarchy(baseInterface));
        }

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

    public static void PrintRegisteredServicesByConvention(this IServiceCollection services)
    {
        Debug.Print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        Debug.Print("Zarejestrowane usługi w kontenerze DI:");
        Debug.Print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        foreach (var service in services)
        {
            var associatedWithInjectionConvention = ImplementsDependency(service.ServiceType, typeof(ITransientDependency))
                                                    || ImplementsDependency(service.ServiceType, typeof(IPerWebRequestDependency))
                                                    || ImplementsDependency(service.ServiceType, typeof(ISingletonDependency));

            if (associatedWithInjectionConvention)
                Debug.Print($"{service.Lifetime} => {service.ServiceType} => {service.ImplementationType}");
        }

        Debug.Print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        Debug.Print("!!!!!!!!!!!!!! KONIEC !!!!!!!!!!!!!!!!!");
        Debug.Print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}