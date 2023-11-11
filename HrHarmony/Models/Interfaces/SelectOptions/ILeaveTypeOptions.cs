using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Models.Interfaces.SelectOptions
{
    public interface ILeaveTypeOptions
    {
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
    }
}
