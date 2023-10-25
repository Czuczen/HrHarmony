using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.ViewModels.Leave;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int LeaveTypeId { get; set; }
    public LeaveTypeDto LeaveType { get; set; }

    public int EmployeeId { get; set; }
    public EmployeeDto Employee { get; set; }
}