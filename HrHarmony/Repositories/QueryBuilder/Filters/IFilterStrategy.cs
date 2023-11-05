using HrHarmony.Attributes;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using LinqKit;
using System.Reflection;

namespace HrHarmony.Repositories.QueryBuilder.Filters
{
    [RegisterOpenGenericInterfaceInDI(typeof(IFilterStrategy<>))]
    public interface IFilterStrategy<TEntity> : ITransientDependency
    {
        public IEnumerable<Type> Types { get; }

        public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string searchString);
    }
}
