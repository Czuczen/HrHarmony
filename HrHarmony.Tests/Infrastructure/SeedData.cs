using HrHarmony.Configuration.Database;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HrHarmony.Tests.Infrastructure;

public static class SeedData
{
    private static readonly Random _random = new();

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        CreateMaritalStatuses(context);
        CreateAddresses(context);
        CreateEducationLevels(context);
        CreateExperiences(context);
        context.SaveChanges();

        CreateEmployees(context);
        context.SaveChanges();

        CreateContractTypes(context);
        CreateLeaveTypes(context);
        CreateAbsenceTypes(context);
        context.SaveChanges();

        CreateEmploymentContracts(context);
        CreateLeaves(context);
        CreateAbsences(context);
        CreateSalaries(context);
        context.SaveChanges();
    }

    // ============== słownikowe połączone z employee ===========================

    private static void CreateMaritalStatuses(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
        {
            var stringLength = _random.Next(1, 21);
            var maritalStatus = new MaritalStatus
            {
                StatusName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
            };

            context.MaritalStatuses.Add(maritalStatus);
        }
    }

    private static void CreateAddresses(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11); 
        for (int i = 0; i < howMany; i++)
        {
            var streetLength = _random.Next(1, 21);
            var cityLength = _random.Next(1, 21);

            var firstPart = _random.Next(10, 100);
            var secondPart = _random.Next(100, 1000);
            
            var address = new Address
            {
                Street = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(streetLength)),
                City = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(cityLength)),
                PostalCode = $"{firstPart:D2}-{secondPart:D3}"
            };

            context.Addresses.Add(address);
        }
    }

    private static void CreateEducationLevels(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
        {
            var stringLength = _random.Next(1, 21);
            var educationLevel = new EducationLevel
            {
                LevelName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
            };

            context.EducationLevels.Add(educationLevel);
        }
    }

    private static void CreateExperiences(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
        {
            var stringLength = _random.Next(1, 21);
            var experience = new Experience
            {
                ExperienceDescription = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
            };

            context.Experiences.Add(experience);
        }
    }

    // ============== słownikowe połączone z innymi ===========================

    private static void CreateContractTypes(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
        {
            var stringLength = _random.Next(1, 21);
            var contractType = new ContractType
            {
                TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
            };

            context.ContractTypes.Add(contractType);
        }
    }

    private static void CreateLeaveTypes(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
        {
            var stringLength = _random.Next(1, 21);
            var leaveType = new LeaveType
            {
                TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
            };

            context.LeaveTypes.Add(leaveType);
        }
    }

    private static void CreateAbsenceTypes(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
        {
            var stringLength = _random.Next(1, 21);
            var absenceType = new AbsenceType
            {
                TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
            };

            context.AbsenceTypes.Add(absenceType);
        }
    }
    
    // ============== employee ===========================

    private static void CreateEmployees(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);

        var maritalStatusCount = context.MaritalStatuses.Count();
        var addressCount = context.Addresses.Count();
        var educationLevelCount = context.EducationLevels.Count();
        var experienceCount = context.Experiences.Count();

        for (int i = 0; i < howMany; i++)
        {
            var fullNameLength = _random.Next(1, 21);
            var emailLength = _random.Next(1, 11);

            var employee = new Employee
            {
                FullName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(fullNameLength)),
                Email = StringUtils.GenerateRandomString(emailLength).ToLower() + "@example.com",
                PhoneNumber = "+48" + _random.Next(1, 9),
                DateOfBirth = DateTime.Now.AddYears(-30).AddDays(_random.Next(365 * 30)),
                MaritalStatusId = _random.Next(1, maritalStatusCount + 1),
                AddressId = _random.Next(1, addressCount + 1),
                EducationLevelId = _random.Next(1, educationLevelCount + 1),
                ExperienceId = _random.Next(1, experienceCount + 1)
            };

            context.Employees.Add(employee);
        }
    }

    // ============== główne połączone z employee ===========================

    private static void CreateEmploymentContracts(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);

        var contractTypesCount = context.ContractTypes.Count();
        var employeesCount = context.Employees.Count();

        for (int i = 0; i < howMany; i++)
        {
            var hourlyRate = (decimal) (_random.Next(1000, 5000) / 100.0);
            var monthlyRate = (decimal) (_random.Next(50000, 100000) / 100.0);
            var basicSalary = (decimal) (_random.Next(3000000, 8000000) / 100.0);

            var employmentContract = new EmploymentContract
            {
                ContractNumber = "Contract-" + _random.Next(1000, 10000),
                StartDate = DateTime.Now.AddMonths(-_random.Next(1, 13)),
                EndDate = DateTime.Now.AddMonths(_random.Next(1, 13)),
                ContractTypeId = _random.Next(1, contractTypesCount + 1),
                EmployeeId = _random.Next(1, employeesCount + 1),
                HourlyRate = Math.Round(hourlyRate, 2),
                MonthlyRate = Math.Round(monthlyRate, 2),
                BasicSalary = Math.Round(basicSalary, 2),
            };

            context.EmploymentContracts.Add(employmentContract);
        }
    }

    private static void CreateLeaves(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);

        var employeesCount = context.Employees.Count();
        var leaveTypesCount = context.LeaveTypes.Count();

        for (int i = 0; i < howMany; i++)
        {
            var leave = new Leave
            {
                StartDate = DateTime.Now.AddDays(-_random.Next(1, 11)),
                EndDate = DateTime.Now.AddDays(_random.Next(1, 11)),
                LeaveTypeId = _random.Next(1, leaveTypesCount + 1),
                EmployeeId = _random.Next(1, employeesCount + 1),
            };

            context.Leaves.Add(leave);
        }
    }

    private static void CreateAbsences(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);

        var employeesCount = context.Employees.Count();
        var absenceTypesCount = context.AbsenceTypes.Count();

        for (int i = 0; i < howMany; i++)
        {
            var absence = new Absence
            {
                AbsenceDate = DateTime.Now.AddDays(-_random.Next(1, 11)),
                AbsenceTypeId = _random.Next(1, absenceTypesCount + 1),
                EmployeeId = _random.Next(1, employeesCount + 1),
            };

            context.Absences.Add(absence);
        }
    }

    private static void CreateSalaries(ApplicationDbContext context)
    {
        var howMany = _random.Next(1, 11);

        var employeesCount = context.Employees.Count();

        for (int i = 0; i < howMany; i++)
        {
            var basicSalary = (decimal) (_random.Next(3000000, 8000000) / 100.0);
            var additionalSalary = (decimal) (_random.Next(50000, 100000) / 100.0);
            var bonuses = (decimal) (_random.Next(10000, 50000) / 100.0);
            var allowances = (decimal) (_random.Next(1000, 5000) / 100.0);
            var zusContributions = (decimal) (_random.Next(100, 500) / 100.0);
            var incomeTax = (decimal) (_random.Next(200, 1000) / 100.0);

            var salary = new Salary
            {
                PaymentDate = DateTime.Now.AddDays(-_random.Next(1, 11)),
                EmployeeId = _random.Next(1, employeesCount + 1),
                BasicSalary = Math.Round(basicSalary, 2),
                AdditionalSalary = Math.Round(additionalSalary, 2),
                Bonuses = Math.Round(bonuses, 2),
                Allowances = Math.Round(allowances, 2),
                ZUSContributions = Math.Round(zusContributions, 2),
                IncomeTax = Math.Round(incomeTax, 2),
            };

            context.Salaries.Add(salary);
        }
    }
}