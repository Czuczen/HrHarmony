using HrHarmony.Attributes;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Repositories.Filters
{
    [RegisterOpenGenericClassInDI(typeof(EnumFilterStrategy<>))]
    public class EnumFilterStrategy<TEntity> : IFilterStrategy<TEntity>
    {
        public IEnumerable<Type> Types => new List<Type> { typeof(Enum) };

        public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string searchString)
        {
            var param = Expression.Parameter(typeof(TEntity), "e");
            var propertyExpression = Expression.Property(param, property.Name);

            if (property.PropertyType.IsEnum)
            {
                var enumType = property.PropertyType;
                var parseMethod = enumType.GetMethod("Parse", new[] { typeof(string) });
                if (parseMethod != null)
                {
                    var enumValue = Expression.Call(parseMethod, Expression.Constant(searchString));
                    var enumEqualExpression = Expression.Equal(propertyExpression, enumValue);
                    filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(enumEqualExpression, param));
                }
            }

            return filters;
        }
    }
}
