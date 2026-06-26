using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.HRM.Features.Queries.GetDataSynGrid;

public class GetDataSynGridQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("HRMVoucher");
        var api = vApi.MapGroup("HRMvouchers/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapPost("paging-filter", Handler)
            .WithSummary("Lấy voucher bằng Store").
            WithTags("HRMvouchers");
    }

    private async Task<IResult> Handler([AsParameters] VoucherService services,
        GetDataSynGridRequest request, CancellationToken clt)
    {
        var query = new GetDataSynGridQuery(request);

        var result = await services.Mediator.Send(query, clt);

        return TypedResults.Ok(result);
    }

}