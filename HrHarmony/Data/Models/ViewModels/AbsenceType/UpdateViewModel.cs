using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.AbsenceType;

public class UpdateViewModel
{
    [Display(Name = "Nazwa rodzaju")]
    public string TypeName { get; set; }
}