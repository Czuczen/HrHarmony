using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Create.Main;

public class EmploymentContractCreateDto
{
    [Required(ErrorMessage = "Pole jest wymagane.")]
    public string ContractNumber { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Date, ErrorMessage = "Proszę podać poprawną datę.")]
    public DateOnly? ContractSigningDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Date, ErrorMessage = "Proszę podać poprawną datę.")]
    public DateOnly? StartDate { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Proszę podać poprawną datę.")]
    public DateOnly? EndDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? ContractTypeId { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? EmployeeId { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 1000, ErrorMessage = "Wartość musi być między 1 a 1 000")]
    public decimal? HourlyRate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 1000000, ErrorMessage = "Wartość musi być między 1 a 1 000 000")]
    public decimal? MonthlyRate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Currency, ErrorMessage = "Proszę podać poprawną wartość.")]
    [Range(1, 1000000, ErrorMessage = "Wartość musi być między 1 a 1 000 000")]
    public decimal? BasicSalary { get; set; }
}