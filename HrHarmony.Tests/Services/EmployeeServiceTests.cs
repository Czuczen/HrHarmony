using HrHarmony.Data.Database.SeedData;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Services;
using HrHarmony.Tests.Configuration;

namespace HrHarmony.Tests.Services;

public class EmployeeServiceTests : HrHarmonyTestsBase<IEmployeeService>
{
    public EmployeeServiceTests(TestFixture fixture) : base(fixture)
    {
        RandomDataSeeder.GroupSaveChanges = false;
    }

    [Fact]
    public async Task Temporary_Test() // service method
    {
        // Arrange
        var employee = RandomDataSeeder.CreateEmployee(Ctx);

        // Act
        var downloadedEmployee = new Employee(); // tymczasowo

        // Assert
        Assert.Equal(employee, downloadedEmployee);
    }
}