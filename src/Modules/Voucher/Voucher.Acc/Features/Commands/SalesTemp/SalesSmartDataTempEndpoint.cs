using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.SalesTemp;

public class SalesSmartDataTempEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();


        api.MapPost("create-sale-temp", CreateSalesTemp);
        api.MapPut("update-sale-temp", UpdateSalesTemp);
        api.MapDelete("delete-sale-temp", DeleteSalesTemp);

    }

    private async Task<IResult> DeleteSalesTemp([AsParameters] VoucherServices services, Guid idVoucher)
    {
        var request = new DeleteSalesSmartDataTempCommand(idVoucher);
        var result = await services.Mediator.Send(request);
        return Results.Ok(result);
    }

    private async Task<IResult> UpdateSalesTemp([AsParameters] VoucherServices services, UpdateSalesSmartDataTempCommand request)
    {
        var result = await services.Mediator.Send(request);
        return Results.Ok(result);
    }

    private async Task<IResult> CreateSalesTemp([AsParameters] VoucherServices services, CreateSalesSmartDataTempCommand request)
    {
        var result = await services.Mediator.Send(request);
        return Results.Ok(result);
    }
}