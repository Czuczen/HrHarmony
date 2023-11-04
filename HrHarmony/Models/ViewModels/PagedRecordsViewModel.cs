using HrHarmony.Models.Dto.Details.Main;
using HrHarmony.Repositories;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json.Linq;

namespace HrHarmony.Models.ViewModels
{
    public class PagedRecordsViewModel<TIndexViewModel>
    {
        public List<TIndexViewModel> Items { get; set; }

        public int PageNumber { get; set; }

        public int TotalCount { get; set; }

        public int SearchedCount { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public bool IsDescending { get; set; }

        public string? SearchString { get; set; }





        public string ValidPageSize => (PageSize > TotalCount ? TotalCount : PageSize > Items.Count ? Items.Count : PageSize).ToString();

        public int TotalPages => (int) Math.Ceiling((double) SearchedCount / PageSize); // lub obliczyć z polem TotalCount

        public IEnumerable<int> AvailablePageSizes = new List<int> { 5, 10, 20, 50, 100 };

        public HtmlString PageSizeOptions
        {
            get
            {
                var ret = "";
                foreach(var pageSize in AvailablePageSizes)
                {
                    var isSelected = PageSize == pageSize ? "selected" : "";
                    ret += $@"<option value=""{pageSize}"" {isSelected}>{pageSize}</option>";
                }

                return new HtmlString(ret);
            }
        }

        public HtmlString Arrow
        {
            get
            {
                var arrowClass = IsDescending ? "arrow-down " : "arrow-up";
                return new HtmlString($"<i class=\"{arrowClass} float-end\"></i>");
            }
        }
    }
}