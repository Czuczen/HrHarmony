namespace HrHarmony.Data.Models.Dto.Update.Main;

public class LeaveUpdateDto : EntityDto<int>
{
    public DateTime EndDate { get; set; }
}