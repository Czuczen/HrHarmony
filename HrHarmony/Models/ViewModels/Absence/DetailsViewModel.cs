namespace HrHarmony.Models.ViewModels.Absence;

public class DetailsViewModel : MainDetails
{
    public int Id{ get; set; }

    public DateTime AbsenceDate { get; set; }

    public int AbsenceTypeId { get; set; }
    public Dto.Details.Dictionary.AbsenceTypeDto AbsenceType { get; set; }

    public int EmployeeId { get; set; }
    public Dto.Details.Main.EmployeeDto Employee { get; set; }
}