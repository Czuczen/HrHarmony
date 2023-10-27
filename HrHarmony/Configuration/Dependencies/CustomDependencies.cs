using HrHarmony.Repositories;
using HrHarmony.Repositories.EntityRepository;

namespace HrHarmony.Configuration.Dependencies;

public static class CustomDependencies
{
    public static readonly IEnumerable<Type> TransientClasses = new List<Type>
    {
        typeof(HttpClient)
    };

    public static readonly IEnumerable<Type> PerWebRequestClasses = new List<Type>
    {
        //typeof(),
    };

    public static readonly IEnumerable<Type> SingletonClasses = new List<Type>
    {
        //typeof(),
    };

    public static readonly Dictionary<Type, Type> ScopedGenericClasses = new()
    {
        [typeof(IRepository<,,,,>)] = typeof(Repository<,,,,>),
        [typeof(IRepository<,>)] = typeof(Repository<,>)
    };
}