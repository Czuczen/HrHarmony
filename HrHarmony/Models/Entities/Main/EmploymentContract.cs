using HrHarmony.Models.Entities.Dictionary;

namespace HrHarmony.Models.Entities.Main;

public class EmploymentContract : Entity<int>
{
    public string ContractNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int ContractTypeId { get; set; }

    public ContractType ContractType { get; set; }

    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal MonthlyRate { get; set; }

    public decimal BasicSalary { get; set; }
}