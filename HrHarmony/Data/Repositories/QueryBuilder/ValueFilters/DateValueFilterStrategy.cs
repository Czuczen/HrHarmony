using System.Linq.Expressions;
using System.Reflection;
using HrHarmony.Attributes;
using LinqKit;

namespace HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;

[RegisterOpenGenericClassInDi(typeof(DateValueFilterStrategy<>))]
public class DateValueFilterStrategy<TEntity> : IValueFilterStrategy<TEntity>
{
    public IEnumerable<Type> Types => new List<Type> { typeof(DateTime), typeof(DateTime?) };

    public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string value)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");
        var propertyExpression = Expression.Property(param, property.Name);

        if (DateTime.TryParse(value, out var searchDate))
        {
            var dateExpression = Expression.Constant(searchDate.Date, property.PropertyType);
            var dateEqualExpression = Expression.Equal(propertyExpression, dateExpression);
            filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(dateEqualExpression, param));
        }
        else if (int.TryParse(value, out var searchInt))
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