using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.LeaveType;

public class UpdateViewModel
{
    [Display(Name = "Nazwa typu")]
    public string TypeName { get; set; }
}