using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.ContractType;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nazwa rodzaju")]
    public string TypeName { get; set; }
}