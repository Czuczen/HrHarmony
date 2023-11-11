using HrHarmony.Models.Entities.Main;

namespace HrHarmony.Models.Entities.Dictionary;

public class AbsenceType : Entity<int>
{
    public string TypeName { get; set; }

    public List<Absence> Absences { get; set; }
}