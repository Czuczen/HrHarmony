using System.Linq.Expressions;

namespace HrHarmony.Data.Repositories.AccessLimiters.Selectable;

public interface ISelectable<TEntity, TSelect>
{
    IQueryable<TSelect> Select(Expression<Func<TEntity, TSelect>> selector);
}