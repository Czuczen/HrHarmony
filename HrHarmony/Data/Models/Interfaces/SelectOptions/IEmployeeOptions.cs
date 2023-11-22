using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface IEmployeeOptions
    {
        public int EmployeeId { get; set; }

        public string EmployeeText { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}