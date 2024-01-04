using HrHarmony.Data.Models.Entities.Dictionary;

namespace HrHarmony.Data.Models.Entities.Main;

public class Employee : Entity<int>
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int MaritalStatusId { get; set; }

    public MaritalStatus MaritalStatus { get; set; }

    public int AddressId { get; set; }

    public Address Address { get; set; }

    public int EducationLevelId { get; set; }

    public EducationLevel EducationLevel { get; set; }

    public int ExperienceId { get; set; }

    public Experience Experience { get; set; }

    public List<EmploymentContract> Contracts { get; set; }

    public List<Absence> Absences { get; set; }

    public List<Salary> Salaries { get; set; }
}