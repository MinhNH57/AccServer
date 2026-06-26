using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Queries.Sales.GetSalesVoucherContent;

public class GetSalesVoucherContentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("content-sales/{idVoucher}", GetSalesVoucherContent)
            .WithName("SalesVoucherContent")
            .WithSummary("Danh sách nội dung phiếu bán hàng.")
            .WithDescription("Nhận danh sách phiếu bán hàng.")
            .WithTags("SalesVouchers");
    }


    private async Task<IResult> GetSalesVoucherContent([AsParameters] VoucherServices services, Guid idVoucher, CancellationToken token)
    {
        var query = new GetSalesVoucherContentQuery(idVoucher);
        var result = await services.Mediator.Send(query, token);
        return Results.Ok(result);
    }
}
 