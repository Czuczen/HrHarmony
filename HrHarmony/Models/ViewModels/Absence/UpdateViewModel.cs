using HrHarmony.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.Absence;

public class UpdateViewModel : IAbsenceOptionFields
{
    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Typ absencji")]
    public int AbsenceTypeId { get; set; }

    public IEnumerable<SelectListItem> AbsenceTypes { get; set; } = new List<SelectListItem>();

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Pracownik")]
    public int EmployeeId { get; set; }

    public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
}