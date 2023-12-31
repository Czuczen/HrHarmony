﻿using HrHarmony.Data.Database.SeedData;
using HrHarmony.Data.Models.Entities.Main;
using HrHarmony.Data.Repositories.Entity;
using HrHarmony.Tests.Configuration;

namespace HrHarmony.Tests.Repositories;

public class EmployeeRepositoryTests : HrHarmonyTestsBase<IRepository<Employee, int>>
{
    public EmployeeRepositoryTests(TestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Create_Employee_Test()
    {
        // Arrange
        var employee = await RandomDataSeeder.CreateEmployee(Ctx);

        // Act
        var downloadedEmployee = await Service.GetByIdAsync(employee.Id);

        // Assert
        Assert.Equal(employee, downloadedEmployee);
    }
}