using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrHarmony.Data.Models.Interfaces.SelectOptions
{
    public interface ILoadLeaveTypeOptions
    {
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
    }
}
