namespace HrHarmony.Models.Dto.Create.Main;

public class EmploymentContractCreateDto
{
    public string ContractNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int ContractTypeId { get; set; }

    public int EmployeeId { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal MonthlyRate { get; set; }

    public decimal BasicSalary { get; set; }
}