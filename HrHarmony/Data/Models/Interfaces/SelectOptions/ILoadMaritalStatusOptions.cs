using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface ILoadMaritalStatusOptions
    {
        public IEnumerable<SelectListItem> MaritalStatuses { get; set; }
    }
}
