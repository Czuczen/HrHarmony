using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface ILoadAddressOptions
    {
        public IEnumerable<SelectListItem> Addresses { get; set; }
    }
}
