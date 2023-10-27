namespace HrHarmony.Models.Dto.Details.Main;

public class SalaryDto : EntityDto<int>
{
    public DateTime PaymentDate { get; set; }

    public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal AdditionalSalary { get; set; }

    public decimal Bonuses { get; set; }

    public decimal Allowances { get; set; }

    public decimal ZUSContributions { get; set; }

    public decimal IncomeTax { get; set; }
}