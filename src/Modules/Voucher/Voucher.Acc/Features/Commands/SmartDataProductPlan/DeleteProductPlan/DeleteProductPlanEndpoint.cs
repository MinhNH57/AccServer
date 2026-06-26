using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.SmartDataProductPlan.DeleteProductPlan;

public class DeleteProductPlanEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapDelete("delete-product-plan/{id}", DeleteVoucher)
            .WithName("delete-product-plan")
            .WithSummary("Xóa kế hoạch sx.")
            .WithTags("Vouchers");
    }

    private async Task<IResult> DeleteVoucher(
        [AsParameters] VoucherServices services,
    string tableName,
        Guid id
        )
    {
        var command = new DeleteProductPlanCommand(id, tableName);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }
}