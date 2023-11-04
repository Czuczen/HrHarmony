namespace HrHarmony.Repositories;

public class PaginatedResult<TEntityDto>
{
    public IEnumerable<TEntityDto> Items { get; set; }

    public int PageNumber { get; set; }

    public int TotalCount { get; set; }

    public int SearchedCount { get; set; }

    public int PageSize { get; set; }

    public string OrderBy { get; set; }

    public bool IsDescending { get; set; }

    public string? SearchString { get; set; }
}