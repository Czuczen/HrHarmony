namespace HrHarmony.Data.Models.ViewModels.Address;

public class DetailsViewModel
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }

    public IEnumerable<Employee.DetailsViewModel> Employees { get; set; }
}