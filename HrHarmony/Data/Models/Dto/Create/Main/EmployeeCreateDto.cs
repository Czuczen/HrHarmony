using HrHarmony.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Create.Main;

public class EmployeeCreateDto
{
    [Required(ErrorMessage = "Pole jest wymagane.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Imię i nazwisko powinny mieć od 3 do 50 znaków.")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Imię i nazwisko mogą zawierać tylko litery i spacje.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [EmailAddress(ErrorMessage = "Proszę wprowadzić poprawny adres e-mail.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [Phone(ErrorMessage = "Podaj prawidłowy numer telefonu!")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.Date, ErrorMessage = "Proszę podać poprawną datę.")]
    [AgeRange(18, 67)]
    public DateTime? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? MaritalStatusId { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? AddressId { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? EducationLevelId { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? ExperienceId { get; set; }
}