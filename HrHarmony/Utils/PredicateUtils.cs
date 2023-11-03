using HrHarmony.Models.Entities;
using HrHarmony.Models.Entities.Main;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace HrHarmony.Utils
{
    public static class PredicateUtils<TEntity>
    {
        //public static Expression<Func<Employee, bool>> IsActive = entity => entity.Id != null;
        //public static Expression<Func<Employee, int, bool>> ByValue = (entity, value) => entity.Id == value;

        //public static Expression<Func<Employee, bool>> GetActiveEntitiesWithValue()
        //{
        //    var combinedPredicate = Expression.Lambda<Func<Employee, bool>>(
        //        Expression.And(PredicateUtils<Employee>.IsActive, PredicateUtils<Employee>.ByValue));

        //    return combinedPredicate;
        //}

        //public static void FuncA()
        //{
        //    Func<Employee, int, bool> combinedPredicate = (entity, value) =>
        //        PredicateUtils<TEntity>.IsActive.Compile()(entity) &&
        //        PredicateUtils<TEntity>.ByValue.Compile()(entity, value);
        //}
    }
}
