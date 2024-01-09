using System.ComponentModel.DataAnnotations.Schema;
using HrHarmony.Data.Models.Entities.Dictionary;

namespace HrHarmony.Data.Models.Entities.Main;

public class EmploymentContract : Entity<int>
{
    public string ContractNumber { get; set; }

    public DateOnly ContractSigningDate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int ContractTypeId { get; set; }

    public ContractType ContractType { get; set; }

    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal HourlyRate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MonthlyRate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal BasicSalary { get; set; }
}