using System.ComponentModel.DataAnnotations;
using HrHarmony.Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.ViewModels.EmploymentContract;

public class UpdateViewModel : IEmploymentContractOptionFields
{
    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Data rozpoczęcia")]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Data zakończenia")]
    [DataType(DataType.DateTime)]
    public DateTime? EndDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Typ umowy")]
    public int ContractTypeId { get; set; }

    public IEnumerable<SelectListItem> ContractTypes { get; set; } = new List<SelectListItem>();

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Pracownik")]
    public int EmployeeId { get; set; }

    public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();

    [Display(Name = "Stawka godzinowa")]
    [DataType(DataType.Currency)]
    //[Range(1, 1000, ErrorMessage = "Wartość musi być między 1 a 1 000")]
    public decimal HourlyRate { get; set; }

    [Display(Name = "Opłata miesięczna")]
    [DataType(DataType.Currency)]
    //[Range(1, 1000000, ErrorMessage = "Wartość musi być między 1 a 1 000 000")]
    public decimal MonthlyRate { get; set; }

    [Display(Name = "Wynagrodzenie podstawowe")]
    [DataType(DataType.Currency)]
    //[Range(1, 1000000, ErrorMessage = "Wartość musi być między 1 a 1 000 000")]
    public decimal BasicSalary { get; set; }
}