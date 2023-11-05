using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.MaritalStatus;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nazwa statusu")]
    public string StatusName { get; set; }
}