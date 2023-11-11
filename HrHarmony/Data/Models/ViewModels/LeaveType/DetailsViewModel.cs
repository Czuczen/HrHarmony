namespace HrHarmony.Data.Models.ViewModels.LeaveType;

public class DetailsViewModel
{
    public int Id { get; set; }
    public string TypeName { get; set; }

    public IEnumerable<Leave.DetailsViewModel> Leaves { get; set; }
}