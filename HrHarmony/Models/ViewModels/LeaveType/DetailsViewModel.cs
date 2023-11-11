using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.ViewModels.LeaveType;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public string TypeName { get; set; }

    public IEnumerable<Leave.DetailsViewModel> Leaves { get; set; }
}