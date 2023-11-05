using HrHarmony.Attributes;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Repositories.Filters
{
    [RegisterOpenGenericClassInDI(typeof(CharFilterStrategy<>))]
    public class CharFilterStrategy<TEntity> : IFilterStrategy<TEntity>
    {
        public IEnumerable<Type> Types => new List<Type> { typeof(char), typeof(char?) };

        public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string searchString)
        {
            var param = Expression.Parameter(typeof(TEntity), "e");
            var propertyExpression = Expression.Property(param, property.Name);

            if (searchString.Length == 1)
            {
                var charExpression = Expression.Constant(searchString[0], property.PropertyType);
                var charEqualExpression = Expression.Equal(propertyExpression, charExpression);
                filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(charEqualExpression, param));
            }
            
            return filters;
        }
    }
}
