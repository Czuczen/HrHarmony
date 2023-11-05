﻿using HrHarmony.Attributes;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace HrHarmony.Repositories.Filters
{
    [RegisterOpenGenericClassInDI(typeof(NumericFilterStrategy<>))]
    public class NumericFilterStrategy<TEntity> : IFilterStrategy<TEntity>
    {
        public IEnumerable<Type> Types => new List<Type> 
        { 
            typeof(byte),   typeof(sbyte),      typeof(byte?),      typeof(sbyte?),
            typeof(short),  typeof(ushort),     typeof(short?),     typeof(ushort?),
            typeof(int),    typeof(uint),       typeof(int?),       typeof(uint?),
            typeof(long),   typeof(ulong),      typeof(long?),      typeof(ulong?),
            typeof(float),  typeof(double),     typeof(float?),     typeof(double?),
            typeof(decimal),                    typeof(decimal?),
            typeof(Guid),                       typeof(Guid?)
        };

        public ExpressionStarter<TEntity> ApplyFilter(ExpressionStarter<TEntity> filters, PropertyInfo property, string searchString)
        {
            var param = Expression.Parameter(typeof(TEntity), "e");
            var propertyExpression = Expression.Property(param, property.Name);

            var parseMethod = property.PropertyType.GetMethod("Parse", new[] { typeof(string) });
            if (parseMethod != null)
            {
                var parseExpression = Expression.Call(parseMethod, Expression.Call(propertyExpression, typeof(object).GetMethod("ToString")));
                var searchValueExpression = Expression.Constant(Convert.ChangeType(searchString, property.PropertyType), property.PropertyType);
                var compareExpression = Expression.Equal(parseExpression, searchValueExpression);
                filters = filters.Or(Expression.Lambda<Func<TEntity, bool>>(compareExpression, param));
            }
           
            return filters;
        }
    }
}