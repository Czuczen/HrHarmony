﻿using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Models.ViewModels.Leave;

public class UpdateViewModel
{
    [Required(ErrorMessage = "Pole jest wymagane!")]
    [Display(Name = "Data zakończenia")]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }
}