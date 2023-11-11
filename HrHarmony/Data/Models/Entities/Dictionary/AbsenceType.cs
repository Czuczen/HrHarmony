using HrHarmony.Data.Models.Entities.Main;

namespace HrHarmony.Data.Models.Entities.Dictionary;

public class AbsenceType : Entity<int>
{
    public string TypeName { get; set; }

    public List<Absence> Absences { get; set; }
}