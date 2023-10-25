using HrHarmony.Models.Entities.Dictionary;

namespace HrHarmony.Models.Entities.Main;

public class Absence : Entity<int>
{
    public DateTime AbsenceDate { get; set; }

    public int AbsenceTypeId { get; set; }

    public AbsenceType AbsenceType { get; set; }

    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }
}