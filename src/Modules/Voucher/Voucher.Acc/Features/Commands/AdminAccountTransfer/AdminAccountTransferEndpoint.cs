using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.AdminAccountTransfer;

public class AdminAccountTransferEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/AdminAccountTransfer",AdminAccountTransfer)
            .WithTags("Vouchers");

    }

    private async Task<IResult>AdminAccountTransfer([AsParameters] VoucherServices services, [AsParameters]AdminAccountTransferRequest request, CancellationToken token)
    {
        var command = new AdminAccountTransferCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }

}