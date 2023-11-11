using HrHarmony.Data.Models.Dto.Details.Dictionary;

namespace HrHarmony.Data.Models.Dto.Details.Main;

public class LeaveDto : EntityDto<int>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int LeaveTypeId { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; }
}