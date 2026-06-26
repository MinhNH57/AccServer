using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.GetDataInheritVoucherType;

public class GetDataInheritVoucherTypeEndpont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapGet("/get-data-inheritvouchertype", GetDataInheritVoucherType)
            .WithTags("Vouchers");
    }

    private async Task<IResult> GetDataInheritVoucherType([AsParameters] VoucherServices services, [AsParameters] GetDataInheritVoucherTypeRequest request, CancellationToken token)
    {
        var command = new GetDataInheritVoucherTypeCommand(request);
        var result = await services.Mediator.Send(command, token);
        return Results.Ok(result);
    }
}