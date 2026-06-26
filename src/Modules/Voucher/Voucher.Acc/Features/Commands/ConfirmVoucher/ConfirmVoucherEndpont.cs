using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.ConfirmVoucher;

public class ConfirmVoucherEndpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapPost("/confirm-voucher", ConfirmVoucher)
            .WithTags("Vouchers");
    }

    private async Task<IResult> ConfirmVoucher([AsParameters] VoucherServices services, ConfirmVoucherRequest request, CancellationToken token, int sendNoti = 1)
    {
        var command = new ConfirmVoucherCommand(request, sendNoti);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }
}