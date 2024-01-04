using HrHarmony.Data.Models.Dto.Details.Dictionary;

namespace HrHarmony.Data.Models.Dto.Details.Main;

public class EmployeeDto : EntityDto<int>
{
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

    public List<EmploymentContractDto> Contracts { get; set; }

    public List<AbsenceDto> Absences { get; set; }

    public List<SalaryDto> Salaries { get; set; }
}