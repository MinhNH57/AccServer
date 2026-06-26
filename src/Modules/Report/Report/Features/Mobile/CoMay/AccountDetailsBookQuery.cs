using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Carter;
using Microsoft.EntityFrameworkCore;
using Report.Features.Mobile.CoMay.Models;
using Report.Infrastructure;
using BuildingBlocks.Web;

namespace Report.Features.Mobile.CoMay;

public class AccountDetailsBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("account-detail", Handler)
            .WithSummary("Sổ chi tiết theo tài khoản");
    }

    private async Task<IResult> Handler([AsParameters] ReportService service, [AsParameters] AccountDetailsBookStore request, CancellationToken clt)
    {
        //Param
        //DetailAcountParentMobile: Lấy chi tiết tất cả tk con theo cha
        var query = new AccountDetailsBookQuery(request);
        var result = await service.Mediator.Send(query, clt);

        return TypedResults.Ok(result);
    }
}

public record AccountDetailsBookResponse(List<object> AccSymbol111, List<object> AccSymbol112);

public class AccountDetailsBookStore : QueryStoreBase
{
    public string? Date { get; set; }
    public string? PathLogo { get; set; }
    public string? PathImages { get; set; }
}
public record AccountDetailsBookQuery(AccountDetailsBookStore AccountDetailsBookStore) : IQuery<Result>;

/// <summary>
/// DebtDetailsBookCM: Báo cáo công nợ
/// </summary>
/// <param name="smartDataServices"></param>
/// <param name="dbContext"></param>
/// <param name="currentUser"></param>
internal sealed class AccountDetailsBookQueryHandler(SmartDataServices smartDataServices, ReportDbContext dbContext, ICurrentUser currentUser) : IQueryHandler<AccountDetailsBookQuery, Result>
{
    public async Task<Result> Handle(AccountDetailsBookQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        query.AccountDetailsBookStore.CodeUnit = currentUser.CodeUnit;
        query.AccountDetailsBookStore.UserCode = currentUser.CodeUser;
        query.AccountDetailsBookStore.AccountSymbol = 111;
        var dataAcc111 = await smartDataServices.GetListObject<object>("AccountDetailsBook", dbContext.Database.GetConnectionString()!,
            query.AccountDetailsBookStore, cancellationToken);

        query.AccountDetailsBookStore.AccountSymbol = 112;
        var dataAcc112 = await smartDataServices.GetListObject<object>("AccountDetailsBook", dbContext.Database.GetConnectionString()!,
            query.AccountDetailsBookStore, cancellationToken);

        return Result.Success(new AccountDetailsBookResponse(dataAcc111, dataAcc112));
    }
}