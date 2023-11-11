namespace HrHarmony.Data.Models.ViewModels.Experience;

public class DetailsViewModel
{
    public int Id { get; set; }
    public string ExperienceDescription { get; set; }

    public IEnumerable<Employee.DetailsViewModel> Employees { get; set; }
}