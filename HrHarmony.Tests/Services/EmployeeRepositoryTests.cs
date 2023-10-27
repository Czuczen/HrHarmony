using CryptoProspector.Tests.Configuration;
using HrHarmony.Models.Dto.Create.Main;
using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Dto.Update.Main;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Repositories;

namespace HrHarmony.Tests.Services;

public class EmployeeRepositoryTests : CryptoProspectorTestsBase<IRepository<Employee, int, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto>>
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
        await Service.Create(employee); // brak id
        var result = new EmployeeCreateDto(); // zamiast tego nowy pobrany


        // Assert
        Assert.Equal(employee, result);
    }
}