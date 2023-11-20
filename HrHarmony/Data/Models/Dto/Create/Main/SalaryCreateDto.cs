using HrHarmony.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Create.Main;

public class SalaryCreateDto
{
    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.DateTime, ErrorMessage = "Proszę podać poprawną datę.")]
    [MinimumCurrentDate(ErrorMessage = "Data musi być dzisiejsza lub późniejsza.")]
    public DateTime? PaymentDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? EmployeeId { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 1000000, ErrorMessage = "Wartość musi być między 1 a 1 000 000")]
    public decimal? BasicSalary { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 100000, ErrorMessage = "Wartość musi być między 1 a 100 000")]
    public decimal? AdditionalSalary { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 10000, ErrorMessage = "Wartość musi być między 1 a 10 000")]
    public decimal? Bonuses { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 5000, ErrorMessage = "Wartość musi być między 1 a 5 000")]
    public decimal? Allowances { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 5000, ErrorMessage = "Wartość musi być między 1 a 5 000")]
    public decimal? ZUSContributions { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 5000, ErrorMessage = "Wartość musi być między 1 a 5 000")]
    public decimal? IncomeTax { get; set; }
}