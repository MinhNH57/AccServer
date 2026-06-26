using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.CostOfGoodsSoldTransfer;

public class CostOfGoodsSoldTransferEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/CostOfGoodsSoldTransfer",CostOfGoodsSoldTransfer)
            .WithTags("Vouchers");

    }

    private async Task<IResult>CostOfGoodsSoldTransfer([AsParameters] VoucherServices services, [AsParameters]CostOfGoodsSoldTransferRequest request, CancellationToken token)
    {
        var command = new CostOfGoodsSoldTransferCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }

}