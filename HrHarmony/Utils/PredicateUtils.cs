using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace HrHarmony.Utils
{
    public static class PredicateUtils
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

        public static Func<Selectable<Employee, EmployeeDto>, IQueryable<EmployeeDto>> CustomEmployeeWithRelatedProjection = s => s.Select(item =>
            new EmployeeDto
            {
                Id = item.Id,
                FullName = item.FullName,
                Address = new AddressDto
                {
                    Street = item.Address.Street,
                    City = item.Address.City,
                },
                Salaries = item.Salaries.Select(salary => new SalaryDto
                {
                    AdditionalSalary = salary.AdditionalSalary
                }).ToList()
            });
    }
}
