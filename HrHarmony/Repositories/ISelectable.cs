using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using System.Linq.Expressions;

namespace HrHarmony.Repositories
{
    public interface ISelectable<TEntity, TSelect>
    {
        IQueryable<TSelect> Select(Expression<Func<TEntity, TSelect>> selector);
    }
}