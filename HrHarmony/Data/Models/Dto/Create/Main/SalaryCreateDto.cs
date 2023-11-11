namespace HrHarmony.Data.Models.Dto.Create.Main;

public class SalaryCreateDto
{
    public DateTime PaymentDate { get; set; }

    public int EmployeeId { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal AdditionalSalary { get; set; }

    public decimal Bonuses { get; set; }

    public decimal Allowances { get; set; }

    public decimal ZUSContributions { get; set; }

    public decimal IncomeTax { get; set; }
}