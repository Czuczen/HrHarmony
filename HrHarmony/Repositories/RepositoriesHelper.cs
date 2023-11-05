using HrHarmony.Configuration.Database;
using HrHarmony.Models.Dto;
using HrHarmony.Models.Entities;
using HrHarmony.Repositories.Filters;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Repositories
{
    public static class RepositoriesHelper
    {
        public static string GetSortField<TEntityDto, TPrimaryKey>(string? orderBy)
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

        public static IQueryable<TEntity> FilterEntities<TEntity, TPrimaryKey, TIndexViewModel>(
            IQueryable<TEntity> query, string? searchString, IEnumerable<IFilterStrategy<TEntity>> filterStrategies)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            if (string.IsNullOrWhiteSpace(searchString)) return query;

            searchString = searchString.Trim().ToLower();
            var dbProperties = typeof(TIndexViewModel).GetProperties()
                .Where(p => p.Name.ToLower() != "id" && !p.Name.ToLower().Contains("id") 
                    && (p.PropertyType.IsValueType || p.PropertyType == typeof(string) 
                        || Nullable.GetUnderlyingType(p.PropertyType) == typeof(string))
                    && !p.PropertyType.FullName.StartsWith("HrHarmony"));

            var filters = PredicateBuilder.New<TEntity>(e => false);
            foreach (var property in dbProperties)
                filterStrategies.Single(item => item.Types.Any(type => type == property.PropertyType)).ApplyFilter(filters, property, searchString);

            return query.Where(filters);
        }
    }
}