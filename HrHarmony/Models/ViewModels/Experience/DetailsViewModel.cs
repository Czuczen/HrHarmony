using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.ViewModels.Experience;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public string ExperienceDescription { get; set; }

    public IEnumerable<Employee.DetailsViewModel> Employees { get; set; }
}