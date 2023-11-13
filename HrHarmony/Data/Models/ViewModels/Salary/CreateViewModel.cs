﻿using System.ComponentModel.DataAnnotations;
using HrHarmony.Data.Models.Interfaces.SelectOptions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.ViewModels.Salary;

public class CreateViewModel : IEmployeeOptions
{
    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Data płatności")]
    [DataType(DataType.DateTime)]
    public DateTime PaymentDate { get; set; }

    [Display(Name = "Pracownik")]
    [Required(ErrorMessage = "Pole jest wymagane!")]
    public int EmployeeId { get; set; }

    public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();

    [Display(Name = "Wynagrodzenie podstawowe")]
    [DataType(DataType.Currency)]
    //[Range(1, 1000000, ErrorMessage = "Wartość musi być między 1 a 1 000 000")]
    public decimal BasicSalary { get; set; }

    [Display(Name = "Dodatkowe wynagrodzenie")]
    [DataType(DataType.Currency)]
    public decimal AdditionalSalary { get; set; }

    [Display(Name = "Bonusy")]
    [DataType(DataType.Currency)]
    public decimal Bonuses { get; set; }

    [Display(Name = "Dodatki")]
    [DataType(DataType.Currency)]
    public decimal Allowances { get; set; }

    [Display(Name = "Składki ZUS")]
    [DataType(DataType.Currency)]
    public decimal ZUSContributions { get; set; }

    [Display(Name = "Podatek dochodowy")]
    [DataType(DataType.Currency)]
    public decimal IncomeTax { get; set; }

}