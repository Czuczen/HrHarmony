using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.ViewModels.EducationLevel;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public string LevelName { get; set; }

    public IEnumerable<Employee.DetailsViewModel> Employees { get; set; }
}