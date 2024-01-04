using HrHarmony.Data.Models.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Main;

public class SalaryDataSeeder : ISeeder
{
    public int Order => 10;

    private readonly List<Salary> _salaries = new()
    {
        new Salary
        {
            Id = 1,
            PaymentDate = new DateTime(2023, 1, 1),
            EmployeeId = 1,
            BasicSalary = 4000.0m,
            AdditionalSalary = 500.0m,
            Bonuses = 1000.0m,
            Allowances = 200.0m,
            ZUSContributions = 500.0m,
            IncomeTax = 1000.0m
        },
        new Salary
        {
            Id = 2,
            PaymentDate = new DateTime(2023, 1, 1),
            EmployeeId = 2,
            BasicSalary = 4500.0m,
            AdditionalSalary = 600.0m,
            Bonuses = 1200.0m,
            Allowances = 250.0m,
            ZUSContributions = 550.0m,
            IncomeTax = 1100.0m
        },
        new Salary
        {
            Id = 3,
            PaymentDate = new DateTime(2023, 1, 1),
            EmployeeId = 3,
            BasicSalary = 4200.0m,
            AdditionalSalary = 550.0m,
            Bonuses = 1100.0m,
            Allowances = 225.0m,
            ZUSContributions = 525.0m,
            IncomeTax = 1050.0m
        },
        new Salary
        {
            Id = 4,
            PaymentDate = new DateTime(2023, 1, 1),
            EmployeeId = 4,
            BasicSalary = 4300.0m,
            AdditionalSalary = 575.0m,
            Bonuses = 1150.0m,
            Allowances = 235.0m,
            ZUSContributions = 535.0m,
            IncomeTax = 1075.0m
        },
        new Salary
        {
            Id = 5,
            PaymentDate = new DateTime(2023, 1, 1),
            EmployeeId = 5,
            BasicSalary = 4400.0m,
            AdditionalSalary = 600.0m,
            Bonuses = 1200.0m,
            Allowances = 250.0m,
            ZUSContributions = 550.0m,
            IncomeTax = 1100.0m
        },
        new Salary
        {
            Id = 6,
            PaymentDate = new DateTime(2023, 2, 1),
            EmployeeId = 1,
            BasicSalary = 4500.0m,
            AdditionalSalary = 625.0m,
            Bonuses = 1250.0m,
            Allowances = 260.0m,
            ZUSContributions = 560.0m,
            IncomeTax = 1125.0m
        },
        new Salary
        {
            Id = 7,
            PaymentDate = new DateTime(2023, 2, 1),
            EmployeeId = 2,
            BasicSalary = 4600.0m,
            AdditionalSalary = 625.0m,
            Bonuses = 1300.0m,
            Allowances = 275.0m,
            ZUSContributions = 575.0m,
            IncomeTax = 1150.0m
        },
        new Salary
        {
            Id = 8,
            PaymentDate = new DateTime(2023, 2, 1),
            EmployeeId = 3,
            BasicSalary = 4700.0m,
            AdditionalSalary = 650.0m,
            Bonuses = 1350.0m,
            Allowances = 285.0m,
            ZUSContributions = 585.0m,
            IncomeTax = 1175.0m
        },
        new Salary
        {
            Id = 9,
            PaymentDate = new DateTime(2023, 2, 1),
            EmployeeId = 4,
            BasicSalary = 4800.0m,
            AdditionalSalary = 675.0m,
            Bonuses = 1400.0m,
            Allowances = 300.0m,
            ZUSContributions = 600.0m,
            IncomeTax = 1200.0m
        },
        new Salary
        {
            Id = 10,
            PaymentDate = new DateTime(2023, 2, 1),
            EmployeeId = 5,
            BasicSalary = 4900.0m,
            AdditionalSalary = 700.0m,
            Bonuses = 1450.0m,
            Allowances = 310.0m,
            ZUSContributions = 610.0m,
            IncomeTax = 1225.0m
        },
        new Salary
        {
            Id = 11,
            PaymentDate = new DateTime(2023, 3, 1),
            EmployeeId = 1,
            BasicSalary = 5000.0m,
            AdditionalSalary = 725.0m,
            Bonuses = 1500.0m,
            Allowances = 325.0m,
            ZUSContributions = 625.0m,
            IncomeTax = 1250.0m
        },
        new Salary
        {
            Id = 12,
            PaymentDate = new DateTime(2023, 3, 1),
            EmployeeId = 2,
            BasicSalary = 5100.0m,
            AdditionalSalary = 750.0m,
            Bonuses = 1550.0m,
            Allowances = 335.0m,
            ZUSContributions = 635.0m,
            IncomeTax = 1275.0m
        },
        new Salary
        {
            Id = 13,
            PaymentDate = new DateTime(2023, 3, 1),
            EmployeeId = 3,
            BasicSalary = 5200.0m,
            AdditionalSalary = 775.0m,
            Bonuses = 1600.0m,
            Allowances = 350.0m,
            ZUSContributions = 650.0m,
            IncomeTax = 1300.0m
        },
        new Salary
        {
            Id = 14,
            PaymentDate = new DateTime(2023, 3, 1),
            EmployeeId = 4,
            BasicSalary = 5300.0m,
            AdditionalSalary = 800.0m,
            Bonuses = 1650.0m,
            Allowances = 360.0m,
            ZUSContributions = 660.0m,
            IncomeTax = 1325.0m
        },
        new Salary
        {
            Id = 15,
            PaymentDate = new DateTime(2023, 3, 1),
            EmployeeId = 5,
            BasicSalary = 5400.0m,
            AdditionalSalary = 825.0m,
            Bonuses = 1700.0m,
            Allowances = 375.0m,
            ZUSContributions = 675.0m,
            IncomeTax = 1350.0m
        },
        new Salary
        {
            Id = 16,
            PaymentDate = new DateTime(2023, 4, 1),
            EmployeeId = 1,
            BasicSalary = 5500.0m,
            AdditionalSalary = 850.0m,
            Bonuses = 1750.0m,
            Allowances = 385.0m,
            ZUSContributions = 685.0m,
            IncomeTax = 1375.0m
        },
        new Salary
        {
            Id = 17,
            PaymentDate = new DateTime(2023, 4, 1),
            EmployeeId = 2,
            BasicSalary = 5600.0m,
            AdditionalSalary = 875.0m,
            Bonuses = 1800.0m,
            Allowances = 400.0m,
            ZUSContributions = 700.0m,
            IncomeTax = 1400.0m
        },
        new Salary
        {
            Id = 18,
            PaymentDate = new DateTime(2023, 4, 1),
            EmployeeId = 3,
            BasicSalary = 5700.0m,
            AdditionalSalary = 900.0m,
            Bonuses = 1850.0m,
            Allowances = 410.0m,
            ZUSContributions = 710.0m,
            IncomeTax = 1425.0m
        },
        new Salary
        {
            Id = 19,
            PaymentDate = new DateTime(2023, 4, 1),
            EmployeeId = 4,
            BasicSalary = 5800.0m,
            AdditionalSalary = 925.0m,
            Bonuses = 1900.0m,
            Allowances = 425.0m,
            ZUSContributions = 725.0m,
            IncomeTax = 1450.0m
        },
        new Salary
        {
            Id = 20,
            PaymentDate = new DateTime(2023, 4, 1),
            EmployeeId = 5,
            BasicSalary = 5900.0m,
            AdditionalSalary = 950.0m,
            Bonuses = 1950.0m,
            Allowances = 435.0m,
            ZUSContributions = 735.0m,
            IncomeTax = 1475.0m
        }
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var salary in _salaries)
            modelBuilder.Entity<Salary>().HasData(salary);
    }
}