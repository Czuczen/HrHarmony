using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Html;

namespace HrHarmony.Data.Models.ViewModels;

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


    // =======================================

    public string IsDescendingAsString => IsDescending.ToString();

    public string ControllerName { get; set; }

    public string ValidPageSize => (PageSize > Items.Count ? Items.Count : PageSize).ToString();

    public int TotalPages => (int)Math.Ceiling((double)SearchedCount / PageSize);

    public IEnumerable<int> AvailablePageSizes = new List<int> { 5, 10, 20, 50, 100 };

    public HtmlString RenderPageSizeOptions
    {
        get
        {
            var ret = "";
            foreach (var pageSize in AvailablePageSizes)
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

    public List<PropertyInfo> Properties => typeof(TIndexViewModel).GetProperties().ToList();

    public Dictionary<string, string> ColumnNames
    {
        get
        {
            var ret = new Dictionary<string, string>();
            foreach (var item in Properties.Where(item => item.Name.ToLower() != "id"))
            {
                var displayAttribute = item.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                    ret[item.Name] = displayAttribute.GetName();
            }

            return ret;
        }
    }

    public HtmlString RenderColumns
    {
        get
        {
            var ret = "";
            foreach (var item in ColumnNames)
            {
                ret += "<th>";
                ret += $"    <a id=\"columnHref{item.Key}\" class=\"custom-link\" data-column-name=\"{item.Key}\">";
                ret += $"        {item.Value}";
                ret += $"        {(OrderBy == item.Key ? Arrow : "")}";
                ret += "    </a>";
                ret += "</th>";
            }

            ret += "<th>Akcje</th>";

            return new HtmlString(ret);
        }
    }

    public HtmlString RenderRecords
    {
        get
        {
            var ret = "";
            foreach (var item in Items)
            {
                ret += "<tr>";
                foreach (var prop in Properties.Where(prop => prop.Name.ToLower() != "id"))
                    ret += $"    <td>{prop.GetValue(item)}</td>";

                var idProp = Properties.Single(p => p.Name.ToLower() == "id");

                ret += "    <td class=\"d-flex\">";
                ret += $"       <a class=\"btn ms-2 btn-success btn-sm\" href=\"/{ControllerName}/Details/{idProp.GetValue(item)}\">Szczegóły</a>";
                ret += $"       <a class=\"btn ms-2 btn-info btn-sm\" href=\"/{ControllerName}/Edit/{idProp.GetValue(item)}\">Edytuj</a>";
                ret += $"       <a class=\"btn ms-2 btn-danger btn-sm\" href=\"/{ControllerName}/Delete/{idProp.GetValue(item)}\">Usuń</a>";
                ret += "    </td>";
                ret += "</tr>";
            }
                
            return new HtmlString(ret);
        }
    }

    public HtmlString RenderPages
    {
        get
        {
            var ret = "";

            // Dodaj przycisk do pierwszej strony przed kropkami
            if (PageNumber > 3)
                ret += $"<a id=\"pageHref1\" data-page-number=\"1\" class=\"btn m-1 p-r-1 p-l-1 bd-highlight btn-primary\">1</a>";

            // Dodaj kropki przed przyciskami
            if (PageNumber > 4)
                ret += $"<a class=\"btn m-1 p-r-1 p-l-1 bd-highlight btn-secondary disabled\">...</a>";

            // Dodaj przyciski
            for (var i = Math.Max(1, PageNumber - 2); i <= Math.Min(TotalPages, PageNumber + 2); i++)
                ret += $"<a id=\"pageHref{i}\" data-page-number=\"{i}\" class=\"btn m-1 p-r-1 p-l-1 bd-highlight {(PageNumber == i ? "btn-secondary disabled" : "btn-primary")}\">{i}</a>";

            // Dodaj kropki po przyciskach
            if (PageNumber < TotalPages - 3)
                ret += $"<a class=\"btn m-1 p-r-1 p-l-1 bd-highlight btn-secondary disabled\">...</a>";

            // Dodaj przycisk do ostatniej strony po kropkach
            if (PageNumber < TotalPages - 2)
                ret += $"<a id=\"pageHref{TotalPages}\" data-page-number=\"{TotalPages}\" class=\"btn m-1 p-r-1 p-l-1 bd-highlight btn-primary\">{TotalPages}</a>";

            return new HtmlString(ret);
        }
    }
}