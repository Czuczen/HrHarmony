using HrHarmony.Data.Models.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Dictionary;

public class ExperienceDataSeeder : ISeeder
{
    public int Order => 3;

    private readonly List<string> _experiences = new()
    {
        "Stażysta", "Młodszy", "Średniozaawansowany", "Starszy", "Ekspert"
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var experience in _experiences)
        {
            modelBuilder.Entity<Experience>().HasData(new Experience
            {
                Id = _experiences.IndexOf(experience) + 1,
                ExperienceDescription = experience
            });
        }
    }
}