using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces
{
    public interface IEmployeeSelectListFields
    {
        public IEnumerable<SelectListItem> MaritalStatuses { get; set; }

        public IEnumerable<SelectListItem> Addresses { get; set; }

        public IEnumerable<SelectListItem> EducationLevels { get; set; }

        public IEnumerable<SelectListItem> Experiences { get; set; }
    }
}