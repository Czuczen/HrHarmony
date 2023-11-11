using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.LeaveType;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nazwa typu")]
    public string TypeName { get; set; }
}