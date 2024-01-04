using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Experience;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nazwa doświadczenia")]
    public string ExperienceName { get; set; }
}