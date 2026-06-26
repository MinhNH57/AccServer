using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.EntityFrameworkCore;
using Report.Infrastructure;

namespace Report.Features.Mobile.CoMay;

public class AccountSalesReportEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("account-sales", Handle)
            .WithSummary("Báo cáo bán hàng");
    }

    private async Task<IResult> Handle([AsParameters] ReportService service, [AsParameters] AccountSalesReportQuery query)
    {
        var result = await service.Mediator.Send(query);
        return TypedResults.Ok(result);
    }
}
public record AccountSalesReportQuery(string Parameter,
    string? Id,
    string? UserCode,
    string? CodeUnit,
    int AccountSymbol,
    string BeginDate,
    string EndDate,
    string? ProductCode) : IQuery<Result>;

internal sealed class AccountSalesReportQueryHandler(
    SmartDataServices smartDataServices,
    ReportDbContext dbContext,
    ICurrentUser currentUser) : IQueryHandler<AccountSalesReportQuery, Result>
{
    public async Task<Result> Handle(AccountSalesReportQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);
        var param = new AccountSalesReportQueryStore
        {
            Parameter = query.Parameter,
            Id = query.Id,
            UserCode = currentUser.CodeUser!,
            CodeUnit = currentUser.CodeUnit,
            AccountSymbol = query.AccountSymbol,
            BeginDate = query.BeginDate,
            EndDate = query.EndDate,
            WareHouseCode = "",
            ProductCode = query.ProductCode ?? "",
            SmartSoftware = ""
        };

        var data = await smartDataServices.GetListObject<object>("AccountSalesReport",
            dbContext.Database.GetConnectionString()!,
            param);

        return Result.Success(data);
    }
}

public class AccountSalesReportQueryStore
{
    public string Parameter { get; set; }
    public string Id { get; set; }
    public string UserCode { get; set; }
    public int CodeUnit { get; set; }
    public int AccountSymbol { get; set; }
    public string BeginDate { get; set; }
    public string EndDate { get; set; }
    public string WareHouseCode { get; set; }
    public string ProductCode { get; set; }
    public string SmartSoftware { get; set; }
}