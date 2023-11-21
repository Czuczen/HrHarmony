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
        public static IHtmlContent RecordsSearchComboBoxFor(this IHtmlHelper helper, string fieldName, 
            string controllerName, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            if (!fieldName.EndsWith("Id"))
                throw new InvalidOperationException($"The field '{fieldName}' must be a relational field ending with 'Id'.");

            var entityName = fieldName.Substring(0, fieldName.Length - 2);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var placeholder = string.IsNullOrEmpty(optionLabel) ? "" : $@"placeholder=""{optionLabel}""";

            var inputAttributes = string.Join(" ",
                attributes.Select(x => $"{x.Key}=\"{x.Value.ToString()}\""));

            var dataList = new StringBuilder();

            foreach (var item in selectList)
                dataList.Append($"<option data-value=\"{item.Value}\">{item.Text}</option>");
            
            var searchableSelect = $@"
                <input id=""{fieldName}"" name=""{fieldName}"" type=""text"" data-controller=""{controllerName}"" data-entity-name=""{entityName}""
                    list=""{fieldName}List"" {inputAttributes} {placeholder} autoComplete=""off"">

                <datalist id=""{fieldName}List"">
                    {dataList}
                </datalist>";

            return helper.Raw(searchableSelect);
        }
    }
}
