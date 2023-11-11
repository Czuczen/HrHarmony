namespace HrHarmony.Data.Models.Dto.Create.Main;

public class AbsenceCreateDto
{
    public DateTime AbsenceDate { get; set; }

    public int AbsenceTypeId { get; set; }

    public int EmployeeId { get; set; }
}