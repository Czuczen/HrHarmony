using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.Experience;

public class CreateViewModel
{
    [Display(Name = "Opis doświadczenia")]
    public string ExperienceDescription { get; set; }

}