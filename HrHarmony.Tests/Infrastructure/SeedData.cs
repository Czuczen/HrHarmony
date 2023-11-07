using HrHarmony.Data.Database;
using HrHarmony.Models.Entities.Dictionary;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Utils;

namespace HrHarmony.Tests.Infrastructure;

public static class SeedData
{
    private static readonly Random _random = new();

    public static void Initialize(ApplicationDbContext context)
    {
        // kolejność ma znaczenie
        CreateMaritalStatuses(context);
        CreateAddresses(context);
        CreateEducationLevels(context);
        CreateExperiences(context);
        
        CreateEmployees(context);
        
        CreateContractTypes(context);
        CreateLeaveTypes(context);
        CreateAbsenceTypes(context);
        
        CreateEmploymentContracts(context);
        CreateLeaves(context);
        CreateAbsences(context);
        CreateSalaries(context);
    }

    // ============== słownikowe połączone z employee ===========================

    public static MaritalStatus CreateMaritalStatus(ApplicationDbContext context)
    {
        var stringLength = _random.Next(3, 21);
        var maritalStatus = new MaritalStatus
        {
            StatusName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.MaritalStatuses.Add(maritalStatus);
        context.SaveChanges();
        
        return maritalStatus;
    }

    public static void CreateMaritalStatuses(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateMaritalStatus(context);
    }

    public static Address CreateAddress(ApplicationDbContext context)
    {
        var streetLength = _random.Next(3, 21);
        var cityLength = _random.Next(3, 21);

        var firstPart = _random.Next(10, 100);
        var secondPart = _random.Next(100, 1000);

        var address = new Address
        {
            Street = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(streetLength)),
            City = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(cityLength)),
            PostalCode = $"{firstPart:D2}-{secondPart:D3}"
        };

        context.Addresses.Add(address);
        context.SaveChanges();
        
        return address;
    }

    public static void CreateAddresses(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateAddress(context);
    }

    public static EducationLevel CreateEducationLevel(ApplicationDbContext context)
    {
        var stringLength = _random.Next(3, 21);
        var educationLevel = new EducationLevel
        {
            LevelName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.EducationLevels.Add(educationLevel);
        context.SaveChanges();

        return educationLevel;
    }

    public static void CreateEducationLevels(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateEducationLevel(context);
    }

    public static Experience CreateExperience(ApplicationDbContext context)
    {
        var stringLength = _random.Next(3, 21);
        var experience = new Experience
        {
            ExperienceDescription = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.Experiences.Add(experience);
        context.SaveChanges();
        
        return experience;
    }

    public static void CreateExperiences(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateExperience(context);
    }

    // ============== słownikowe połączone z innymi ===========================

    public static ContractType CreateContractType(ApplicationDbContext context)
    {
        var stringLength = _random.Next(3, 21);
        var contractType = new ContractType
        {
            TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.ContractTypes.Add(contractType);
        context.SaveChanges();

        return contractType;
    }

    public static void CreateContractTypes(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateContractType(context);
    }

    public static LeaveType CreateLeaveType(ApplicationDbContext context)
    {
        var stringLength = _random.Next(3, 21);
        var leaveType = new LeaveType
        {
            TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.LeaveTypes.Add(leaveType);
        context.SaveChanges();

        return leaveType;
    }

    public static void CreateLeaveTypes(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateLeaveType(context);
    }

    public static AbsenceType CreateAbsenceType(ApplicationDbContext context)
    {
        var stringLength = _random.Next(3, 21);
        var absenceType = new AbsenceType
        {
            TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.AbsenceTypes.Add(absenceType);
        context.SaveChanges();
        
        return absenceType;
    }

    public static void CreateAbsenceTypes(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateAbsenceType(context);
    }

    // ============== employee ===========================

    public static Employee CreateEmployee(ApplicationDbContext context)
    {
        var maritalStatusIds = context.MaritalStatuses.Select(a => a.Id).ToList();
        var addressIds = context.Addresses.Select(a => a.Id).ToList();
        var educationLevelIds = context.EducationLevels.Select(a => a.Id).ToList();
        var experienceIds = context.Experiences.Select(a => a.Id).ToList();

        var fullNameLength = _random.Next(3, 21);
        var emailLength = _random.Next(3, 11);

        var employee = new Employee
        {
            FullName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(fullNameLength)),
            Email = StringUtils.GenerateRandomString(emailLength).ToLower() + "@example.com",
            PhoneNumber = "+48" + _random.Next(10000000, 99999999),
            DateOfBirth = DateTime.Now.AddYears(-30).AddDays(_random.Next(365 * 30)),
            MaritalStatusId = maritalStatusIds[_random.Next(0, maritalStatusIds.Count)],
            AddressId = addressIds[_random.Next(0, addressIds.Count)],
            EducationLevelId = educationLevelIds[_random.Next(0, educationLevelIds.Count)],
            ExperienceId = experienceIds[_random.Next(0, experienceIds.Count)]
        };

        context.Employees.Add(employee);
        context.SaveChanges();

        return employee;
    }

    public static void CreateEmployees(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateEmployee(context);
    }

    // ============== główne połączone z employee ===========================

    public static EmploymentContract CreateEmploymentContract(ApplicationDbContext context)
    {
        var contractTypesIds = context.ContractTypes.Select(a => a.Id).ToList();
        var employeesIds = context.Employees.Select(a => a.Id).ToList();

        var hourlyRate = (decimal)(_random.Next(1000, 5000) / 100.0);
        var monthlyRate = (decimal)(_random.Next(50000, 100000) / 100.0);
        var basicSalary = (decimal)(_random.Next(3000000, 8000000) / 100.0);

        var employmentContract = new EmploymentContract
        {
            ContractNumber = "Contract-" + _random.Next(1000, 10000),
            StartDate = DateTime.Now.AddMonths(-_random.Next(1, 13)),
            EndDate = DateTime.Now.AddMonths(_random.Next(1, 13)),
            ContractTypeId = contractTypesIds[_random.Next(0, contractTypesIds.Count)],
            EmployeeId = employeesIds[_random.Next(0, employeesIds.Count)],
            HourlyRate = Math.Round(hourlyRate, 2),
            MonthlyRate = Math.Round(monthlyRate, 2),
            BasicSalary = Math.Round(basicSalary, 2),
        };

        context.EmploymentContracts.Add(employmentContract);
        context.SaveChanges();

        return employmentContract;
    }

    public static void CreateEmploymentContracts(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateEmploymentContract(context);
    }

    public static Leave CreateLeave(ApplicationDbContext context)
    {
        var employeesIds = context.Employees.Select(a => a.Id).ToList();
        var leaveTypesIds = context.LeaveTypes.Select(a => a.Id).ToList();

        var leave = new Leave
        {
            StartDate = DateTime.Now.AddDays(-_random.Next(1, 11)),
            EndDate = DateTime.Now.AddDays(_random.Next(1, 11)),
            LeaveTypeId = leaveTypesIds[_random.Next(0, leaveTypesIds.Count)],
            EmployeeId = employeesIds[_random.Next(0, employeesIds.Count)],
        };

        context.Leaves.Add(leave);
        context.SaveChanges();
        
        return leave;
    }

    public static void CreateLeaves(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateLeave(context);
    }

    public static Absence CreateAbsence(ApplicationDbContext context)
    {
        var employeesIds = context.Employees.Select(a => a.Id).ToList();
        var absenceTypesIds = context.AbsenceTypes.Select(a => a.Id).ToList();

        var absence = new Absence
        {
            AbsenceDate = DateTime.Now.AddDays(-_random.Next(1, 11)),
            AbsenceTypeId = absenceTypesIds[_random.Next(0, absenceTypesIds.Count)],
            EmployeeId = employeesIds[_random.Next(0, employeesIds.Count)],
        };

        context.Absences.Add(absence);
        context.SaveChanges();
        
        return absence;
    }

    public static void CreateAbsences(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateAbsence(context);
    }

    public static Salary CreateSalary(ApplicationDbContext context)
    {
        var employeesIds = context.Employees.Select(a => a.Id).ToList();

        var basicSalary = (decimal)(_random.Next(3000000, 8000000) / 100.0);
        var additionalSalary = (decimal)(_random.Next(50000, 100000) / 100.0);
        var bonuses = (decimal)(_random.Next(10000, 50000) / 100.0);
        var allowances = (decimal)(_random.Next(1000, 5000) / 100.0);
        var zusContributions = (decimal)(_random.Next(100, 500) / 100.0);
        var incomeTax = (decimal)(_random.Next(200, 1000) / 100.0);

        var salary = new Salary
        {
            PaymentDate = DateTime.Now.AddDays(-_random.Next(1, 11)),
            EmployeeId = employeesIds[_random.Next(0, employeesIds.Count)],
            BasicSalary = Math.Round(basicSalary, 2),
            AdditionalSalary = Math.Round(additionalSalary, 2),
            Bonuses = Math.Round(bonuses, 2),
            Allowances = Math.Round(allowances, 2),
            ZUSContributions = Math.Round(zusContributions, 2),
            IncomeTax = Math.Round(incomeTax, 2),
        };

        context.Salaries.Add(salary);
        context.SaveChanges();
        
        return salary;
    }

    public static void CreateSalaries(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? _random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateSalary(context);
    }
}