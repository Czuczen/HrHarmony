using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface ILoadEducationLevelOptions
    {
        public IEnumerable<SelectListItem> EducationLevels { get; set; }
    }
}
