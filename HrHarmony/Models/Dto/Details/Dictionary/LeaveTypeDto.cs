using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.Dto.Details.Dictionary;

public class LeaveTypeDto : EntityDto<int>
{
    public string TypeName { get; set; }

    public List<LeaveDto> Leaves { get; set; }
}