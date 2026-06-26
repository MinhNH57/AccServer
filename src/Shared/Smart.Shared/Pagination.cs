namespace Smart.Shared;

public class Pagination<T>
{
    public List<T>? PageData { get; set; }
    public Dictionary<string, object>? SummaryData { get; set; }
    public int Total { get; set; }
    public int TotalDisplay { get; set; }
    public bool TableEmpty { get; set; }
}
