using HrHarmony.Data.Models.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Dictionary;

public class ContractTypeDataSeeder : ISeeder
{
    public int Order => 4;

    private readonly List<string> _types = new()
    {
        "Umowa o pracę", "Umowa zlecenie", "Umowa o dzieło", "B2B"
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var contractType in _types)
        {
            modelBuilder.Entity<ContractType>().HasData(new ContractType
            {
                Id = _types.IndexOf(contractType) + 1,
                TypeName = contractType
            });
        }
    }
}