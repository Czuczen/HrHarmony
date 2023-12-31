﻿using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Create.Main;

public class AbsenceCreateDto
{
    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.DateTime, ErrorMessage = "Proszę podać poprawną datę.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.DateTime, ErrorMessage = "Proszę podać poprawną datę.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? AbsenceTypeId { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    public int? EmployeeId { get; set; }
}