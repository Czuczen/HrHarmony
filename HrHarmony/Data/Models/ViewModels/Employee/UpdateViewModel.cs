using System.ComponentModel.DataAnnotations;
using HrHarmony.Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.ViewModels.Employee;

public class UpdateViewModel : IEmployeeOptionFields
{
    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Imię i nazwisko")]
    public string FullName { get; set; }

    [Display(Name = "Numer telefonu")]
    [Phone(ErrorMessage = "Podaj prawidłowy numer telefonu!")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Data urodzenia")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Stan cywilny")]
    public int MaritalStatusId { get; set; }

    public IEnumerable<SelectListItem> MaritalStatuses { get; set; } = new List<SelectListItem>();

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Adres")]
    public int AddressId { get; set; }

    public IEnumerable<SelectListItem> Addresses { get; set; } = new List<SelectListItem>();

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Wykształcenie")]
    public int EducationLevelId { get; set; }

    public IEnumerable<SelectListItem> EducationLevels { get; set; } = new List<SelectListItem>();

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Doświadczenie")]
    public int ExperienceId { get; set; }

    public IEnumerable<SelectListItem> Experiences { get; set; } = new List<SelectListItem>();
}