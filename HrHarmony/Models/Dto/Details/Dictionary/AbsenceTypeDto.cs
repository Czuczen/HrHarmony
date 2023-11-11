using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.Dto.Details.Dictionary;

public class AbsenceTypeDto : EntityDto<int>
{
    public string TypeName { get; set; }

    public List<AbsenceDto> Absences { get; set; }
}