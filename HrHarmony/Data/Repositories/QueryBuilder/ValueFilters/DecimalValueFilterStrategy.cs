using HrHarmony.Attributes;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;

[RegisterOpenGenericClassInDi(typeof(DecimalValueFilterStrategy<>))]
public class DecimalValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
{
    public IEnumerable<Type> Types => new List<Type> { typeof(decimal), typeof(decimal?) };

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");
        var propertyExpression = Expression.Property(param, property.Name);

        if (decimal.TryParse(value, out var decimalValue))
        {
            var intValueExpression = Expression.Convert(propertyExpression, typeof(int));
            var decimalValueExpression = Expression.Constant(decimalValue, typeof(decimal));

            var compareExpression = Expression.Or(
                Expression.Equal(propertyExpression, decimalValueExpression),
                Expression.Equal(intValueExpression, Expression.Constant((int)decimalValue)));

            filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(compareExpression, param));
        }

        return filters;
    }
}