using HrHarmony.Attributes;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Repositories.QueryBuilder.Filters
{
    [RegisterOpenGenericClassInDI(typeof(BoolValueFilterStrategy<>))]
    public class BoolValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
    {
        public IEnumerable<Type> Types => new List<Type> { typeof(bool), typeof(bool?) };

        public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
        {
            var param = Expression.Parameter(typeof(TEntity), "e");
            var propertyExpression = Expression.Property(param, property.Name);

            if (bool.TryParse(value, out var searchBool))
            {
                var boolExpression = Expression.Constant(searchBool, property.PropertyType);
                var boolEqualExpression = Expression.Equal(propertyExpression, boolExpression);
                filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(boolEqualExpression, param));
            }

            return filters;
        }
    }
}
