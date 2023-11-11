using HrHarmony.Data.Models.Dto.Details.Main;

namespace HrHarmony.Data.Models.Dto.Details.Dictionary;

public class LeaveTypeDto : EntityDto<int>
{
    public string TypeName { get; set; }

    public List<LeaveDto> Leaves { get; set; }
}