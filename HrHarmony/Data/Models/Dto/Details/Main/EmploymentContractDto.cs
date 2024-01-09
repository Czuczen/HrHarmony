using HrHarmony.Data.Models.Dto.Details.Dictionary;

namespace HrHarmony.Data.Models.Dto.Details.Main;

public class EmploymentContractDto : EntityDto<int>
{
    public string ContractNumber { get; set; }

    public DateOnly ContractSigningDate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int ContractTypeId { get; set; }

    public ContractTypeDto ContractType { get; set; }

    public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal MonthlyRate { get; set; }

    public decimal BasicSalary { get; set; }
}