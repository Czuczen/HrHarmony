namespace HrHarmony.Data.QueryBuilder.Pagination;

public class PaginatedQuery<TEntity>
{
    public int TotalCount { get; set; }
        
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string OrderBy { get; set; }

    public bool IsDescending { get; set; }

    public string SearchString { get; set; }

    public int SearchedCount { get; set; }

    public IQueryable<TEntity> Query { get; set; }
}