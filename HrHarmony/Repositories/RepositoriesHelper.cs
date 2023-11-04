using HrHarmony.Configuration.Database;
using HrHarmony.Models.Dto;
using HrHarmony.Models.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Repositories
{
    public static class RepositoriesHelper
    {
        public static string GetDefaultSortField<TEntityDto, TPrimaryKey>(string? orderBy)
            where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
        {
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                var orderProp = typeof(TEntityDto).GetProperty(orderBy);
                if (orderProp != null)
                    return orderBy;
            }
            else
            {
                var preferredSortFields = new string[] { "name", "fullname", "type", "nr", "date", "description" };
                var properties = typeof(TEntityDto).GetProperties();

                foreach (var fieldName in preferredSortFields)
                {
                    var property = properties.FirstOrDefault(prop => prop.Name.ToLower().Contains(fieldName));
                    if (property != null)
                        return property.Name;
                }
            }
            
            return "Id";
        }

        public static IQueryable<TEntity> FilterEntities<TEntity, TPrimaryKey, TIndexViewModel>(IQueryable<TEntity> query, string? searchString)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            if (string.IsNullOrWhiteSpace(searchString)) return query;

            searchString = searchString.Trim().ToLower();
            var indexModelType = typeof(TIndexViewModel);

            //var dbProperties = indexModelType.GetProperties()
            var dbProperties = typeof(TEntity).GetProperties()
                .Where(p => p.Name.ToLower() != "Id" && !p.Name.ToLower().Contains("id") 
                    && (p.PropertyType.IsValueType || p.PropertyType == typeof(string) 
                        || Nullable.GetUnderlyingType(p.PropertyType) == typeof(string))
                    && !p.PropertyType.FullName.StartsWith("HrHarmony"));

           var filters = AddFilters<TEntity>(dbProperties, searchString);

            query = query.Where(filters);

            return query;
        }



        private static ExpressionStarter<TEntity> AddFilters<TEntity>(IEnumerable<PropertyInfo> dbProperties, string searchString)
        {
            var filters = PredicateBuilder.New<TEntity>(e => false);
            foreach (var property in dbProperties)
            {
                var param = Expression.Parameter(typeof(TEntity), "e");
                var propertyExpression = Expression.Property(param, property.Name);

                if (property.PropertyType == typeof(string))
                {
                    var isNullableString = Nullable.GetUnderlyingType(property.PropertyType) == typeof(string);

                    var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                    var toLowerExpression = Expression.Call(propertyExpression, toLowerMethod);

                    var searchValueExpression = Expression.Constant(searchString.ToLower(), typeof(string));

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
                }
                else if (property.PropertyType.IsNumericType() || property.PropertyType == typeof(Guid))
                {
                    var parseMethod = property.PropertyType.GetMethod("Parse", new[] { typeof(string) });
                    if (parseMethod != null)
                    {
                        var parseExpression = Expression.Call(parseMethod, Expression.Call(propertyExpression, typeof(object).GetMethod("ToString")));
                        var searchValueExpression = Expression.Constant(Convert.ChangeType(searchString, property.PropertyType), property.PropertyType);
                        var compareExpression = Expression.Equal(parseExpression, searchValueExpression);
                        filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(compareExpression, param));
                    }
                }
                else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    if (DateTime.TryParse(searchString, out var searchDate))
                    {
                        var dateExpression = Expression.Constant(searchDate.Date, property.PropertyType);
                        var dateEqualExpression = Expression.Equal(propertyExpression, dateExpression);
                        filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(dateEqualExpression, param));
                    }
                }
                else if (property.PropertyType == typeof(char) || property.PropertyType == typeof(char?))
                {
                    if (searchString.Length == 1)
                    {
                        var charExpression = Expression.Constant(searchString[0], property.PropertyType);
                        var charEqualExpression = Expression.Equal(propertyExpression, charExpression);
                        filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(charEqualExpression, param));
                    }
                }
                else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                {
                    if (bool.TryParse(searchString, out var searchBool))
                    {
                        var boolExpression = Expression.Constant(searchBool, property.PropertyType);
                        var boolEqualExpression = Expression.Equal(propertyExpression, boolExpression);
                        filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(boolEqualExpression, param));
                    }
                }
                else if (property.PropertyType.IsEnum)
                {
                    // Obsługa typu enum
                    var enumType = property.PropertyType;
                    var parseMethod = enumType.GetMethod("Parse", new[] { typeof(string) });
                    if (parseMethod != null)
                    {
                        var enumValue = Expression.Call(parseMethod, Expression.Constant(searchString));
                        var enumEqualExpression = Expression.Equal(propertyExpression, enumValue);
                        filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(enumEqualExpression, param));
                    }
                }
            }

            return filters;
        }

        // Metoda rozszerzająca do sprawdzania, czy dany typ jest liczbowy
        public static bool IsNumericType(this Type type)
        {
            return type == typeof(byte) || type == typeof(sbyte) ||
                   type == typeof(short) || type == typeof(ushort) ||
                   type == typeof(int) || type == typeof(uint) ||
                   type == typeof(long) || type == typeof(ulong) ||
                   type == typeof(float) || type == typeof(double) || type == typeof(decimal) ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && Nullable.GetUnderlyingType(type).IsNumericType());
        }






        //private static ExpressionStarter<TEntity> AddFilters<TEntity>(IEnumerable<PropertyInfo> dbProperties, string searchString)
        //{
        //    var filters = PredicateBuilder.New<TEntity>(e => false);
        //    foreach (var property in dbProperties)
        //    {
        //        if (property.PropertyType == typeof(string))
        //        {
        //            bool isNullableString = Nullable.GetUnderlyingType(property.PropertyType) == typeof(string);
        //            if (isNullableString)
        //                filters = filters.Or(e => EF.Property<string?>(e, property.Name).ToLower().Contains(searchString));
        //            else
        //                filters = filters.Or(e => EF.Property<string>(e, property.Name).ToLower().Contains(searchString));
        //        }
        //        else if (property.PropertyType == typeof(int))
        //        {
        //            filters = filters.Or(e => EF.Property<int>(e, property.Name).ToString().Contains(searchString));
        //        }
        //        else if (property.PropertyType == typeof(int?))
        //        {
        //            filters = filters.Or(e => EF.Property<int?>(e, property.Name).ToString().Contains(searchString));
        //        }
        //        else if (property.PropertyType == typeof(long))
        //        {
        //            filters = filters.Or(e => EF.Property<long>(e, property.Name).ToString().Contains(searchString));
        //        }
        //        else if (property.PropertyType == typeof(long?))
        //        {
        //            filters = filters.Or(e => EF.Property<long?>(e, property.Name).ToString().Contains(searchString));
        //        }
        //        else if (property.PropertyType == typeof(decimal))
        //        {
        //            filters = filters.Or(e => EF.Property<decimal>(e, property.Name).ToString().Contains(searchString));
        //        }
        //        else if (property.PropertyType == typeof(decimal?))
        //        {
        //            filters = filters.Or(e => EF.Property<decimal?>(e, property.Name).ToString().Contains(searchString));
        //        }
        //        else if (property.PropertyType == typeof(DateTime))
        //        {
        //            if (DateTime.TryParse(searchString, out var searchDate))
        //                filters = filters.Or(e => EF.Property<DateTime>(e, property.Name).Date == searchDate.Date);
        //        }
        //        else if (property.PropertyType == typeof(DateTime?))
        //        {
        //            //filters = filters.Or(e => EF.Property<DateTime?>(e, property.Name).ToString("yyyy-MM-dd").Contains(searchString));
        //        }
        //    }

        //    return filters;
        //}
    }
}
