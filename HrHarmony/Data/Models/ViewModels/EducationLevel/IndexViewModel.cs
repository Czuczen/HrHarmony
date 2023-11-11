using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.EducationLevel;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nazwa poziomu")]
    public string LevelName { get; set; }
}