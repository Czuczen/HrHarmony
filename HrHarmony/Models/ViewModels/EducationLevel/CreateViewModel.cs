using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.EducationLevel;

public class CreateViewModel
{
    [Display(Name = "Nazwa poziomu")]
    public string LevelName { get; set; }

}