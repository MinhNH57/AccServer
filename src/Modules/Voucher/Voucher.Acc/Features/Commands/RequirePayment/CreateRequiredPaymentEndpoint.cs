using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.RequirePayment;

public class CreateRequiredPaymentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();
        api.MapPost("create-require-payment", CreateRequiredPayment)
            .WithSummary("Tạo đề nghị thanh toán");
    }

    private async Task<IResult> CreateRequiredPayment([AsParameters] VoucherServices services, CreateRequiPaymentDataModel request)
    {
        var command = new CreateRequiredPaymentCommand(request);
        
        var rsl = await services.Mediator.Send(command);

        return TypedResults.Ok(rsl);
    }
}