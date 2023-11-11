using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.Dto.Details.Dictionary;

public class ExperienceDto : EntityDto<int>
{
    public string ExperienceDescription { get; set; }

    public List<EmployeeDto> Employees { get; set; }
}