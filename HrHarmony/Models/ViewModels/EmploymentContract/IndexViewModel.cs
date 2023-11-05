using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.EmploymentContract;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Numer umowy")]
    public string ContractNumber { get; set; }
}