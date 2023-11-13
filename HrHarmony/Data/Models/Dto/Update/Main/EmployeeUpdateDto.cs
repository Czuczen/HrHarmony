﻿namespace HrHarmony.Data.Models.Dto.Update.Main;

public class EmployeeUpdateDto : EntityDto<int>
{
    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int MaritalStatusId { get; set; }

    public int AddressId { get; set; }

    public int EducationLevelId { get; set; }

    public int ExperienceId { get; set; }
}