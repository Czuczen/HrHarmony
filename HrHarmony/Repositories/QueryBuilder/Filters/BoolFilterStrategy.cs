using HrHarmony.Attributes;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Repositories.QueryBuilder.Filters
{
    [RegisterOpenGenericClassInDI(typeof(BoolFilterStrategy<>))]
    public class BoolFilterStrategy<TEntity> : IFilterStrategy<TEntity>
    {
        public IEnumerable<Type> Types => new List<Type> { typeof(bool), typeof(bool?) };

        public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string searchString)
        {
            var param = Expression.Parameter(typeof(TEntity), "e");
            var propertyExpression = Expression.Property(param, property.Name);

            if (bool.TryParse(searchString, out var searchBool))
            {
                var boolExpression = Expression.Constant(searchBool, property.PropertyType);
                var boolEqualExpression = Expression.Equal(propertyExpression, boolExpression);
                filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(boolEqualExpression, param));
            }

            return filters;
        }
    }
}
