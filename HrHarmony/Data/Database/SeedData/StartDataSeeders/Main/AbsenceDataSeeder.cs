using HrHarmony.Data.Models.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Main;

public class AbsenceDataSeeder : ISeeder
{
    public int Order => 8;

    private readonly List<Absence> _absences = new()
    {
        new Absence
        {
            Id = 1,
            StartDate = new DateTime(2023, 1, 10),
            EndDate = new DateTime(2023, 1, 15),
            AbsenceTypeId = 1,
            EmployeeId = 1
        },
        new Absence
        {
            Id = 2,
            StartDate = new DateTime(2023, 2, 20),
            EndDate = new DateTime(2023, 2, 25),
            AbsenceTypeId = 2,
            EmployeeId = 2
        },
        new Absence
        {
            Id = 3,
            StartDate = new DateTime(2023, 3, 5),
            EndDate = new DateTime(2023, 3, 10),
            AbsenceTypeId = 3,
            EmployeeId = 3
        },
        new Absence
        {
            Id = 4,
            StartDate = new DateTime(2023, 4, 15),
            EndDate = new DateTime(2023, 4, 20),
            AbsenceTypeId = 4,
            EmployeeId = 4
        },
        new Absence
        {
            Id = 5,
            StartDate = new DateTime(2023, 5, 25),
            EndDate = new DateTime(2023, 5, 30),
            AbsenceTypeId = 5,
            EmployeeId = 5
        },
        new Absence
        {
            Id = 6,
            StartDate = new DateTime(2023, 6, 10),
            EndDate = new DateTime(2023, 6, 15),
            AbsenceTypeId = 6,
            EmployeeId = 1
        },
        new Absence
        {
            Id = 7,
            StartDate = new DateTime(2023, 9, 1),
            EndDate = new DateTime(2023, 9, 5),
            AbsenceTypeId = 7,
            EmployeeId = 2
        },
        new Absence
        {
            Id = 8,
            StartDate = new DateTime(2023, 10, 10),
            EndDate = new DateTime(2023, 10, 15),
            AbsenceTypeId = 8,
            EmployeeId = 3
        },
        new Absence
        {
            Id = 9,
            StartDate = new DateTime(2023, 11, 20),
            EndDate = new DateTime(2023, 11, 25),
            AbsenceTypeId = 9,
            EmployeeId = 4
        },
        new Absence
        {
            Id = 10,
            StartDate = new DateTime(2023, 12, 5),
            EndDate = new DateTime(2023, 12, 10),
            AbsenceTypeId = 10,
            EmployeeId = 5
        },
        new Absence
        {
            Id = 11,
            StartDate = new DateTime(2024, 1, 15),
            EndDate = new DateTime(2024, 1, 20),
            AbsenceTypeId = 11,
            EmployeeId = 1
        },
        new Absence
        {
            Id = 12,
            StartDate = new DateTime(2024, 2, 25),
            EndDate = new DateTime(2024, 2, 29),
            AbsenceTypeId = 12,
            EmployeeId = 2
        },
        new Absence
        {
            Id = 13,
            StartDate = new DateTime(2024, 3, 5),
            EndDate = new DateTime(2024, 3, 10),
            AbsenceTypeId = 1,
            EmployeeId = 3
        },
        new Absence
        {
            Id = 14,
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2024, 4, 20),
            AbsenceTypeId = 2,
            EmployeeId = 4
        },
        new Absence
        {
            Id = 15,
            StartDate = new DateTime(2024, 5, 25),
            EndDate = new DateTime(2024, 5, 30),
            AbsenceTypeId = 3,
            EmployeeId = 5
        }
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var absence in _absences)
            modelBuilder.Entity<Absence>().HasData(absence);
    }
}