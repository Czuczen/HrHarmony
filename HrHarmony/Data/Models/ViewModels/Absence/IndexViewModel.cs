using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Absence;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Data absencji")]
    public DateTime AbsenceDate { get; set; }
}