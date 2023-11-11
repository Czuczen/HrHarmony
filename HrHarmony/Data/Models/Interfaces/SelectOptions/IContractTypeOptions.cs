using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface IContractTypeOptions
    {
        public IEnumerable<SelectListItem> ContractTypes { get; set; }
    }
}
