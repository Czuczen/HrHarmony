using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Experience;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Opis doświadczenia")]
    public string ExperienceDescription { get; set; }
}