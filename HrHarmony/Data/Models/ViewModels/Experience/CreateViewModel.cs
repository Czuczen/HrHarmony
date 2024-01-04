using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Experience;

public class CreateViewModel
{
    [Display(Name = "Nazwa doświadczenia")]
    public string ExperienceName { get; set; }

}