using HrHarmony.Data.Models.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Dictionary;

public class MaritalStatusDataSeeder : ISeeder
{
    public int Order => 1;

    private readonly List<string> _statuses = new()
    {
        "Kawaler", "Panna", "Żonaty", "Mężatka", "Rozwiedziony", "Rozwiedziona", "Wdowiec", "Wdowa"
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var status in _statuses)
        {
            modelBuilder.Entity<MaritalStatus>().HasData(new MaritalStatus
            {
                Id = _statuses.IndexOf(status) + 1,
                StatusName = status
            });
        }
    }
}