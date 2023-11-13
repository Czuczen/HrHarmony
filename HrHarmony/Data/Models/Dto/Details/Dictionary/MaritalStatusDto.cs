﻿using HrHarmony.Data.Models.Dto.Details.Main;

namespace HrHarmony.Data.Models.Dto.Details.Dictionary;

public class MaritalStatusDto : EntityDto<int>
{
    public string StatusName { get; set; }

    public List<EmployeeDto> Employees { get; set; }
}