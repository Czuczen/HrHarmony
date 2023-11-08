using System.Linq.Expressions;
using System.Reflection;
using HrHarmony.Attributes;
using LinqKit;

namespace HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;

[RegisterOpenGenericClassInDI(typeof(CharValueFilterStrategy<>))]
public class CharValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
{
    public IEnumerable<Type> Types => new List<Type> { typeof(char), typeof(char?) };

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");
        var propertyExpression = Expression.Property(param, property.Name);

        if (value.Length == 1)
        {
            var charExpression = Expression.Constant(value[0], property.PropertyType);
            var charEqualExpression = Expression.Equal(propertyExpression, charExpression);
            filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(charEqualExpression, param));
        }

        return filters;
    }
}