using HrHarmony.Attributes;
using LinqKit;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Repositories.Filters
{
    [RegisterOpenGenericClassInDI(typeof(DateFilterStrategy<>))]
    public class DateFilterStrategy<TEntity> : IFilterStrategy<TEntity>
    {
        public IEnumerable<Type> Types => new List<Type> { typeof(DateTime), typeof(DateTime?) };

        public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string searchString)
        {
            var param = Expression.Parameter(typeof(TEntity), "e");
            var propertyExpression = Expression.Property(param, property.Name);

            if (DateTime.TryParse(searchString, out var searchDate))
            {
                var dateExpression = Expression.Constant(searchDate.Date, property.PropertyType);
                var dateEqualExpression = Expression.Equal(propertyExpression, dateExpression);
                filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(dateEqualExpression, param));
            }
            else if (int.TryParse(searchString, out var searchInt))
            {
                var dateConditions = new List<Expression>();

                if (searchInt >= 1 && searchInt <= 31)
                    dateConditions.Add(Expression.Equal(
                        Expression.Property(propertyExpression, "Day"), Expression.Constant(searchInt)));

                if (searchInt >= 1 && searchInt <= 12)
                    dateConditions.Add(Expression.Equal(
                        Expression.Property(propertyExpression, "Month"), Expression.Constant(searchInt)));

                if (searchInt >= DateTime.MinValue.Year && searchInt <= DateTime.MaxValue.Year)
                    dateConditions.Add(Expression.Equal(
                        Expression.Property(propertyExpression, "Year"), Expression.Constant(searchInt)));

                var combinedDateExpression = dateConditions.Any() ? dateConditions.Aggregate(Expression.OrElse) : null;
                if (combinedDateExpression != null)
                    filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(combinedDateExpression, param));
            }

            return filters;
        }
    }
}
