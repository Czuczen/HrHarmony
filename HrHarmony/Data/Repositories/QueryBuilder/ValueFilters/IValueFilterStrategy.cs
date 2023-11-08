using System.Reflection;
using HrHarmony.Attributes;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using LinqKit;

namespace HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;

[RegisterOpenGenericInterfaceInDI(typeof(IValueFilterStrategy<>))]
public interface IValueFilterStrategy<TEntity> : ITransientDependency
{
    public IEnumerable<Type> Types { get; }

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value);
}