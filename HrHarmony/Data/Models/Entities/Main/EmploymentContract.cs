using System.ComponentModel.DataAnnotations.Schema;
using HrHarmony.Data.Models.Entities.Dictionary;

namespace HrHarmony.Data.Models.Entities.Main;

public class EmploymentContract : Entity<int>
{
    public string ContractNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

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