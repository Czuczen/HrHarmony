using HrHarmony.Models.Entities.Main;
using HrHarmony.Services;
using HrHarmony.Tests.Configuration;
using HrHarmony.Tests.Infrastructure;

namespace HrHarmony.Tests.Services
{
    public class EmployeeServiceTests : HrHarmonyTestsBase<IEmployeeService>
    {
        public EmployeeServiceTests(TestFixture fixture) : base(fixture)
        {

        }

        [Fact]
        public async Task Temporary_Test() // service method
        {
            // Arrange
            var employee = SeedData.CreateEmployee(Ctx);

            // Act
            var downloadedEmployee = new Employee(); // tymczasowo

            // Assert
            Assert.Equal(employee, downloadedEmployee);
        }
    }
}
