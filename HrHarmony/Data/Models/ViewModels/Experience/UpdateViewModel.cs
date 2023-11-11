using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Experience;

public class UpdateViewModel
{
    [Display(Name = "Opis doświadczenia")]
    public string ExperienceDescription { get; set; }
}