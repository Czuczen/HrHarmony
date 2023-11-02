namespace HrHarmony.Repositories;

public class PaginatedResult<TEntity>
{
    public IEnumerable<TEntity> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
