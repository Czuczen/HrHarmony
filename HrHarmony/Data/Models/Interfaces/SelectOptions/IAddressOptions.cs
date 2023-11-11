using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface IAddressOptions
    {
        public IEnumerable<SelectListItem> Addresses { get; set; }
    }
}
