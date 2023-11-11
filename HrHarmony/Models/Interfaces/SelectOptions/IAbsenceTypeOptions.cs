using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces.SelectOptions
{
    public interface IAbsenceTypeOptions
    {
        public IEnumerable<SelectListItem> AbsenceTypes { get; set; }
    }
}
