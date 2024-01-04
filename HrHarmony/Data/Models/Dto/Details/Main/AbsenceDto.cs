using HrHarmony.Data.Models.Dto.Details.Dictionary;

namespace HrHarmony.Data.Models.Dto.Details.Main;

public class AbsenceDto : EntityDto<int>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int AbsenceTypeId { get; set; }

    public AbsenceTypeDto AbsenceType { get; set; }

    public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; }
}