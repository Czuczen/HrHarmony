using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.Experience;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Opis doświadczenia")]
    public string ExperienceDescription { get; set; }
}