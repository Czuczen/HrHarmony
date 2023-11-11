using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.LeaveType;

public class UpdateViewModel
{
    [Display(Name = "Nazwa typu")]
    public string TypeName { get; set; }
}