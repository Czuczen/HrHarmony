using HrHarmony.Data.Models.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Dictionary;

public class EducationLevelDataSeeder : ISeeder
{
    public int Order => 4;

    private readonly List<string> _levels = new()
    {
        "Podstawowe", "Średnie", "Wyższe", "Inżynierskie", "Magisterskie", "Doktorat"
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var educationLevel in _levels)
        {
            modelBuilder.Entity<EducationLevel>().HasData(new EducationLevel
            {
                Id = _levels.IndexOf(educationLevel) + 1,
                LevelName = educationLevel
            });
        }
    }
}