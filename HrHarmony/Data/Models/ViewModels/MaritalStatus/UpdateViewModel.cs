using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.MaritalStatus;

public class UpdateViewModel
{
    [Display(Name = "Nazwa statusu")]
    public string StatusName { get; set; }
}