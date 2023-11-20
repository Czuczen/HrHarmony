using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Salary;

public class UpdateViewModel
{
    [Display(Name = "Wynagrodzenie podstawowe")]
    public decimal BasicSalary { get; set; }

    [Display(Name = "Dodatkowe wynagrodzenie")]
    public decimal AdditionalSalary { get; set; }

    [Display(Name = "Bonusy")]
    public decimal Bonuses { get; set; }

    [Display(Name = "Dodatki")]
    public decimal Allowances { get; set; }

    [Display(Name = "Składki ZUS")]
    public decimal ZUSContributions { get; set; }

    [Display(Name = "Podatek dochodowy")]
    public decimal IncomeTax { get; set; }
}