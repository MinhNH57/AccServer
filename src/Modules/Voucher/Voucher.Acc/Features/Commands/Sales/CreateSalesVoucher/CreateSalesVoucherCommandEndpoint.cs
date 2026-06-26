using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.Sales.CreateSalesVoucher;

public class CreateSalesVoucherCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();
        
        api.MapPost("create-sales-voucher", CreateVoucher)
            .WithTags("Vouchers");
    }

    private async Task<IResult> CreateVoucher(
        [AsParameters] VoucherServices services,
        List<DynamicListDataModel> dynamicData,
        CancellationToken token)
    {
        var command = new CreateSalesVoucherCommand(dynamicData);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }

}