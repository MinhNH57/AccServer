using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Queries.GetVoucherStore;

public class GetVoucherFromStoreQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("vouchers/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("get-dynamic-voucher", GetDynamic)
            .WithSummary("Lấy voucher bằng Store").
            WithTags("Vouchers");
    }

    private async Task<IResult> GetDynamic([AsParameters] VoucherServices services,
        [AsParameters] GetVoucherQuery query, CancellationToken clt)
    {
        var command = new GetVoucherFromStoreQuery(query);
        var result = await services.Mediator.Send(command, clt);

        return Results.Ok(result);
    }
}