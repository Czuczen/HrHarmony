using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.Dto.Details.Dictionary;

public class MaritalStatusDto : EntityDto<int>
{
    public string StatusName { get; set; }

    public List<EmployeeDto> Employees { get; set; }
}