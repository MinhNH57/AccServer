using BuildingBlocks.Pagination.Version1;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Model.ProductPlant;

namespace Voucher.Acc.Features.Queries.GetProductionPlan;

public class GetProductionPlanEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("ProductionPlan/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("", FindAll)
            .WithName("ListProductionPlan")
            .WithSummary("Danh sách kế hoạch sx.")
            .WithTags("ProductionPlan");
        api.MapGet("{id:Guid}", FindOne)
           .WithName("DataProductionPlan")
           .WithTags("ProductionPlan");
    }

    private static async Task<Results<Ok<ApiResponse<List<SmartProductionPlan>>>, BadRequest<ApiResponse<string>>>> FindAll(
        [AsParameters] VoucherServices services,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        CancellationToken token)
    {
        var query = new GetProductionPlanQuery(filtering, sorting, pagination);
        var result = await services.Mediator.Send(query, token);
        // var response = ApiResponseFactory<List<SmartData>>.Ok(smartDatas);
        return TypedResults.Ok(result);
    }
    private static async Task<Ok<ApiResponse<SmartProductionPlan>>> FindOne(
   [AsParameters] VoucherServices services,
   //[FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
   [FromRoute] Guid id)
    {
        var smartProductionPlan = await services.Context.SmartProductionPlans
            .Include(x => x.SmartDataProductionPlans.OrderBy(y => y.CreateDate))
            .FirstOrDefaultAsync(x => x.Id == id);

        var response = ApiResponseFactory<SmartProductionPlan>.Ok(smartProductionPlan);

        return TypedResults.Ok(response);
    }
}