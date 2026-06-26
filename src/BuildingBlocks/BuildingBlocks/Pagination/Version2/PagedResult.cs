using Newtonsoft.Json;

namespace BuildingBlocks.Pagination.Version2;

public class PagedResult<TDataType> where TDataType : class
{
    [JsonConstructor]
    public PagedResult(int pageNumber, int pageSize, int totalRecode, List<TDataType> items)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecode = totalRecode;
        Items = items;
    }

    public PagedResult()
    {

    }
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 1;
    public long TotalRecode { get; set; }
    public List<AggregateResult>? AggregateResults { get; set; }
    public IDictionary<string, double>? SummaryData { get; set; }
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