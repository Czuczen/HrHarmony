using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.Dto.Details.Dictionary;

public class AddressDto : EntityDto<int>
{
    public string Street { get; set; }

    public string City { get; set; }

    public string PostalCode { get; set; }

    public List<EmployeeDto> Employees { get; set; }
}