using System.Linq.Expressions;

namespace HrHarmony.Repositories.Selectable
{
    public interface ISelectable<TEntity, TSelect>
    {
        IQueryable<TSelect> Select(Expression<Func<TEntity, TSelect>> selector);
    }
}