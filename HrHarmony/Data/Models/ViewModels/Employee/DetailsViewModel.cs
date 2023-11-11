namespace HrHarmony.Data.Models.ViewModels.Employee;

public class DetailsViewModel
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int MaritalStatusId { get; set; }
    public MaritalStatus.IndexViewModel MaritalStatus { get; set; }

    public int AddressId { get; set; }
    public Address.IndexViewModel Address { get; set; }

    public int EducationLevelId { get; set; }
    public EducationLevel.IndexViewModel EducationLevel { get; set; }

    public int ExperienceId { get; set; }
    public Experience.IndexViewModel Experience { get; set; }

    public IEnumerable<EmploymentContract.DetailsViewModel> Contracts { get; set; }
    public IEnumerable<Leave.DetailsViewModel> Leaves { get; set; }
    public IEnumerable<Absence.DetailsViewModel> Absences { get; set; }
    public IEnumerable<Salary.DetailsViewModel> Salaries { get; set; }
}