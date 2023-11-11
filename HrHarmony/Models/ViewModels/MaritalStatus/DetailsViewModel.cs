namespace HrHarmony.Models.ViewModels.MaritalStatus;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public string StatusName { get; set; }

    public IEnumerable<Employee.DetailsViewModel> Employees { get; set; }
}