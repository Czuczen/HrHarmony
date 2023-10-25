using HrHarmony.Models.Dto.Details.Dictionary;
using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.ViewModels.EmploymentContract;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public string ContractNumber { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int ContractTypeId { get; set; }
    public ContractTypeDto ContractType { get; set; }

    public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; }

    public decimal HourlyRate { get; set; }
    public decimal MonthlyRate { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal AdditionalSalary { get; set; }
    public decimal Bonuses { get; set; }
    public decimal Allowances { get; set; }

    public decimal ZUSContributions { get; set; }
    public decimal IncomeTax { get; set; }
}