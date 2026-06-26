using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Catalog.Features.Web.Fund.Store.StoreModel;

public class StoreModelQuery
{
    public string? StoreName { get; set; } = "FundImportFromExcel";
    public string? Parameter { get; set; } = "CreateMember";
    public string? Id { get; set; } = string.Empty;
    public string? UserCode { get; set; } = string.Empty;
    public int? CodeUnit { get; set; } = 0;
    public string? AccountSymbol { get; set; } = string.Empty;
    public string? WareHouseCode { get; set; } = string.Empty;
    public string? BeginDate { get; set; } = DateTime.Now.ToString("MM-dd-yyyy");
    public string? EndDate { get; set; } = DateTime.Now.ToString("MM-dd-yyyy");
    public string? ProductCode { get; set; } = string.Empty;
    public string? SmartSoftware { get; set; } = string.Empty;
}
public record StoreQueryComnand(StoreModelQuery Request) : ICommand<Result>;