using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Absence;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Data rozpoczęcia")]
    public DateTime StartDate { get; set; }

    [Display(Name = "Data zakończenia")]
    public DateTime EndDate { get; set; }
}