using HrHarmony.Data.Models.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Main;

public class EmploymentContractDataSeeder : ISeeder
{
    public int Order => 10;

    private readonly List<EmploymentContract> _contracts = new()
    {
        new EmploymentContract
        {
            Id = 1,
            ContractNumber = "CNT1",
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 12, 31),
            ContractTypeId = 1,
            EmployeeId = 1,
            HourlyRate = 50.0m,
            MonthlyRate = 3000.0m,
            BasicSalary = 36000.0m
        },
        new EmploymentContract
        {
            Id = 2,
            ContractNumber = "CNT2",
            StartDate = new DateTime(2023, 3, 15),
            EndDate = null,
            ContractTypeId = 2,
            EmployeeId = 2,
            HourlyRate = 45.0m,
            MonthlyRate = 2800.0m,
            BasicSalary = 33600.0m
        },
        new EmploymentContract
        {
            Id = 3,
            ContractNumber = "CNT3",
            StartDate = new DateTime(2022, 5, 10),
            EndDate = new DateTime(2023, 5, 9),
            ContractTypeId = 3,
            EmployeeId = 3,
            HourlyRate = 55.0m,
            MonthlyRate = 3200.0m,
            BasicSalary = 38400.0m
        },
        new EmploymentContract
        {
            Id = 4,
            ContractNumber = "CNT4",
            StartDate = new DateTime(2023, 1, 1),
            EndDate = null,
            ContractTypeId = 4,
            EmployeeId = 4,
            HourlyRate = 60.0m,
            MonthlyRate = 3500.0m,
            BasicSalary = 42000.0m
        },
        new EmploymentContract
        {
            Id = 5,
            ContractNumber = "CNT5",
            StartDate = new DateTime(2023, 6, 20),
            EndDate = null,
            ContractTypeId = 1,
            EmployeeId = 5,
            HourlyRate = 50.0m,
            MonthlyRate = 3000.0m,
            BasicSalary = 36000.0m
        }
    };
        
    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var contract in _contracts)
            modelBuilder.Entity<EmploymentContract>().HasData(contract);
    }
}