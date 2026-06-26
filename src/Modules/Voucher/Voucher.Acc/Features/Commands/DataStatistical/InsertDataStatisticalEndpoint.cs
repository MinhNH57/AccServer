using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.DataStatistical;

public class InsertDataStatisticalEndpoint : ICarterModule
{

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("production/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPost("insert-data-statistical", Handler)
            .WithSummary("Lấy dữ liệu thống kê sản xuất")
            .WithTags("ProductionPlan");
    }
    
    private async Task<IResult> Handler([AsParameters] VoucherServices voucherServices, InsertDataStatisticalCommand request, CancellationToken clt)
    {
        var result = await voucherServices.Mediator.Send(request, clt);

        return TypedResults.Ok(result);
    }
}