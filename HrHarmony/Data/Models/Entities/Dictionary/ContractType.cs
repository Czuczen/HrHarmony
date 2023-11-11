using HrHarmony.Data.Models.Entities.Main;

namespace HrHarmony.Data.Models.Entities.Dictionary;

public class ContractType : Entity<int>
{
    public string TypeName { get; set; }

    public List<EmploymentContract> Contracts { get; set; }
}