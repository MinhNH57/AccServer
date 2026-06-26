using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Queries.GetDataSynGrid;

public class GetDataSynGridQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("vouchers/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapPost("paging-filter", Handler)
            .WithSummary("Lấy voucher bằng Store").
            WithTags("Vouchers");
    }

    private async Task<IResult> Handler([AsParameters] VoucherServices services,
        GetDataSynGridRequest request, CancellationToken clt)
    {
        var query = new GetDataSynGridQuery(request);

        var result = await services.Mediator.Send(query, clt);

        return TypedResults.Ok(result);
    }

}