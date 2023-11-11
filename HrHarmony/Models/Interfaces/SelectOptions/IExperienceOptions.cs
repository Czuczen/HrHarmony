using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces.SelectOptions
{
    public interface IExperienceOptions
    {
        public IEnumerable<SelectListItem> Experiences { get; set; }
    }
}
