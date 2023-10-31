namespace HrHarmony.Configuration.Dependencies;

public static class TestsCustomDependencies
{
    public static readonly IEnumerable<Type> TransientClasses = new List<Type>
    {
        //typeof(HttpClient)
    };

    public static readonly IEnumerable<Type> PerWebRequestClasses = new List<Type>
    {
        //typeof(),
    };

    public static readonly IEnumerable<Type> SingletonClasses = new List<Type>
    {
        //typeof(),
    };
}