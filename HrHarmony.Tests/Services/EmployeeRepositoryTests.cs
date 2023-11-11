using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Data.Repositories.Entity;
using HrHarmony.Tests.Configuration;
using HrHarmony.Tests.Infrastructure;

namespace HrHarmony.Tests.Services;

public class EmployeeRepositoryTests : HrHarmonyTestsBase<IRepository<Employee, int>>
{
    public EmployeeRepositoryTests(TestFixture fixture) : base(fixture)
    {

    }

    [Fact]
    public async Task Create_Employee_Test()
    {
        // Arrange
        var employee = SeedData.CreateEmployee(Ctx);

        // Act
        var downloadedEmployee = await Service.GetByIdAsync(employee.Id);
        
        // Assert
        Assert.Equal(employee, downloadedEmployee);
    }
}