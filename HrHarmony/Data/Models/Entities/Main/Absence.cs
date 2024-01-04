using HrHarmony.Data.Models.Entities.Dictionary;

namespace HrHarmony.Data.Models.Entities.Main;

public class Absence : Entity<int>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int AbsenceTypeId { get; set; }

    public AbsenceType AbsenceType { get; set; }

    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }
}