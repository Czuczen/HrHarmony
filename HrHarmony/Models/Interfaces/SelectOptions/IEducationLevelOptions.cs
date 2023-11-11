using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces.SelectOptions
{
    public interface IEducationLevelOptions
    {
        public IEnumerable<SelectListItem> EducationLevels { get; set; }
    }
}
