﻿using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Employee;

public class IndexViewModel
{
    public int Id { get; set; }

    [Display(Name = "Imię i nazwisko")]
    public string FullName { get; set; }
}