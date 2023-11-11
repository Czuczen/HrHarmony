using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.ViewModels.ContractType;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public string TypeName { get; set; }

    public IEnumerable<EmploymentContract.DetailsViewModel> Contracts { get; set; }
}