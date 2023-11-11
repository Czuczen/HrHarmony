using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces.SelectOptions
{
    public interface IContractTypeOptions
    {
        public IEnumerable<SelectListItem> ContractTypes { get; set; }
    }
}
