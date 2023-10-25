namespace HrHarmony.Models.Dto.Create.Main;

public class LeaveCreateDto
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int LeaveTypeId { get; set; }

    public int EmployeeId { get; set; }
}