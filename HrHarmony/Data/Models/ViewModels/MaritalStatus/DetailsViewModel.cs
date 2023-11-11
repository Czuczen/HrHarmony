namespace HrHarmony.Data.Models.ViewModels.MaritalStatus;

public class DetailsViewModel
{
    public int Id { get; set; }
    public string StatusName { get; set; }

    public IEnumerable<Employee.DetailsViewModel> Employees { get; set; }
}