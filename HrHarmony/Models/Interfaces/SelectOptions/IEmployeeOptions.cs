using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces.SelectOptions
{
    public interface IEmployeeOptions
    {
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}