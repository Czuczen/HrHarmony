using System.ComponentModel.DataAnnotations;
using HrHarmony.Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.ViewModels.Employee;

public class CreateViewModel : ILoadGroupedEmployeeOptions
{
    [Display(Name = "Imię i nazwisko")]
    public string FullName { get; set; }

    [Display(Name = "Adres emial")]
    public string Email { get; set; }

    [Display(Name = "Numer telefonu")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Data urodzenia")]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "Stan cywilny")]
    public int MaritalStatusId { get; set; }

    public IEnumerable<SelectListItem> MaritalStatuses { get; set; } = new List<SelectListItem>();

    [Display(Name = "Adres")]
    public int AddressId { get; set; }

    public IEnumerable<SelectListItem> Addresses { get; set; } = new List<SelectListItem>();

    [Display(Name = "Wykształcenie")]
    public int EducationLevelId { get; set; }

    public IEnumerable<SelectListItem> EducationLevels { get; set; } = new List<SelectListItem>();

    [Display(Name = "Doświadczenie")]
    public int ExperienceId { get; set; }

    public IEnumerable<SelectListItem> Experiences { get; set; } = new List<SelectListItem>();
}