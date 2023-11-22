using System.ComponentModel.DataAnnotations;
using HrHarmony.Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.ViewModels.Leave;

public class CreateViewModel : ILeaveOptionFields
{
    [Display(Name = "Data rozpoczęcia")]
    public DateTime StartDate { get; set; }

    [Display(Name = "Data zakończenia")]
    public DateTime EndDate { get; set; }

    [Display(Name = "Rodzaj urlopu")]
    public int LeaveTypeId { get; set; }

    public IEnumerable<SelectListItem> LeaveTypes { get; set; } = new List<SelectListItem>();

    [Display(Name = "Pracownik")]
    public int EmployeeId { get; set; }

    public string EmployeeText { get; set; }

    public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();

}