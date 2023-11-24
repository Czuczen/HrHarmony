using System.ComponentModel.DataAnnotations;
using HrHarmony.Data.Models.Interfaces.SelectOptions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.ViewModels.Salary;

public class CreateViewModel : ILoadEmployeeOptions
{
    [Display(Name = "Data płatności")]
    public DateTime? PaymentDate { get; set; }

    [Display(Name = "Pracownik")]
    public int EmployeeId { get; set; }

    public string EmployeeText { get; set; }

    public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();

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