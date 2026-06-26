using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Queries.GetVoucherContent;

public class GetVoucherContentEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("content/{idVoucher}", GetVoucherContent)
            .WithName("ContentVoucher")
            .WithSummary("Danh sách nội dung phiếu.")
            .WithDescription("Nhận danh sách nội dung phiếu.")
            .WithTags("Vouchers");
    }

    private async Task<IResult> GetVoucherContent([AsParameters] VoucherServices services,
        Guid idVoucher,
        CancellationToken token)
    {
        var query = new GetVoucherContentQuery(idVoucher);

        var result = await services.Mediator.Send(query, token);

        return Results.Ok(result);
    }
}