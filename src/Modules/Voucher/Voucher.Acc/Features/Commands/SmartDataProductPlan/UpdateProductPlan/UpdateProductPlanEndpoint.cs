using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.SmartDataProductPlan.UpdateProductPlan;


public class UpdateProductPlanEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPut("update-product-plan/{id}", UpdateVoucher)
            .WithTags("Vouchers");
    }

    private async Task<IResult> UpdateVoucher(
        [AsParameters] VoucherServices services,
        Guid id,
        UpdateProductPlanRequest request,
        CancellationToken token)
    {
        request.Id = id;
        var command = new UpdateProductPlanCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }
}