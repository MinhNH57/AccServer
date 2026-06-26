using BuildingBlocks.Pagination.Version1;
using Financial.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Financial.Apis;

public static class FinancialApi
{
    public static RouteGroupBuilder MapFinancialApiV1(this IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Financial");
        var api = vApi.MapGroup("financial/").HasApiVersion(1.0).RequireAuthorization();

        // Routes for querying object.

        api.MapGet("ReportBalanceSheet/{id:guid}", FindOne);
        api.MapGet("ReportBalanceSheet", FindMany);

        return api;
    }

    private static async Task<Ok<ApiResponse<ReportBalanceSheet>>> FindOne(
    [AsParameters] FinancialServices services,
    [FromRoute] int id)
    {
        var reportBalanceSheet = await services.Context.ReportBalanceSheets
            .FirstOrDefaultAsync(x => x.Id == id);

        var response = ApiResponseFactory<ReportBalanceSheet>.Ok(reportBalanceSheet);

        return TypedResults.Ok(response);
    }
    private static async Task<Ok<ApiResponse<List<ReportBalanceSheet>>>> FindMany(
        [AsParameters] FinancialServices services,
        [AsParameters] PaginationRequest paginationRequest)
    {
        var page = paginationRequest.Page ?? 1;
        var pageSize = paginationRequest.PageSize ?? 20;

        if (page < 1) page = 1;
        if (pageSize < 1) page = 1;

        var root = (IQueryable<ReportBalanceSheet>)services.Context.ReportBalanceSheets;

        var totalItems = await root
            .LongCountAsync();

        var itemsOnPage = await root
            .OrderBy(c => c.CodeReport)
            .Skip(pageSize * page)
            .Take(pageSize)
            .ToListAsync();

        var response = ApiResponseFactory<List<ReportBalanceSheet>>.Ok(itemsOnPage, "financial/ReportBalanceSheet", totalItems, page, pageSize);

        return TypedResults.Ok(response);
    }


}
