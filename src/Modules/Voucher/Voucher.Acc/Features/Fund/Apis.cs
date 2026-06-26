using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Fund;

public class Apis : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/fund/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapPost("create-fund-money-paid", CreateFundMoneyPaid)
            .WithSummary("Tạo mới lần thu tiền từ phiếu dự thu")
            .WithTags("Fund");

    }

    private async Task<IResult> CreateFundMoneyPaid([AsParameters] VoucherServices services, CreatFundSmartMoneyPaidModel request, CancellationToken token)
    {
        var command = new CreateFundSmartMoneyPaidCommand(request);
        var result = await services.Mediator.Send(command, token);
        return TypedResults.Ok(result);
    }

}