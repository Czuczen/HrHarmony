using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.Entities.Dictionary;

public class MaritalStatus : Entity<int>
{
    public string StatusName { get; set; }

    public List<Employee> Employees { get; set; }
}