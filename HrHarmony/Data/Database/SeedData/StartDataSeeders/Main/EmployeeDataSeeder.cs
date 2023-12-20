using HrHarmony.Data.Models.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Main;

public class EmployeeDataSeeder : ISeeder
{
    public int Order => 8;

    private readonly List<Employee> _employees = new()
    {
        new Employee
        {
            Id = 1,
            FullName = "Janusz Kowalski",
            Email = "janusz.kowalski@example.com",
            PhoneNumber = "123456789",
            DateOfBirth = new DateTime(1990, 5, 15),
            MaritalStatusId = 1,
            AddressId = 1,
            EducationLevelId = 1,
            ExperienceId = 1,
        },
        new Employee
        { 
            Id = 2,
            FullName = "Anna Nowak",
            Email = "anna.nowak@example.com",
            PhoneNumber = "987654321",
            DateOfBirth = new DateTime(1992, 8, 25),
            MaritalStatusId = 2,
            AddressId = 2,
            EducationLevelId = 2,
            ExperienceId = 2,
        },
        new Employee
        {
            Id = 3,
            FullName = "Piotr Wiśniewski",
            Email = "piotr.wisniewski@example.com",
            PhoneNumber = "111222333",
            DateOfBirth = new DateTime(1988, 3, 12),
            MaritalStatusId = 3,
            AddressId = 3,
            EducationLevelId = 3,
            ExperienceId = 3,
        },
        new Employee
        {
            Id = 4,
            FullName = "Zofia Dąbrowska",
            Email = "zofia.dabrowska@example.com",
            PhoneNumber = "444555666",
            DateOfBirth = new DateTime(1991, 11, 8),
            MaritalStatusId = 4,
            AddressId = 4,
            EducationLevelId = 4,
            ExperienceId = 4,
        },
        new Employee
        {
            Id = 5,
            FullName = "Adam Kaczmarek",
            Email = "adam.kaczmarek@example.com",
            PhoneNumber = "777888999",
            DateOfBirth = new DateTime(1985, 7, 20),
            MaritalStatusId = 5,
            AddressId = 5,
            EducationLevelId = 5,
            ExperienceId = 5,
        }
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var employee in _employees)
            modelBuilder.Entity<Employee>().HasData(employee);
    }
}