using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.GenNoInv;

public class GenNoInvEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPost("gen-no-inv", GenNoInv)
            .WithName("GenNoInv")
            .WithSummary("Gen số chứng từ.")
            .WithTags("Vouchers");
    }

    private async Task<IResult> GenNoInv(
        [AsParameters] VoucherServices services,
        GenNoInvRequest request)
    {
        var command = new GenNoInvComnand(request);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }
}