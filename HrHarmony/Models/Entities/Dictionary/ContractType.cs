using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.Entities.Dictionary;

public class ContractType : Entity<int>
{
    public string TypeName { get; set; }

    public List<EmploymentContract> Contracts { get; set; }
}