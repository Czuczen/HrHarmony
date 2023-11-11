namespace HrHarmony.Data.Models.Dto.Create.Main;

public class EmployeeCreateDto
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int MaritalStatusId { get; set; }

    public int AddressId { get; set; }

    public int EducationLevelId { get; set; }

    public int ExperienceId { get; set; }
}