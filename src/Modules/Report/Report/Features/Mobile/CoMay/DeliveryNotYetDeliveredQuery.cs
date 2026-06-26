using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Microsoft.EntityFrameworkCore;
using Report.Features.Mobile.CoMay.Models;
using Report.Infrastructure;

namespace Report.Features.Mobile.CoMay;


public class DeliveryNotYetDeliveredEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("tracking-order", Handler)
            .WithSummary("Theo dõi đơn hàng mua, bán");

    }

    private async Task<IResult> Handler([AsParameters] ReportService service, [AsParameters] QueryStoreBase request, CancellationToken clt)
    {
        var query = new DeliveryNotYetDeliveredQuery(request);
        var result =  await service.Mediator.Send(query, clt);
        return TypedResults.Ok(result);
    }
}

public record DeliveryNotYetDeliveredQuery(QueryStoreBase QueryStoreBase) : IQuery<Result>;

public class DeliveryNotYetDeliveredQueryHandler(SmartDataServices smartDataServices, ReportDbContext dbContext, ICurrentUser currentUser) : IQueryHandler<DeliveryNotYetDeliveredQuery, Result>
{
    public async Task<Result> Handle(DeliveryNotYetDeliveredQuery query, CancellationToken cancellationToken)
    {
        query.QueryStoreBase.CodeUnit = currentUser.CodeUnit;
        query.QueryStoreBase.UserCode = currentUser.CodeUser;
        var result = await smartDataServices
            .GetListObject<object>("DeliveryNotYetDelivered", dbContext.Database.GetConnectionString()!, query.QueryStoreBase);

        return Result.Success(result);
    }
}