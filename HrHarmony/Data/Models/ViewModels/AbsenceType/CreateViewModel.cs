using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.AbsenceType;

public class CreateViewModel
{
    [Display(Name = "Nazwa rodzaju")]
    public string TypeName { get; set; }

}