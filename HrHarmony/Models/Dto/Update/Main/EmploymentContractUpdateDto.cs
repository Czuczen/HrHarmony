namespace HrHarmony.Models.Dto.Update.Main;

public class EmploymentContractUpdateDto : EntityDto<int>
{
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int ContractTypeId { get; set; }

    public int EmployeeId { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal MonthlyRate { get; set; }

    public decimal BasicSalary { get; set; }
}