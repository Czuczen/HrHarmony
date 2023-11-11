using HrHarmony.Data.Models.Dto.Details.Main;

namespace HrHarmony.Data.Models.Dto.Details.Dictionary;

public class EducationLevelDto : EntityDto<int>
{
    public string LevelName { get; set; }

    public List<EmployeeDto> Employees { get; set; }
}