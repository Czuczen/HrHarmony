using HrHarmony.Data.Models.Entities.Main;

namespace HrHarmony.Data.Models.Entities.Dictionary;

public class Address : Entity<int>
{
    public string Street { get; set; }

    public string City { get; set; }

    public string PostalCode { get; set; }

    public List<Employee> Employees { get; set; }
}