using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.Salary;

public class UpdateViewModel
{
    [Display(Name = "Wynagrodzenie podstawowe")]
    [DataType(DataType.Currency)]
    //[Range(1, 1000000, ErrorMessage = "Wartość musi być między 1 a 1 000 000")]
    public decimal BasicSalary { get; set; }

    [Display(Name = "Dodatkowe wynagrodzenie")]
    [DataType(DataType.Currency)]
    public decimal AdditionalSalary { get; set; }

    [Display(Name = "Bonusy")]
    [DataType(DataType.Currency)]
    public decimal Bonuses { get; set; }

    [Display(Name = "Dodatki")]
    [DataType(DataType.Currency)]
    public decimal Allowances { get; set; }

    [Display(Name = "Składki ZUS")]
    [DataType(DataType.Currency)]
    public decimal ZUSContributions { get; set; }

    [Display(Name = "Podatek dochodowy")]
    [DataType(DataType.Currency)]
    public decimal IncomeTax { get; set; }
}