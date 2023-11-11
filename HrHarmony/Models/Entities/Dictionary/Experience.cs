using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.Entities.Dictionary;

public class Experience : Entity<int>
{
    public string ExperienceDescription { get; set; }

    public List<Employee> Employees { get; set; }
}