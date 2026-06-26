using BuildingBlocks.Response;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.CreateSmartContentsQuanlityControl;

public class SmartContentsQuanlityControlEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPost("create-data-quanlity-control", CreateDataQuantityControl)
            .WithSummary("Tạo mới phiếu QC sản phẩm.")
            .WithTags("Vouchers");
    }

    private async Task<Ok<Result>> CreateDataQuantityControl([AsParameters] VoucherServices services, 
        QuanlityControlRequest request)
    {
        var command = new CreateSmartContentsQuanlityControlCommand(request);

        var result = await services.Mediator.Send(command);

        return TypedResults.Ok(result);
    }
}