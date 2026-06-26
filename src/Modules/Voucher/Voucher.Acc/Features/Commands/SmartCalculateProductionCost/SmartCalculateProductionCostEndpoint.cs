using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.SmartCalculateProductionCost;

public class SmartCalculateProductionCostEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/SmartCalculateProductionCost",SmartCalculateProductionCost)
            .WithTags("Vouchers");

    }

    private async Task<IResult>SmartCalculateProductionCost([AsParameters] VoucherServices services, [AsParameters]SmartCalculateProductionCostRequest request, CancellationToken token)
    {
        var command = new SmartCalculateProductionCostCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }

}