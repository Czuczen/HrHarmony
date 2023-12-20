using HrHarmony.Data.Models.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Dictionary;

public class AbsenceTypeDataSeeder : ISeeder
{
    public int Order => 7;

    private readonly List<string> _absenceTypes = new()
    {
        "Urlop wypoczynkowy", "Chorobowe", "Urlop bezpłatny", "Opieka nad dzieckiem"
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var absenceType in _absenceTypes)
        {
            modelBuilder.Entity<AbsenceType>().HasData(new AbsenceType
            {
                Id = _absenceTypes.IndexOf(absenceType) + 1,
                TypeName = absenceType
            });
        }
    }
}