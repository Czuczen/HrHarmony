using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using System.Text;

namespace HrHarmony.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent RecordsSearchComboBoxFor(this IHtmlHelper helper, string fieldName, string textValue,
            string controllerName, IEnumerable<SelectListItem> selectList, string placeholder, object htmlAttributes)
        {
            if (!fieldName.EndsWith("Id"))
                throw new InvalidOperationException($"The field '{fieldName}' must be a relational field ending with 'Id'.");

            var entityName = fieldName.Substring(0, fieldName.Length - 2);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var inputAttributes = string.Join(" ",
                attributes.Select(x => $"{x.Key}=\"{x.Value.ToString()}\""));

            var options = new StringBuilder();
            foreach (var item in selectList)
                options.Append($"<option class=\"dropdown-item\" value=\"{item.Value}\">{item.Text}</option>");

            var searchableSelect = $@"
                <div class=""dropdown"">
                    <input type=""hidden"" id=""{fieldName}"" name=""{fieldName}"" value="""" />
                    <input type=""text"" id=""{fieldName}Search"" data-list=""{fieldName}List"" placeholder=""{placeholder}"" 
                        data-controller=""{controllerName}"" data-entity-name=""{entityName}"" {inputAttributes}
                        data-bs-toggle=""dropdown"" aria-expanded=""false"" value=""{textValue}"" autocomplete=""off"">
            
                    <ul id=""{fieldName}List"" class=""dropdown-menu w-100 overflow-auto"" aria-labelledby=""{fieldName}Search"">
                        {options}
                    </ul>
            
                </div>";

            return helper.Raw(searchableSelect);
        }
    }
}
