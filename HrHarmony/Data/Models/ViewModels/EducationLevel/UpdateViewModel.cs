using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.EducationLevel;

public class UpdateViewModel
{
    [Display(Name = "Nazwa poziomu")]
    public string LevelName { get; set; }
}