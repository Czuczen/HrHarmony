using HrHarmony.Data.Models.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Dictionary;

public class AbsenceTypeDataSeeder : ISeeder
{
    public int Order => 6;

    private readonly List<string> _absenceTypes = new()
    {
        "Urlop wypoczynkowy", "Urlop bezpłatny", "Urlop okolicznościowy", "Urlop zdrowotny",
        "Urlop macierzyński", "Urlop ojcowski", "Urlop na żądanie", "Urlop naukowy",
        "Urlop szkoleniowy", "Urlop wychowawczy", "Urlop rehabilitacyjny", "Zwolnienie lekarskie"
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