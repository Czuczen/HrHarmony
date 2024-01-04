namespace HrHarmony.Data.Models.ViewModels.Absence;

public class DetailsViewModel
{
    public int Id{ get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int AbsenceTypeId { get; set; }
    public AbsenceType.IndexViewModel AbsenceType { get; set; }

    public int EmployeeId { get; set; }
    public Employee.IndexViewModel Employee { get; set; }
}