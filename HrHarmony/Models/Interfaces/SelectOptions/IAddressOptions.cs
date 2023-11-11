using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces.SelectOptions
{
    public interface IAddressOptions
    {
        public IEnumerable<SelectListItem> Addresses { get; set; }
    }
}
