using HrHarmony.Data.Models.Entities.Main;

namespace HrHarmony.Data.Models.Entities.Dictionary;

public class Experience : Entity<int>
{
    public string ExperienceDescription { get; set; }

    public List<Employee> Employees { get; set; }
}