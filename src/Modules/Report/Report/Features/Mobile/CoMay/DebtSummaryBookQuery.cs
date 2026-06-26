using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.EntityFrameworkCore;
using Report.Infrastructure;

namespace Report.Features.Mobile.CoMay;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("debt-summary-book", Handle)
            .WithSummary("Tổng hợp công nợ phải thu, phải trả");
    }

    private async Task<IResult> Handle([AsParameters] ReportService service, [AsParameters] DebtSummaryBookQuery query)
    {
        var result = await service.Mediator.Send(query);
        return TypedResults.Ok(result);
    }
}

public class DebtSummaryBookQueryStore
{
    public string Parameter { get; set; }
    public string Id { get; set; }
    public string UserCode { get; set; }
    public int CodeUnit { get; set; }
    public int AccountSymbol { get; set; }
    public string BeginDate { get; set; }
    public string EndDate { get; set; }
    public string Date { get; set; }
    public string PathImages { get; set; }
    public string PathLogo { get; set; }
    public string WareHouseCode { get; set; }
    public string ProductCode { get; set; }
    public string SmartSoftware { get; set; }
}

public record DebtSummaryBookQuery(string Parameter,
    string? Id,
    string? UserCode,
    string? CodeUnit,
    int AccountSymbol,
    string BeginDate,
    string EndDate,
    string Date,
    string? ObjectCode) : IQuery<Result>;

internal sealed class DebtSummaryBookQueryHandler(SmartDataServices smartDataServices, ReportDbContext dbContext, ICurrentUser currentUser) : IQueryHandler<DebtSummaryBookQuery, Result>
{
    public async Task<Result> Handle(DebtSummaryBookQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var param = new DebtSummaryBookQueryStore
        {
            Parameter = query.Parameter,
            Id = query.Id,
            UserCode = currentUser.CodeUser!,
            CodeUnit = currentUser.CodeUnit,
            AccountSymbol = query.AccountSymbol,
            BeginDate = query.BeginDate,
            EndDate = query.EndDate,
            Date = query.Date,
            PathImages = "",
            PathLogo = "",
            WareHouseCode = "",
            ProductCode = query.ObjectCode ?? "",
            SmartSoftware = ""
        };
        List<object> data;
        if (string.IsNullOrEmpty(query.ObjectCode))
        {
            data = await smartDataServices.GetListObject<object>("DebtSummaryBook", dbContext.Database.GetConnectionString()!,
               param, cancellationToken);
        }
        else
        {
            data = await smartDataServices.GetListObject<object>("AccountDetailsBook", dbContext.Database.GetConnectionString()!,
               param, cancellationToken);
        }
        
        return Result.Success(data);
    }

}