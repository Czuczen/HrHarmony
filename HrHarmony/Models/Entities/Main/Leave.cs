using HrHarmony.Models.Entities.Dictionary;

namespace HrHarmony.Models.Entities.Main;

public class Leave : Entity<int>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int LeaveTypeId { get; set; }

    public LeaveType LeaveType { get; set; }

    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }
}