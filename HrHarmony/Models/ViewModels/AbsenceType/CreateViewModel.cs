using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.AbsenceType;

public class CreateViewModel
{
    [Display(Name = "Nazwa rodzaju")]
    public string TypeName { get; set; }

}