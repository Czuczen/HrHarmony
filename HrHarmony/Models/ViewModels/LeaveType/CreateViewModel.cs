using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.LeaveType;

public class CreateViewModel
{
    [Display(Name = "Nazwa typu")]
    public string TypeName { get; set; }

}