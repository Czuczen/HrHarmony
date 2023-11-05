using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.AbsenceType;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nazwa rodzaju")]
    public string TypeName { get; set; }
}