using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Queries.GetQuantityOfInventory;

public class GetQuantityOfInventoryEndpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/get-quantity-of-inventory", GetDebitBlanc)
            .WithSummary("Lấy số lượng tồn")
            .WithTags("Vouchers");
    }

    private async Task<IResult> GetDebitBlanc([AsParameters] VoucherServices services, [AsParameters] GetQuantityOfInventoryRequest request, CancellationToken token)
    {
        var command = new GetQuantityOfInventoryCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }
}