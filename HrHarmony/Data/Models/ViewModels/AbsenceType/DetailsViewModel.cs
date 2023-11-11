namespace HrHarmony.Data.Models.ViewModels.AbsenceType;

public class DetailsViewModel
{
    public int Id { get; set; }
    public string TypeName { get; set; }

    public IEnumerable<Absence.DetailsViewModel> Absences { get; set; }
}