using HrHarmony.Data.Models.Entities.Dictionary;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Utils;
using static HrHarmony.Consts.Enums;

namespace HrHarmony.Data.Database.SeedData;

public static class RandomDataSeeder
{
    private static readonly Random Random = new();
    private static bool _groupSaveChanges;

    private static int? _maritalStatusesCount;
    private static int? _addressesCount;
    private static int? _educationLevelsCount;
    private static int? _experiencesCount;
    private static int? _employeesCount;
    private static int? _contractTypesCount;
    private static int? _absenceTypesCount;
    private static int? _employmentContractsCount;
    private static int? _absencesCount;
    private static int? _salariesCount;
    

    public static async Task Initialize(ApplicationDbContext context, SampleObjectsCreationSizeLevel? sizeLevel)
    {
        SetCreationSize(sizeLevel);
        _groupSaveChanges = true;

        // kolejność ma znaczenie
        await CreateMaritalStatuses(context, _maritalStatusesCount);
        await CreateAddresses(context, _addressesCount);
        await CreateEducationLevels(context, _educationLevelsCount);
        await CreateExperiences(context, _experiencesCount);
        await context.SaveChangesAsync();

        await CreateEmployees(context, _employeesCount);
        await context.SaveChangesAsync();

        await CreateContractTypes(context, _contractTypesCount);
        await CreateAbsenceTypes(context, _absenceTypesCount);
        await context.SaveChangesAsync();

        await CreateEmploymentContracts(context, _employmentContractsCount);
        await CreateAbsences(context, _absencesCount);
        await CreateSalaries(context, _salariesCount);
        await context.SaveChangesAsync();
    }

    // ============== słownikowe połączone z employee ===========================

    public static async Task<MaritalStatus> CreateMaritalStatus(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var maritalStatus = new MaritalStatus
        {
            StatusName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.MaritalStatuses.Add(maritalStatus);
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return maritalStatus;
    }

    public static async Task CreateMaritalStatuses(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateMaritalStatus(context);
    }

    public static async Task<Address> CreateAddress(ApplicationDbContext context)
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
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return address;
    }

    public static async Task CreateAddresses(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateAddress(context);
    }

    public static async Task<EducationLevel> CreateEducationLevel(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var educationLevel = new EducationLevel
        {
            LevelName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.EducationLevels.Add(educationLevel);
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return educationLevel;
    }

    public static async Task CreateEducationLevels(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateEducationLevel(context);
    }

    public static async Task<Experience> CreateExperience(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var experience = new Experience
        {
            ExperienceName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.Experiences.Add(experience);
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return experience;
    }

    public static async Task CreateExperiences(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateExperience(context);
    }

    // ============== słownikowe połączone z innymi ===========================

    public static async Task<ContractType> CreateContractType(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var contractType = new ContractType
        {
            TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.ContractTypes.Add(contractType);
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return contractType;
    }

    public static async Task CreateContractTypes(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateContractType(context);
    }

    public static async Task<AbsenceType> CreateAbsenceType(ApplicationDbContext context)
    {
        var stringLength = Random.Next(3, 21);
        var absenceType = new AbsenceType
        {
            TypeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(stringLength))
        };

        context.AbsenceTypes.Add(absenceType);
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return absenceType;
    }

    public static async Task CreateAbsenceTypes(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateAbsenceType(context);
    }

    // ============== employee ===========================

    public static async Task<Employee> CreateEmployee(ApplicationDbContext context)
    {
        var maritalStatusIds = context.MaritalStatuses.Select(a => a.Id).ToList();
        var addressIds = context.Addresses.Select(a => a.Id).ToList();
        var educationLevelIds = context.EducationLevels.Select(a => a.Id).ToList();
        var experienceIds = context.Experiences.Select(a => a.Id).ToList();

        var employeeName = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(Random.Next(3, 16)));
        var employeeSurname = StringUtils.FirstCharacterUpRestDown(StringUtils.GenerateRandomString(Random.Next(3, 16)));
        var email = StringUtils.GenerateRandomString(Random.Next(3, 11)).ToLower() + "@example.com";

        var minDoB = DateTime.Now.AddYears(-60);
        var maxDoB = DateTime.Now.AddYears(-18);
        var randomDoB = minDoB.AddDays(Random.Next((maxDoB - minDoB).Days));

        var employee = new Employee
        {
            FullName = employeeName + " " + employeeSurname,
            Email = email,
            PhoneNumber = "+48" + Random.Next(10000000, 99999999),
            DateOfBirth = randomDoB,
            MaritalStatusId = maritalStatusIds[Random.Next(0, maritalStatusIds.Count)],
            AddressId = addressIds[Random.Next(0, addressIds.Count)],
            EducationLevelId = educationLevelIds[Random.Next(0, educationLevelIds.Count)],
            ExperienceId = experienceIds[Random.Next(0, experienceIds.Count)]
        };

        context.Employees.Add(employee);
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return employee;
    }

    public static async Task CreateEmployees(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateEmployee(context);
    }

    // ============== główne połączone z employee ===========================

    public static async Task<EmploymentContract> CreateEmploymentContract(ApplicationDbContext context)
    {
        var contractTypesIds = context.ContractTypes.Select(a => a.Id).ToList();
        var employeesIds = context.Employees.Select(a => a.Id).ToList();

        var needEndDate = Random.NextDouble() < 0.3; // 30% szansy na true

        var hourlyRate = (decimal)(Random.Next(1000, 5000) / 100.0);
        var monthlyRate = (decimal)(Random.Next(50000, 100000) / 100.0);
        var basicSalary = (decimal)(Random.Next(3000000, 8000000) / 100.0);

        var employmentContract = new EmploymentContract
        {
            ContractNumber = "CNT" + Random.Next(1000, 10000),
            StartDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-Random.Next(1, 13))),
            EndDate = needEndDate ? DateOnly.FromDateTime(DateTime.Now.AddMonths(Random.Next(1, 13))) : null,
            ContractTypeId = contractTypesIds[Random.Next(0, contractTypesIds.Count)],
            EmployeeId = employeesIds[Random.Next(0, employeesIds.Count)],
            HourlyRate = Math.Round(hourlyRate, 2),
            MonthlyRate = Math.Round(monthlyRate, 2),
            BasicSalary = Math.Round(basicSalary, 2),
        };

        context.EmploymentContracts.Add(employmentContract);
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return employmentContract;
    }

    public static async Task CreateEmploymentContracts(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateEmploymentContract(context);
    }

    public static async Task<Absence> CreateAbsence(ApplicationDbContext context)
    {
        var employeesIds = context.Employees.Select(a => a.Id).ToList();
        var absenceTypesIds = context.AbsenceTypes.Select(a => a.Id).ToList();

        var absence = new Absence
        {
            StartDate = DateTime.Now.AddDays(-Random.Next(1, 11)),
            EndDate = DateTime.Now.AddDays(Random.Next(1, 11)),
            AbsenceTypeId = absenceTypesIds[Random.Next(0, absenceTypesIds.Count)],
            EmployeeId = employeesIds[Random.Next(0, employeesIds.Count)],
        };

        context.Absences.Add(absence);
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return absence;
    }

    public static async Task CreateAbsences(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateAbsence(context);
    }

    public static async Task<Salary> CreateSalary(ApplicationDbContext context)
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
        if (!_groupSaveChanges)
            await context.SaveChangesAsync();

        return salary;
    }

    public static async Task CreateSalaries(ApplicationDbContext context, int? howMany = null)
    {
        howMany ??= Random.Next(1, 11);
        for (int i = 0; i < howMany; i++)
            await CreateSalary(context);
    }

    private static void SetCreationSize(SampleObjectsCreationSizeLevel? sizeLevel)
    {
        switch (sizeLevel)
        {
            case SampleObjectsCreationSizeLevel.Low:
                _maritalStatusesCount = 5;
                _addressesCount = 20;
                _educationLevelsCount = 5;
                _experiencesCount = 5;
                _employeesCount = 5;
                _contractTypesCount = 5;
                _absenceTypesCount = 5;
                _employmentContractsCount = 5;
                _absencesCount = 20;
                _salariesCount = 50;
                break;
            case SampleObjectsCreationSizeLevel.Medium:
                _maritalStatusesCount = 25;
                _addressesCount = 100;
                _educationLevelsCount = 25;
                _experiencesCount = 25;
                _employeesCount = 25;
                _contractTypesCount = 25;
                _absenceTypesCount = 25;
                _employmentContractsCount = 25;
                _absencesCount = 100;
                _salariesCount = 250;
                break;
            case SampleObjectsCreationSizeLevel.High:
                _maritalStatusesCount = 125;
                _addressesCount = 500;
                _educationLevelsCount = 125;
                _experiencesCount = 125;
                _employeesCount = 125;
                _contractTypesCount = 125;
                _absenceTypesCount = 125;
                _employmentContractsCount = 125;
                _absencesCount = 500;
                _salariesCount = 1250;
                break;
            case SampleObjectsCreationSizeLevel.Extreme:
                _maritalStatusesCount = 12500;
                _addressesCount = 50000;
                _educationLevelsCount = 12500;
                _experiencesCount = 12500;
                _employeesCount = 12500;
                _contractTypesCount = 12500;
                _absenceTypesCount = 12500;
                _employmentContractsCount = 12500;
                _absencesCount = 50000;
                _salariesCount = 125000;
                break;
            default:
                _maritalStatusesCount = 1;
                _addressesCount = 1;
                _educationLevelsCount = 1;
                _experiencesCount = 1;
                _employeesCount = 1;
                _contractTypesCount = 1;
                _absenceTypesCount = 1;
                _employmentContractsCount = 1;
                _absencesCount = 5;
                _salariesCount = 10;
                break;
        }
    }
}
