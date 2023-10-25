using HrHarmony.Models.Dto.Details.Dictionary;

namespace HrHarmony.Models.Dto.Details.Main;

public class AbsenceDto : EntityDto<int>
{
    public DateTime AbsenceDate { get; set; }

    public int AbsenceTypeId { get; set; }

    public AbsenceTypeDto AbsenceType { get; set; }

    public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; }
}