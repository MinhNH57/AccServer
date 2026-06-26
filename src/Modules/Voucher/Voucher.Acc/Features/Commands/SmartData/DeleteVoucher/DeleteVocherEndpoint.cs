using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.SmartData.DeleteVoucher;

public class DeleteVocherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapDelete("delete-voucher/{id}", DeleteVoucher)
            .WithName("delete-voucher")
            .WithSummary("Xóa chứng từ.")
            .WithTags("Vouchers");
    }

    private async Task<IResult> DeleteVoucher(
        [AsParameters] VoucherServices services,
    string tableName,
        Guid id
        )
    {
        var command = new DeleteVocherCommand(id, tableName);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }
}