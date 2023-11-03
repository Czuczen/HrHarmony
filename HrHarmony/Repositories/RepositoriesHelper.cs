namespace HrHarmony.Repositories
{
    public static class RepositoriesHelper
    {
        public static string GetDefaultSortField<TEntityDto>(string? orderBy)
        {
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                var orderProp = typeof(TEntityDto).GetProperty(orderBy);
                if (orderProp != null)
                    return orderBy;
            }
            else
            {
                var preferredSortFields = new string[] { "name", "fullname", "type", "nr", "date", "description" };
                var properties = typeof(TEntityDto).GetProperties();

                foreach (var fieldName in preferredSortFields)
                {
                    var property = properties.FirstOrDefault(prop => prop.Name.ToLower().Contains(fieldName));
                    if (property != null)
                        return property.Name;
                }
            }
            
            return "Id";
        }
    }
}
