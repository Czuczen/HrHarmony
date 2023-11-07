using System.Linq.Expressions;

namespace HrHarmony.Data.AccessLimiters.Selectable;

public class Selectable<TEntity, TSelect> : ISelectable<TEntity, TSelect>
{
    private readonly IQueryable<TEntity> _source;

    public Selectable(IQueryable<TEntity> source)
    {
        _source = source;
    }

    public IQueryable<TSelect> Select(Expression<Func<TEntity, TSelect>> selector)
    {
        return _source.Select(selector);
    }
}
