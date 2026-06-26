using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.AdminCalculatingCostOfGoodsSold;

public class CalculatingCostOfGoodsSoldEndpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/AdminCalculatingCostOfGoodsSold", CalculatingCostOfGoodsSold)
            .WithTags("Vouchers");

    }

    private async Task<IResult> CalculatingCostOfGoodsSold([AsParameters] VoucherServices services, [AsParameters] CalculatingCostOfGoodsSoldRequest request, CancellationToken token)
    {
        var command = new CalculatingCostOfGoodsSoldCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }

}