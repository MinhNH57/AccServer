using BuildingBlocks.Response;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Voucher.Acc.Features.Commands.CreateContentsSupplierEvaluation;

public class CreateContentsSupplierEvaluationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0)
            .RequireAuthorization();

        api.MapPost("/create-contents-supplier-evaluation", CreateContentsSupplierEvaluation)
            .WithSummary("Tạo nội dung kiểm định nhà cung cấp")
            .WithTags("Vouchers");
    }

    private async Task<Ok<Result>> CreateContentsSupplierEvaluation([AsParameters] VoucherServices services,
        CreateContentsSupplierEvaluationRequest request,
        CancellationToken token)
    {
        var command = new CreateContentsSupplierEvaluationCommand(request);

       var result =   await services.Mediator.Send(command, token);

       return TypedResults.Ok(result);
    }
}