namespace SmartAccCloud.Application.Pagination;

public class PagedResult<TDataType> where TDataType : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecode { get; set; }
    public List<AggregateResult>? AggregateResults { get; set; }

    public int PageCount
    {
        get
        {
            var pageCount = (double)TotalRecode / PageSize;
            return (int)Math.Ceiling(pageCount);
        }
    }

    public List<TDataType> Items { get; set; } = new();
}