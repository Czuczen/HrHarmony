using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.MaritalStatus;

public class UpdateViewModel
{
    [Display(Name = "Nazwa statusu")]
    public string StatusName { get; set; }
}