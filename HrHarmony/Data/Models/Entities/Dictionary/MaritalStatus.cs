using HrHarmony.Data.Models.Entities.Main;

namespace HrHarmony.Data.Models.Entities.Dictionary;

public class MaritalStatus : Entity<int>
{
    public string StatusName { get; set; }

    public List<Employee> Employees { get; set; }
}