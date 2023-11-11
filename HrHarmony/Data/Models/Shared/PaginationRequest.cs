namespace HrHarmony.Data.Models.Shared;

public class PaginationRequest
{
    private int? _pageNumber;
    public int PageNumber
    {
        get => _pageNumber ?? 1;
        set => _pageNumber = value;
    }

    private int? _pageSize;
    public int PageSize
    {
        get => _pageSize ?? 10;
        set => _pageSize = value;
    }

    public string? OrderBy { get; set; }

    private bool? _isDescending;
    public bool IsDescending
    {
        get => _isDescending ?? false;
        set => _isDescending = value;
    }

    public string? SearchString { get; set; }
}