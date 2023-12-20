using HrHarmony.Data.Models.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Dictionary;

public class LeaveTypeDataSeeder : ISeeder
{
    public int Order => 2;

    private readonly List<string> _types = new()
    {
        "Urlop wypoczynkowy", "Urlop bezpłatny", "Urlop okolicznościowy", "Urlop zdrowotny", 
        "Urlop macierzyński", "Urlop ojcowski", "Urlop na żądanie", "Urlop naukowy", 
        "Urlop szkoleniowy", "Urlop wychowawczy", "Urlop rehabilitacyjny"
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var leaveType in _types)
        {
            modelBuilder.Entity<LeaveType>().HasData(new LeaveType
            {
                Id = _types.IndexOf(leaveType) + 1,
                TypeName = leaveType
            });
        }
    }
}