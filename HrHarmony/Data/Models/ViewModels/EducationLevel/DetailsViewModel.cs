namespace HrHarmony.Data.Models.ViewModels.EducationLevel;

public class DetailsViewModel
{
    public int Id { get; set; }
    public string LevelName { get; set; }

    public IEnumerable<Employee.DetailsViewModel> Employees { get; set; }
}