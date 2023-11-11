using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.Dto.Details.Dictionary;

public class EducationLevelDto : EntityDto<int>
{
    public string LevelName { get; set; }

    public List<EmployeeDto> Employees { get; set; }
}