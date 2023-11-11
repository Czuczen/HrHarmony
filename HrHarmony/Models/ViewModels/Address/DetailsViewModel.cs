using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.ViewModels.Address;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }

    public IEnumerable<Employee.DetailsViewModel> Employees { get; set; }
}