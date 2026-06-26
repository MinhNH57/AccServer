namespace Report.Features.Mobile.CoMay.Models;

public class QueryStoreBase
{
    public string Parameter { get; set; }
    public string? Id { get; set; }
    public string? UserCode { get; set; }
    public int? CodeUnit { get; set; }
    public int? AccountSymbol { get; set; }
    public string BeginDate { get; set; }
    public string EndDate { get; set; }
    public string? WareHouseCode { get; set; }
    public string? ProductCode { get; set; }
    public string? SmartSoftware { get; set; }
}