using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.EntityFrameworkCore;
using Report.Features.Mobile.CoMay.Models;
using Report.Infrastructure;

namespace Report.Features.Mobile.CoMay;

public class ReportPrintSalesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("print-sales", Handle)
            .WithSummary("Tổng hợp in báo cáo");
    }

    private async Task<IResult> Handle([AsParameters] ReportService service, [AsParameters] ReportPrintSalesStore request, CancellationToken cancellationToken)
    {
        var query = new ReportPrintSalesQuery(request);
        var result = await service.Mediator.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}

public class ReportPrintSalesStore : QueryStoreBase
{
    public string? Date { get; set; }
    public string? PathLogo { get; set; }
    public string? PathImages { get; set; }
}

public record ReportPrintSalesQuery(ReportPrintSalesStore ReportPrintSalesStore) : IQuery<Result>;

/// <summary>
/// SalesSL: Sl sản xuất/ giao hàng bán theo nhóm hàng
/// SalesSLMonth: Sl sản xuất/ giao hàng bán tháng
/// EceivablesPayables: Công nợ phải thu, trả
/// SalesSLPlan: Thực hiện kế hoạch
/// CashFlow: BÁO CÁO TỔNG HỢP DOANH THU THEO MẶT HÀNG 
/// </summary>
/// <param name="smartDataServices"></param>
/// <param name="dbContext"></param>
/// <param name="currentUser"></param>
public class ReportPrintSalesQueryHandler(SmartDataServices smartDataServices,
    ReportDbContext dbContext,
    ICurrentUser currentUser) : IQueryHandler<ReportPrintSalesQuery, Result>
{
    public async Task<Result> Handle(ReportPrintSalesQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        query.ReportPrintSalesStore.CodeUnit = currentUser.CodeUnit;
        query.ReportPrintSalesStore.UserCode = currentUser.CodeUser;
        List<object> data;
        if (query.ReportPrintSalesStore.Parameter == "CashFlow")
        {
            data = await smartDataServices.GetListObject<object>("ReportPrintActivityCashFlow",
                dbContext.Database.GetConnectionString()!,
                query.ReportPrintSalesStore, cancellationToken);
        }
        else
        {
            data = await smartDataServices.GetListObject<object>("ReportPrintSales",
               dbContext.Database.GetConnectionString()!,
               query.ReportPrintSalesStore, cancellationToken);
        }

        return Result.Success(data);
    }
}