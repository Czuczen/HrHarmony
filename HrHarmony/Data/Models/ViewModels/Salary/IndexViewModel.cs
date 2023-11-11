using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Salary;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Data płatności")]
    public DateTime PaymentDate { get; set; }
}