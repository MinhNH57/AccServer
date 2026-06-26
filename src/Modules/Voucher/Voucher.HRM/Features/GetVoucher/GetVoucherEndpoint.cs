using BuildingBlocks.Pagination.Version1;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Voucher.HRM.Entities;

namespace Voucher.HRM.Features.Queries.GetVoucher;

public class GetVoucherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("VoucherHRM");
        var api = vApi.MapGroup("HRMvoucher/").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("", FindAll)
            .WithName("HRMListVoucher")
            .WithSummary("Danh sách phiếu.")
            .WithDescription("Nhận danh sách phiếu được phân trang.")
            .WithTags("HRMVouchers");
    }

    private static async Task<Results<Ok<ApiResponse<List<SmartDataApplication>>>, BadRequest<ApiResponse<string>>>> FindAll(
    [AsParameters] VoucherService services,
    [AsParameters] FilteringRequest filtering,
    [AsParameters] SortRequest sorting,
    [AsParameters] PaginationRequest pagination,
    CancellationToken token)
    {
        var query = new GetVoucherQuery(filtering, sorting, pagination);
        var result = await services.Mediator.Send(query, token);
        return TypedResults.Ok(result);
    }
}