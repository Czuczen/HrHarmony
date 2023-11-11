using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.MaritalStatus;

public class CreateViewModel
{
    [Display(Name = "Nazwa statusu")]
    public string StatusName { get; set; }

}