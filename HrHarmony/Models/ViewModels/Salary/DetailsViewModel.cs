namespace HrHarmony.Models.ViewModels.Salary;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public int EmployeeId { get; set; }

    public Employee.IndexViewModel Employee { get; set; }

    public decimal BasicSalary { get; set; }
    public decimal AdditionalSalary { get; set; }
    public decimal Bonuses { get; set; }
    public decimal Allowances { get; set; }
    public decimal ZUSContributions { get; set; }
    public decimal IncomeTax { get; set; }
}
