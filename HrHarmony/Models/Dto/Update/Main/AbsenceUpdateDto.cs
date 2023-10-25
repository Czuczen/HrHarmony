namespace HrHarmony.Models.Dto.Update.Main;

public class AbsenceUpdateDto : EntityDto<int>
{
    public int AbsenceTypeId { get; set; }

    public int EmployeeId { get; set; }
}