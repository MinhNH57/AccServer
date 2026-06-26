using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Voucher.Sgas.Features.PumpCodes.UpdatePumpCode;
using Voucher.Sgas.Model.SmartDataPumpCode;

namespace Voucher.Sgas.Features.PumpCodes;

public class ApiPumpCode : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/sgas/pump/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPut("update-pump-code", UpdatePumpCode)
            .WithSummary("Cập nhật mã bơm.")
            .WithDescription("Cập nhật mã bơm.")
            .WithTags("SgasVoucher");
         
    }
    private async Task<IResult> UpdatePumpCode([AsParameters] VoucherServices services, UpdatePumpCodeRequest request, CancellationToken token)
    {
        var query = new UpdatePumpCodeCommand(request);
        var result = await services.Mediator.Send(query, token);
        return Results.Ok(result);
    } 
}