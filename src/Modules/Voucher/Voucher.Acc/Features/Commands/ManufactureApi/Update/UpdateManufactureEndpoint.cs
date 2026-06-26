using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.ManufactureApi.Update;

public class UpdateManufactureEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("Manufacture/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPut("update-manufacture/{id}", UpdateVoucher)
            .WithTags("Manufacture");
    }

    private async Task<IResult> UpdateVoucher(
        [AsParameters] VoucherServices services,
        Guid id,
        UpdateManufactureRequest request,
        CancellationToken token)
    {
        request.Id = id;
        var command = new UpdateManufactureCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }
}