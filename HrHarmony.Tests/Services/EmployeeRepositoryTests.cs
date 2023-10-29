using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories.EntityRepository;
using HrHarmony.Tests.Configuration;

namespace HrHarmony.Tests.Services;

public class EmployeeRepositoryTests : HrHarmonyTestsBase<IRepository<Employee, int>>
{
    public EmployeeRepositoryTests(TestFixture fixture) : base(fixture)
    {

    }

    [Fact]
    public async Task Create_EmployeeTemporary_Test()
    {
        // Arrange
        var employee = new Employee
        {
            FullName = "Johny",
            Email = "Wick",
            PhoneNumber = "765986156",
            DateOfBirth = DateTime.Now,
            MaritalStatusId = 5,
            AddressId = 1,
            EducationLevelId = 1, // dodać tworzenie połączonych encji
            ExperienceId = 1
        };

        // Act
        //var createdEmployee = await Service.Create(employee);
        //var downloadedEmployee = await Service.GetById(createdEmployee.Id);
        var result = new Employee(); // zamiast tego nowy pobrany


        // Assert
        Assert.Equal(employee, result);
    }
}