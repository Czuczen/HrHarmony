namespace HrHarmony.Models.ViewModels.Leave;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int LeaveTypeId { get; set; }
    public LeaveType.IndexViewModel LeaveType { get; set; }

    public int EmployeeId { get; set; }
    public Employee.IndexViewModel Employee { get; set; }
}