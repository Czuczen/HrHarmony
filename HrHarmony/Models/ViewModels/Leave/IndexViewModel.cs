using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.Leave;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Data rozpoczęcia")]
    public DateTime StartDate { get; set; }
}