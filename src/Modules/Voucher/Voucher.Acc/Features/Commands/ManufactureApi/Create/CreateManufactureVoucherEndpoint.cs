using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.ManufactureApi.Create;

public class CreateManufactureVoucherCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPost("create-manufacture-voucher", CreateVoucher)
            .WithTags("Vouchers");
    }

    private async Task<IResult> CreateVoucher(
        [AsParameters] VoucherServices services,
        List<DynamicListDataModel> dynamicData,
        CancellationToken token)
    {
        var command = new CreateManufactureVoucherCommand(dynamicData);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }


}