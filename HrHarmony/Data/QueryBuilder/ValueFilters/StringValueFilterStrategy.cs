using System.Linq.Expressions;
using System.Reflection;
using HrHarmony.Attributes;
using LinqKit;

namespace HrHarmony.Data.QueryBuilder.ValueFilters;

[RegisterOpenGenericClassInDI(typeof(StringValueFilterStrategy<>))]
public class StringValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
{
    public IEnumerable<Type> Types => new List<Type> { typeof(string) };

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");
        var propertyExpression = Expression.Property(param, property.Name);

        var isNullableString = Nullable.GetUnderlyingType(property.PropertyType) == typeof(string);

        var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
        var toLowerExpression = Expression.Call(propertyExpression, toLowerMethod);

        var searchValueExpression = Expression.Constant(value.ToLower(), typeof(string));

        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        var containsExpression = Expression.Call(toLowerExpression, containsMethod, searchValueExpression);

        if (isNullableString)
        {
            var isNullExpression = Expression.Equal(propertyExpression, Expression.Constant(null, typeof(string)));
            filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(Expression.OrElse(isNullExpression, containsExpression), param));
        }
        else
        {
            filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(containsExpression, param));
        }

        return filters;
    }
}