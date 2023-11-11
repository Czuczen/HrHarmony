using HrHarmony.Data.Models.Entities.Main;

namespace HrHarmony.Data.Models.Entities.Dictionary;

public class EducationLevel : Entity<int>
{
    public string LevelName { get; set; }

    public List<Employee> Employees { get; set; }
}