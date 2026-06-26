using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Queries.CreateOtherVoucher;

public class CreateOtherVoucherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPost("create-othervoucher", CreateOtherVoucher)
            .WithSummary("Tạo phiếu khác")
            .WithTags("Vouchers");
    }
    private async Task<IResult> CreateOtherVoucher([AsParameters] VoucherServices services, CreateOtherVoucherQuery request,
        CancellationToken token)
    {
        var result = await services.Mediator.Send(request, token);

        return Results.Ok(result);
    }
}