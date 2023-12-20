using HrHarmony.Data.Models.Entities.Main;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Main;

public class AbsenceDataSeeder : ISeeder
{
    public int Order => 9;

    private readonly List<Absence> _absences = new()
    {
        new Absence
        {
            Id = 1,
            AbsenceDate = new DateTime(2023, 1, 5),
            AbsenceTypeId = 1,
            EmployeeId = 1
        },
        new Absence
        {
            Id = 2,
            AbsenceDate = new DateTime(2023, 2, 10),
            AbsenceTypeId = 2,
            EmployeeId = 2
        },
        new Absence
        {
            Id = 3,
            AbsenceDate = new DateTime(2023, 3, 15),
            AbsenceTypeId = 3,
            EmployeeId = 3
        },
        new Absence
        {
            Id = 4,
            AbsenceDate = new DateTime(2023, 4, 20),
            AbsenceTypeId = 4,
            EmployeeId = 4
        },
        new Absence
        { 
            Id = 5,
            AbsenceDate = new DateTime(2023, 5, 25),
            AbsenceTypeId = 1,
            EmployeeId = 5
        }
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var absence in _absences)
            modelBuilder.Entity<Absence>().HasData(absence);
    }
}