using System.ComponentModel.DataAnnotations.Schema;

namespace HrHarmony.Models.Entities.Main;

public class Salary : Entity<int>
{
    public DateTime PaymentDate { get; set; }

    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal BasicSalary { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal AdditionalSalary { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Bonuses { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Allowances { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal ZUSContributions { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal IncomeTax { get; set; }
}