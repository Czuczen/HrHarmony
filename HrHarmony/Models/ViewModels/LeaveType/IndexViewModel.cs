using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.LeaveType;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nazwa typu")]
    public string TypeName { get; set; }
}