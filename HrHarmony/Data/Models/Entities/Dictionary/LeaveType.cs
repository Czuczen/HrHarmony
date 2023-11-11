using HrHarmony.Data.Models.Entities.Main;

namespace HrHarmony.Data.Models.Entities.Dictionary;

public class LeaveType : Entity<int>
{
    public string TypeName { get; set; }

    public List<Leave> Leaves { get; set; }
}