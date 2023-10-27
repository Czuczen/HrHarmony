using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;
using HrHarmony.Tests.Configuration;

namespace HrHarmony.Tests.Services;

public class EmployeeRepositoryTests : HrHarmonyTestsBase<IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto>>
{
    public EmployeeRepositoryTests(TestFixture fixture) : base(fixture)
    {

    }

    [Fact]
    public async Task Create_Employee_Test()
    {
        // Arrange
        var employee = new EmployeeCreateDto
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
        //var createdEmployee = await Service.Create(employee); // będzie trzeba coś pokombinować z mapowaniem
        //var downloadedEmployee = await Service.GetById(createdEmployee.Id);
        var result = new EmployeeCreateDto(); // zamiast tego nowy pobrany


        // Assert
        Assert.Equal(employee, result);
    }
}