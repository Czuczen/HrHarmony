using HrHarmony.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.Leave;

public class CreateViewModel : ILeaveOptionFields
{
    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Data rozpoczęcia")]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Data zakończenia")]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }

    [Display(Name = "Rodzaj urlopu")]
    [Required(ErrorMessage = "Pole jest wymagane!")]
    public int LeaveTypeId { get; set; }

    public IEnumerable<SelectListItem> LeaveTypes { get; set; } = new List<SelectListItem>();

    [Display(Name = "Pracownik")]
    [Required(ErrorMessage = "Pole jest wymagane!")]
    public int EmployeeId { get; set; }

    public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();

}