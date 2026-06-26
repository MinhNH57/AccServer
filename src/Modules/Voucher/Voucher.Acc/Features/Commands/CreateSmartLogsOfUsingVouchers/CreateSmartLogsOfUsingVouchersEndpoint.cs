using BuildingBlocks.Response;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.CreateSmartLogsOfUsingVouchers;

public class CreateSmartLogsOfUsingVouchersEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPost("create-smart-logs-voucher", CreateSmartLogVoucher)
            .WithTags("Vouchers");

    }

    private async Task<Ok<Result>> CreateSmartLogVoucher([AsParameters] VoucherServices services, 
        SmartLogsOfUsingVouchers request, CancellationToken token)
    {
        var command = new CreateSmartLogsOfUsingVouchersCommand(request);

       var result = await services.Mediator.Send(command, token);

       return TypedResults.Ok(result);
    }
}