using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces.SelectOptions
{
    public interface IMaritalStatusOptions
    {
        public IEnumerable<SelectListItem> MaritalStatuses { get; set; }
    }
}
