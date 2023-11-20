using System.ComponentModel.DataAnnotations;
using HrHarmony.Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.ViewModels.EmploymentContract;

public class UpdateViewModel : IEmploymentContractOptionFields
{
    [Display(Name = "Data rozpoczęcia")]
    public DateTime StartDate { get; set; }
    
    [Display(Name = "Data zakończenia")]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Typ umowy")]
    public int ContractTypeId { get; set; }

    public IEnumerable<SelectListItem> ContractTypes { get; set; } = new List<SelectListItem>();

    [Display(Name = "Pracownik")]
    public int EmployeeId { get; set; }

    public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();

    [Display(Name = "Stawka godzinowa")]
    public decimal HourlyRate { get; set; }

    [Display(Name = "Opłata miesięczna")]
    public decimal MonthlyRate { get; set; }

    [Display(Name = "Wynagrodzenie podstawowe")]
    public decimal BasicSalary { get; set; }
}