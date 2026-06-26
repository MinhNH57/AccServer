using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.Sales.DeleteSalesVoucher;

public class DeleteSalesVocherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapDelete("delete-sale-voucher/{id}", DeleteVoucher)
            .WithName("delete-sale-voucher")
            .WithSummary("Xóa chứng từ.")
            .WithTags("Vouchers");
    }

    private async Task<IResult> DeleteVoucher(
        [AsParameters] VoucherServices services,
        Guid id
        )
    {
        var command = new DeleteSalesVocherCommand(id);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }
}