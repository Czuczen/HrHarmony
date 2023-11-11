namespace HrHarmony.Data.Models.ViewModels.ContractType;

public class DetailsViewModel
{
    public int Id { get; set; }
    public string TypeName { get; set; }

    public IEnumerable<EmploymentContract.DetailsViewModel> Contracts { get; set; }
}