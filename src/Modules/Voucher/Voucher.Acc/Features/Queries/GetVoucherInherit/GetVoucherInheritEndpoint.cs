using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Queries.GetVoucherInherit;

public class GetVoucherInheritEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPost("inherit-voucher", GetVoucherInherit)
            .WithSummary("Lấy danh sách phiếu để kế thừa.")
            .WithTags("Vouchers");
    }
    private async Task<IResult> GetVoucherInherit([AsParameters] VoucherServices services,
        string datatype, GetVoucherInheritQuery request,
        CancellationToken token)
    {
        var result = await services.Mediator.Send(request, token);

        return Results.Ok(result);
    }
}