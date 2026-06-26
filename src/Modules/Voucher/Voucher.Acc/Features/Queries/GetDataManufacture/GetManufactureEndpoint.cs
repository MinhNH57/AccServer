using BuildingBlocks.Pagination.Version1;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.GetDataManufacture;

public class GetManufactureEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("Manufacture/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("", FindAll)
            .WithName("ListDataManufacture")
            .WithSummary("Danh sách yêu cầu.")
            .WithTags("Manufacture");
        api.MapGet("{id:Guid}", FindOne)
           .WithName("DataManufacture")
           .WithTags("Manufacture");
    }

    private static async Task<Results<Ok<ApiResponse<List<SmartDataManufacture>>>, BadRequest<ApiResponse<string>>>> FindAll(
        [AsParameters] VoucherServices services,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        CancellationToken token)
    {
        var query = new GetManufactureQuery(filtering, sorting, pagination);
        var result = await services.Mediator.Send(query, token);
        // var response = ApiResponseFactory<List<SmartData>>.Ok(smartDatas);
        return TypedResults.Ok(result);
    }
    private static async Task<Ok<ApiResponse<SmartDataManufacture>>> FindOne(
   [AsParameters] VoucherServices services,
   //[FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
   [FromRoute] Guid id)
    {
        var smartDataManufacture = await services.Context.SmartDataManufactures
            .Include(x => x.SmartDataManufactureContents.OrderBy(y => y.CreateDate))
            .Include(x => x.SmartDataBillOfMaterials)
            .FirstOrDefaultAsync(x => x.Id == id);

        var response = ApiResponseFactory<SmartDataManufacture>.Ok(smartDataManufacture);

        return TypedResults.Ok(response);
    }
}