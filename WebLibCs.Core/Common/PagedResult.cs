namespace WebLibCs.Core.Common;

public record PagedResult<T>(
    IEnumerable<T> Items,
    int CurrentPage,
    int PageSize,
    int TotalCount
)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}