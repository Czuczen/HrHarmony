using HrHarmony.Attributes;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;

[RegisterOpenGenericClassInDi(typeof(LongValueFilterStrategy<>))]
public class LongValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
{
    public IEnumerable<Type> Types => new List<Type> { typeof(long), typeof(long?) };

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");
        var propertyExpression = Expression.Property(param, property.Name);

        if (long.TryParse(value, out var longValue))
        {
            var longValueExpression = Expression.Constant(longValue, typeof(long));
            var compareExpression = Expression.Equal(propertyExpression, longValueExpression);
            filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(compareExpression, param));
        }

        return filters;
    }
}
