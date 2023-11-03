using HrHarmony.Models.Dto;
using HrHarmony.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public static IQueryable<TEntity> FilterEntities<TEntity, TPrimaryKey>(IQueryable<TEntity> query, string? searchString, string? searchBy)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            if (!string.IsNullOrWhiteSpace(searchString) && !string.IsNullOrWhiteSpace(searchBy))
            {
                searchBy = searchBy.Trim().ToLower();
                searchString = searchString.Trim().ToLower();

                var entityType = typeof(TEntity);
                var parameter = Expression.Parameter(entityType, "e");
                var property = Expression.Property(parameter, searchBy);
                var toLower = Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
                var containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) }); // Wyraźne określenie przeciążenia
                var searchStringExpression = Expression.Constant(searchString);

                var containsCall = Expression.Call(toLower, containsMethod, searchStringExpression);
                var lambda = Expression.Lambda(containsCall, parameter);

                return query.Where((Expression<Func<TEntity, bool>>)lambda);
            }

            return query;
        }
    }
}
