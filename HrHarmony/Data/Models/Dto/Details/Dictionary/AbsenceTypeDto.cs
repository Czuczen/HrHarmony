using HrHarmony.Data.Models.Dto.Details.Main;

namespace HrHarmony.Data.Models.Dto.Details.Dictionary;

public class AbsenceTypeDto : EntityDto<int>
{
    public string TypeName { get; set; }

    public List<AbsenceDto> Absences { get; set; }
}