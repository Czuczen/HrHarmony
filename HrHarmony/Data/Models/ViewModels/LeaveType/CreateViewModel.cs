using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.LeaveType;

public class CreateViewModel
{
    [Display(Name = "Nazwa typu")]
    public string TypeName { get; set; }

}