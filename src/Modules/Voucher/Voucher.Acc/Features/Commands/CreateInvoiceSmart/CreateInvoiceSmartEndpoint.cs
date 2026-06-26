using BuildingBlocks.Response;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.CreateInvoiceSmart;

public class CreateInvoiceSmartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapPost("/create-invoice-smart", CreateInvoiceSmart)
            .WithTags("Vouchers");
    }

    private async Task<Results<Created<Result>, BadRequest<Result>>> CreateInvoiceSmart([AsParameters] VoucherServices services, CreateInvoiceSmartCommand request, CancellationToken token)
    {
        var result = await services.Mediator.Send(request, token);

        if (result.IsSuccess)
            return TypedResults.Created("", result);
        return TypedResults.BadRequest(result);
    }
}