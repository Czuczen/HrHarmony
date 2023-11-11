using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Experience;

public class CreateViewModel
{
    [Display(Name = "Opis doświadczenia")]
    public string ExperienceDescription { get; set; }

}