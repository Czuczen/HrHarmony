using HrHarmony.Data.Models.Dto.Details.Main;

namespace HrHarmony.Data.Models.Dto.Details.Dictionary;

public class ExperienceDto : EntityDto<int>
{
    public string ExperienceDescription { get; set; }

    public List<EmployeeDto> Employees { get; set; }
}