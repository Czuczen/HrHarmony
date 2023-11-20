using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Leave;

public class UpdateViewModel
{
    [Display(Name = "Data zakończenia")]
    public DateTime EndDate { get; set; }
}