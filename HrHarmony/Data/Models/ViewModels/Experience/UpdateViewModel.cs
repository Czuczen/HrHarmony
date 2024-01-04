using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Experience;

public class UpdateViewModel
{
    [Display(Name = "Nazwa doświadczenia")]
    public string ExperienceName { get; set; }
}