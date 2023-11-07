using System.Linq.Expressions;
using System.Reflection;
using HrHarmony.Attributes;
using LinqKit;

namespace HrHarmony.Data.QueryBuilder.ValueFilters;

[RegisterOpenGenericClassInDI(typeof(NumericValueFilterStrategy<>))]
public class NumericValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
{
    public IEnumerable<Type> Types => new List<Type>
    {
        typeof(byte),   typeof(sbyte),      typeof(byte?),      typeof(sbyte?),
        typeof(short),  typeof(ushort),     typeof(short?),     typeof(ushort?),
        typeof(int),    typeof(uint),       typeof(int?),       typeof(uint?),
        typeof(long),   typeof(ulong),      typeof(long?),      typeof(ulong?),
        typeof(float),  typeof(double),     typeof(float?),     typeof(double?),
        typeof(decimal),                    typeof(decimal?),
        typeof(Guid),                       typeof(Guid?)
    };

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");
        var propertyExpression = Expression.Property(param, property.Name);

        var parseMethod = property.PropertyType.GetMethod("Parse", new[] { typeof(string) });
        if (parseMethod != null)
        {
            var parseExpression = Expression.Call(parseMethod, Expression.Call(propertyExpression, typeof(object).GetMethod("ToString")));
            var searchValueExpression = Expression.Constant(Convert.ChangeType(value, property.PropertyType), property.PropertyType);
            var compareExpression = Expression.Equal(parseExpression, searchValueExpression);
            filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(compareExpression, param));
        }

        return filters;
    }
}