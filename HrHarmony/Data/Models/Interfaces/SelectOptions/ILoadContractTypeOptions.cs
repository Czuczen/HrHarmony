using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions;

public interface ILoadContractTypeOptions
{
    public IEnumerable<SelectListItem> ContractTypes { get; set; }
}