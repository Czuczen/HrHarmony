using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Utils;
using static HrHarmony.Data.Models.Entities.Enums;

namespace HrHarmony.Data.Database.SeedData;

public static class SeedData
{
    private static readonly Random Random = new();
    public static bool GroupSaveChanges = true;

    public static int? MaritalStatusesCount = 10;
    public static int? AddressesCount = 100;
    public static int? EducationLevelsCount = 10;
    public static int? ExperiencesCount = 12;
    public static int? EmployeesCount = 500;
    public static int? ContractTypesCount = 7;
    public static int? LeaveTypesCount = 5;
    public static int? AbsenceTypesCount = 4;
    public static int? EmploymentContractsCount = 700;
    public static int? LeavesCount = 1000;
    public static int? AbsencesCount = 2000;
    public static int? SalariesCount = 5000;
    

    public static void Initialize(ApplicationDbContext context, SampleObjectsCreationSizeLevel sizeLevel)
    {
        SetCreationSize(sizeLevel);

        // kolejność ma znaczenie
        CreateMaritalStatuses(context, MaritalStatusesCount);
        CreateAddresses(context, AddressesCount);
        CreateEducationLevels(context, EducationLevelsCount);
        CreateExperiences(context, ExperiencesCount);
        context.SaveChanges();

        CreateEmployees(context, EmployeesCount);
        context.SaveChanges();
        
        CreateContractTypes(context, ContractTypesCount);
        CreateLeaveTypes(context, LeaveTypesCount);
        CreateAbsenceTypes(context, AbsenceTypesCount);
        context.SaveChanges();

        CreateEmploymentContracts(context, EmploymentContractsCount);
        CreateLeaves(context, LeavesCount);
        CreateAbsences(context, AbsencesCount);
        CreateSalaries(context, SalariesCount);
        context.SaveChanges();
    }

    // ============== słownikowe połączone z employee ===========================

    public static MaritalStatus CreateMaritalStatus(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var maritalStatus = new MaritalStatus
        {
            StatusName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.MaritalStatuses.Add(maritalStatus);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return maritalStatus;
    }

    public static void CreateMaritalStatuses(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateMaritalStatus(context);
    }

    public static Address CreateAddress(ApplicationDbContext context)
    {
        var streetLength = Random.Next(3, 21);
        var cityLength = Random.Next(3, 21);

        var firstPart = Random.Next(10, 100);
        var secondPart = Random.Next(100, 1000);

        var address = new Address
        {
            Street = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(streetLength)),
            City = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(cityLength)),
            PostalCode = $"{firstPart:D2}-{secondPart:D3}"
        };

        context.Addresses.Add(address);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return address;
    }

    public static void CreateAddresses(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateAddress(context);
    }

    public static EducationLevel CreateEducationLevel(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var educationLevel = new EducationLevel
        {
            LevelName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.EducationLevels.Add(educationLevel);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return educationLevel;
    }

    public static void CreateEducationLevels(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateEducationLevel(context);
    }

    public static Experience CreateExperience(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var experience = new Experience
        {
            ExperienceDescription = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.Experiences.Add(experience);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return experience;
    }

    public static void CreateExperiences(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateExperience(context);
    }

    // ============== słownikowe połączone z innymi ===========================

    public static ContractType CreateContractType(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var contractType = new ContractType
        {
            TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.ContractTypes.Add(contractType);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return contractType;
    }

    public static void CreateContractTypes(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateContractType(context);
    }

    public static LeaveType CreateLeaveType(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var leaveType = new LeaveType
        {
            TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.LeaveTypes.Add(leaveType);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return leaveType;
    }

    public static void CreateLeaveTypes(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateLeaveType(context);
    }

    public static AbsenceType CreateAbsenceType(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var absenceType = new AbsenceType
        {
            TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.AbsenceTypes.Add(absenceType);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return absenceType;
    }

    public static void CreateAbsenceTypes(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
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

        var fullNameLength = Random.Next(3, 21);
        var emailLength = Random.Next(3, 11);

        var employee = new Employee
        {
            FullName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(fullNameLength)),
            Email = StringUtils.GenerateRandomString(emailLength).ToLower() + "@example.com",
            PhoneNumber = "+48" + Random.Next(10000000, 99999999),
            DateOfBirth = DateTime.Now.AddYears(-30).AddDays(Random.Next(365 * 30)),
            MaritalStatusId = maritalStatusIds[Random.Next(0, maritalStatusIds.Count)],
            AddressId = addressIds[Random.Next(0, addressIds.Count)],
            EducationLevelId = educationLevelIds[Random.Next(0, educationLevelIds.Count)],
            ExperienceId = experienceIds[Random.Next(0, experienceIds.Count)]
        };

        context.Employees.Add(employee);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return employee;
    }

    public static void CreateEmployees(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateEmployee(context);
    }

    // ============== główne połączone z employee ===========================

    public static EmploymentContract CreateEmploymentContract(ApplicationDbContext context)
    {
        var contractTypesIds = context.ContractTypes.Select(a => a.Id).ToList();
        var employeesIds = context.Employees.Select(a => a.Id).ToList();

        var hourlyRate = (decimal)(Random.Next(1000, 5000) / 100.0);
        var monthlyRate = (decimal)(Random.Next(50000, 100000) / 100.0);
        var basicSalary = (decimal)(Random.Next(3000000, 8000000) / 100.0);

        var employmentContract = new EmploymentContract
        {
            ContractNumber = "Contract-" + Random.Next(1000, 10000),
            StartDate = DateTime.Now.AddMonths(-Random.Next(1, 13)),
            EndDate = DateTime.Now.AddMonths(Random.Next(1, 13)),
            ContractTypeId = contractTypesIds[Random.Next(0, contractTypesIds.Count)],
            EmployeeId = employeesIds[Random.Next(0, employeesIds.Count)],
            HourlyRate = Math.Round(hourlyRate, 2),
            MonthlyRate = Math.Round(monthlyRate, 2),
            BasicSalary = Math.Round(basicSalary, 2),
        };

        context.EmploymentContracts.Add(employmentContract);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return employmentContract;
    }

    public static void CreateEmploymentContracts(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateEmploymentContract(context);
    }

    public static Leave CreateLeave(ApplicationDbContext context)
    {
        var employeesIds = context.Employees.Select(a => a.Id).ToList();
        var leaveTypesIds = context.LeaveTypes.Select(a => a.Id).ToList();

        var leave = new Leave
        {
            StartDate = DateTime.Now.AddDays(-Random.Next(1, 11)),
            EndDate = DateTime.Now.AddDays(Random.Next(1, 11)),
            LeaveTypeId = leaveTypesIds[Random.Next(0, leaveTypesIds.Count)],
            EmployeeId = employeesIds[Random.Next(0, employeesIds.Count)],
        };

        context.Leaves.Add(leave);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return leave;
    }

    public static void CreateLeaves(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateLeave(context);
    }

    public static Absence CreateAbsence(ApplicationDbContext context)
    {
        var employeesIds = context.Employees.Select(a => a.Id).ToList();
        var absenceTypesIds = context.AbsenceTypes.Select(a => a.Id).ToList();

        var absence = new Absence
        {
            AbsenceDate = DateTime.Now.AddDays(-Random.Next(1, 11)),
            AbsenceTypeId = absenceTypesIds[Random.Next(0, absenceTypesIds.Count)],
            EmployeeId = employeesIds[Random.Next(0, employeesIds.Count)],
        };

        context.Absences.Add(absence);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return absence;
    }

    public static void CreateAbsences(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateAbsence(context);
    }

    public static Salary CreateSalary(ApplicationDbContext context)
    {
        var employeesIds = context.Employees.Select(a => a.Id).ToList();

        var basicSalary = (decimal)(Random.Next(3000000, 8000000) / 100.0);
        var additionalSalary = (decimal)(Random.Next(50000, 100000) / 100.0);
        var bonuses = (decimal)(Random.Next(10000, 50000) / 100.0);
        var allowances = (decimal)(Random.Next(1000, 5000) / 100.0);
        var zusContributions = (decimal)(Random.Next(100, 500) / 100.0);
        var incomeTax = (decimal)(Random.Next(200, 1000) / 100.0);

        var salary = new Salary
        {
            PaymentDate = DateTime.Now.AddDays(-Random.Next(1, 11)),
            EmployeeId = employeesIds[Random.Next(0, employeesIds.Count)],
            BasicSalary = Math.Round(basicSalary, 2),
            AdditionalSalary = Math.Round(additionalSalary, 2),
            Bonuses = Math.Round(bonuses, 2),
            Allowances = Math.Round(allowances, 2),
            ZUSContributions = Math.Round(zusContributions, 2),
            IncomeTax = Math.Round(incomeTax, 2),
        };

        context.Salaries.Add(salary);
        if (!GroupSaveChanges)
            context.SaveChanges();

        return salary;
    }

    public static void CreateSalaries(ApplicationDbContext context, int? howMany = null)
    {
        howMany = howMany ?? Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            CreateSalary(context);
    }


    // ====================================================================================================
    // ====================================================================================================
    // ====================================================================================================

    private static void SetCreationSize(SampleObjectsCreationSizeLevel sizeLevel)
    {
        if (sizeLevel == SampleObjectsCreationSizeLevel.Low)
        {
            
        }
    }
}