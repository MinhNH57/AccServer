using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.Sales.UpdateSalesVoucher;

public class UpdateSalesVoucherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPut("update-sales-voucher/{id}", UpdateVoucher)
            .WithTags("Vouchers");
    }

    private async Task<IResult> UpdateVoucher(
        [AsParameters] VoucherServices services,
        Guid id,
        UpdateSalesVoucherRequest request,
        CancellationToken token)
    {
        request.Id = id;
        var command = new UpdateSalesVoucherCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }
}