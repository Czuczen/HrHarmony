using System.ComponentModel.DataAnnotations;
using HrHarmony.Attributes;
using HrHarmony.Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.ViewModels.Absence;

public class CreateViewModel : IAbsenceOptionFields
{
    // od dnia dzisiejszego??
    [Display(Name = "Data absencji")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required]
    [MinimumCurrentDate]
    public DateTime AbsenceDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Typ absencji")]
    public int AbsenceTypeId { get; set; }

    public IEnumerable<SelectListItem> AbsenceTypes { get; set; } = new List<SelectListItem>();

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Pracownik")]
    public int EmployeeId { get; set; }

    public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
}