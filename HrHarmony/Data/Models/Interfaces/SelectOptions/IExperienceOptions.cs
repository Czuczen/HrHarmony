using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface IExperienceOptions
    {
        public IEnumerable<SelectListItem> Experiences { get; set; }
    }
}
