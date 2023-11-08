using HrHarmony.Data.Repositories.AccessLimiters.Selectable;
using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Utils;

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

    public static Func<IQueryable<Employee>, IQueryable<Employee>> CustomEmployeeWithRelatedProjectionQ = q => q.Select(item =>
        new Employee
        {
            Id = item.Id,
            FullName = item.FullName,
            Address = new Address
            {
                Street = item.Address.Street,
                City = item.Address.City,
            },
            Salaries = item.Salaries.Select(salary => new Salary
            {
                AdditionalSalary = salary.AdditionalSalary
            }).ToList()
        });

    public static Func<Selectable<Employee, EmployeeDto>, IQueryable<EmployeeDto>> CustomEmployeeWithRelatedProjectionF = s => s.Select(item =>
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