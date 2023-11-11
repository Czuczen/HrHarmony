using System.Linq.Expressions;
using System.Reflection;
using HrHarmony.Attributes;
using LinqKit;

namespace HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;

[RegisterOpenGenericClassInDi(typeof(EnumValueFilterStrategy<>))]
public class EnumValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
{
    public IEnumerable<Type> Types => new List<Type> { typeof(Enum) };

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");
        var propertyExpression = Expression.Property(param, property.Name);

        if (property.PropertyType.IsEnum)
        {
            var enumType = property.PropertyType;
            var parseMethod = enumType.GetMethod("Parse", new[] { typeof(string) });
            if (parseMethod != null)
            {
                var enumValue = Expression.Call(parseMethod, Expression.Constant(value));
                var enumEqualExpression = Expression.Equal(propertyExpression, enumValue);
                filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(enumEqualExpression, param));
            }
        }

        return filters;
    }
}