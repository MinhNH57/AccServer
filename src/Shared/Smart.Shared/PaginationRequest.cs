namespace Smart.Shared;

public class PaginationRequest
{
    public string? Parameters { get; set; }
    public string? Sort { get; set; }
    public List<Filter>? Filter { get; set; }
    public List<Filter>? CustomFilter { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public bool UseSp { get; set; }
    public int View { get; set; }
    public List<int>? SummaryColumns { get; set; }
    public int LoadMode { get; set; }
}
