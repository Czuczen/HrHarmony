﻿using HrHarmony.Models.Dto.Details.Main;

namespace HrHarmony.Models.Dto.Details.Dictionary;

public class ContractTypeDto : EntityDto<int>
{
    public string TypeName { get; set; }

    public List<EmploymentContractDto> Contracts { get; set; }
}