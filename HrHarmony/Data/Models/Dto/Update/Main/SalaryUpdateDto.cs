namespace HrHarmony.Data.Models.Dto.Update.Main;

public class SalaryUpdateDto : EntityDto<int>
{
    public decimal BasicSalary { get; set; }

    public decimal AdditionalSalary { get; set; }

    public decimal Bonuses { get; set; }

    public decimal Allowances { get; set; }

    public decimal ZUSContributions { get; set; }

    public decimal IncomeTax { get; set; }
}