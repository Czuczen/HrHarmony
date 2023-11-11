using HrHarmony.Attributes;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;

[RegisterOpenGenericClassInDi(typeof(IntValueFilterStrategy<>))]
public class IntValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
{
    public IEnumerable<Type> Types => new List<Type> { typeof(int), typeof(int?) };

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");
        var propertyExpression = Expression.Property(param, property.Name);

        if (int.TryParse(value, out var intValue))
        {
            var intValueExpression = Expression.Constant(intValue, typeof(int));
            var compareExpression = Expression.Equal(propertyExpression, intValueExpression);
            filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(compareExpression, param));
        }

        return filters;
    }
}