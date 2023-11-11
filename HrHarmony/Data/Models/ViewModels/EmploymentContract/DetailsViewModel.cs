namespace HrHarmony.Data.Models.ViewModels.EmploymentContract;

public class DetailsViewModel
{
    public int Id { get; set; }
    public string ContractNumber { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int ContractTypeId { get; set; }
    public ContractType.IndexViewModel ContractType { get; set; }

    public int EmployeeId { get; set; }

    public Employee.IndexViewModel Employee { get; set; }

    public decimal HourlyRate { get; set; }
    public decimal MonthlyRate { get; set; }
    public decimal BasicSalary { get; set; }
}