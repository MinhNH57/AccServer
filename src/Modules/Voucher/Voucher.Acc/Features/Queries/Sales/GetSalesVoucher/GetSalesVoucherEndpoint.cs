using BuildingBlocks.Pagination.Version1;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.Sales.GetSalesVoucher;

public class GetSalesVoucherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("sales", FindAll)
            .WithName("ListSalesVoucher")
            .WithSummary("Danh sách phiếu bán hàng.")
            .WithDescription("Nhận danh sách phiếu được phân trang.")
            .WithTags("SalesVouchers");
    } 

    private static async Task<Results<Ok<ApiResponse<List<SalesSmartData>>>, BadRequest<ApiResponse<string>>>> FindAll(
        [AsParameters] VoucherServices services,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        CancellationToken token)
    {
        var query = new GetSalesVoucherQuery(filtering, sorting, pagination);
        var result = await services.Mediator.Send(query, token);
        // var response = ApiResponseFactory<List<SmartData>>.Ok(smartDatas);
        return TypedResults.Ok(result);
    }
}
 