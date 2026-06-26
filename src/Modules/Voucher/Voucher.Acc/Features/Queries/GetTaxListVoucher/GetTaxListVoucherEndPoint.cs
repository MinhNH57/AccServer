using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Queries.GetTaxListVoucher;

public class GetTaxListVoucherEndPoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("tax-list/{idVoucher}", GetListVatTax)
            .WithName("GetListVatTax")
            .WithSummary("Danh sách thuế GTGT.")
            .WithDescription("Nhận danh sách thuế GTGT.")
            .WithTags("Vouchers");
    }

    private async Task<IResult> GetListVatTax( 
        [AsParameters] VoucherServices services,
        Guid idVoucher, 
        CancellationToken token)
    {
        var query = new GetTaxListVoucherQuery(idVoucher);
        var result = await services.Mediator.Send(query, token);

        return Results.Ok(result);
    }
}