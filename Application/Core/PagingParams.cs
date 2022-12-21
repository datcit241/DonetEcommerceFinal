namespace Application.Core;

public class PagingParams
{
    private const int MaxPageSize = 10;
    private int _pageSize = 10;
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Min(MaxPageSize, value);
    }
}