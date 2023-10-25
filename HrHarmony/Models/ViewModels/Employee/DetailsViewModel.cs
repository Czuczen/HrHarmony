using HrHarmony.Models.Dto.Details.Dictionary;

namespace HrHarmony.Models.ViewModels.Employee;

public class DetailsViewModel : MainDetails
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int MaritalStatusId { get; set; }
    public MaritalStatusDto MaritalStatus { get; set; }

    public int AddressId { get; set; }
    public AddressDto Address { get; set; }

    public int EducationLevelId { get; set; }
    public EducationLevelDto EducationLevel { get; set; }

    public int ExperienceId { get; set; }
    public ExperienceDto Experience { get; set; }

    public IEnumerable<EmploymentContract.DetailsViewModel> Contracts { get; set; }
    public IEnumerable<Leave.DetailsViewModel> Leaves { get; set; }
    public IEnumerable<Absence.DetailsViewModel> Absences { get; set; }
}