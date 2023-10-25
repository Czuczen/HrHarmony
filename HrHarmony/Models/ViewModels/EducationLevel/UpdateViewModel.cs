using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.EducationLevel;

public class UpdateViewModel
{
    [Display(Name = "Nazwa poziomu")]
    public string LevelName { get; set; }
}