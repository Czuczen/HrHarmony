namespace HrHarmony.Models.Dto.Update.Main;

public class LeaveUpdateDto : EntityDto<int>
{
    public DateTime EndDate { get; set; }
}