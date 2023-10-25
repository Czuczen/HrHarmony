using HrHarmony.Repositories;

namespace HrHarmony.Configuration.Dependencies;

public static class CustomDependencies
{
    public static IEnumerable<Type> TransientClasses = new List<Type>
    {
        typeof(HttpClient)
    };

    public static IEnumerable<Type> PerWebRequestClasses = new List<Type>
    {
        //typeof(),
    };

    public static IEnumerable<Type> SingletonClasses = new List<Type>
    {
        //typeof(),
    };

    public static Dictionary<Type, Type> ScopedGenericClasses = new Dictionary<Type, Type>
    {
        [typeof(IRepository<,,,,>)] = typeof(Repository<,,,,>)
    };
}