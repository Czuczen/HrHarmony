using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface IEmployeeOptions
    {
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}