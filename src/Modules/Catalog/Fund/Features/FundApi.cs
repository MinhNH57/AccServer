using BuildingBlocks.Pagination.Version1;
using Carter;
using Catalog.Fund.Features.Commands;
using Catalog.Fund.Features.Queries;
using Catalog.Fund.Features.Queries.GetData;
using Catalog.Fund.Features.Queries.GetInfoMemmber;
using Catalog.Fund.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Fund.Features;

public class FundApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Fund");
        
        var api = vApi.MapGroup("fund/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("get-data/{tableName}", GetDynamicData)
            .WithName("GetData")
            .WithSummary("Lấy dữ liệu động");

        api.MapGet("get-object-by-cccd", GetObjectByCccdDetail)
            .WithSummary("Lấy chi tiết đối tượng bằng CCCD");

        api.MapPost("add-object-fund", AddObjectFund)
            .WithSummary("Tạo đơn khảo sát");

        api.MapPut("edit-object-fund", EditObjectFund)
            .WithSummary("Sửa đối tượng");

        api.MapDelete("delete-object-fund/{cccd}", DeleteObjectFund)
            .WithSummary("Xóa đối tượng");

        api.MapPost("get-info-member", GetInfoMemmber)
            .WithSummary("Lấy thông tin thành viên theo CCCD")
            .WithTags("Fund");

        api.MapGet("group-excel-obj", GroupExcelObject)
            .WithSummary("Nhóm đơn vay")
            .WithTags("Fund");

        api.MapPost("excel-object-fund", CreateExcelObject)
            .WithSummary("Thêm mới đơn đề nghị vay vốn")
            .WithTags("Fund");

        api.MapGet("excel-object-fund/{id}", GetExcelObjById)
            .WithSummary("Lấy chi tiết đối tượng bằng CCCD");
    }

    private async Task<IResult> GroupExcelObject([AsParameters] CatalogFundService service, string ids, string? type, CancellationToken token)
    {
        var command = new GroupExcelObjectCommand(type, ids);
        var result = await service.Mediator.Send(command, token);
        return TypedResults.Ok(result);
    }

    private async Task<IResult> GetExcelObjById([AsParameters] CatalogFundService service, Guid id, CancellationToken token)
    {
        var query = new GetExcelCatalogObjByIdQuery(id);
        var result = await service.Mediator.Send(query, token);

        return TypedResults.Ok(result);
    }

    [AllowAnonymous]
    private async Task<IResult> CreateExcelObject([AsParameters] CatalogFundService service, [AsParameters] CreateExcelCatalogObjectCommand requests)
    {
        var result = await service.Mediator.Send(requests);
        return TypedResults.Ok(result);
    }

    [AllowAnonymous]
    private async Task<IResult> GetDynamicData(
        [AsParameters] CatalogFundService service,
        [AsParameters] FilteringRequest filtering,
        [AsParameters] SortRequest sorting,
        [AsParameters] PaginationRequest pagination,
        string tableName,
        CancellationToken token)
    {
        var query = new GetDynamicDataQuery(filtering, sorting, pagination, tableName);
        var result = await service.Mediator.Send(query, token);
        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }
        return Results.Ok(result);
    }

    private async Task<IResult> DeleteObjectFund(
        [AsParameters] CatalogFundService service,
        string cccd,
        CancellationToken token)
    {
        var commnad = new DeleteObjectFundCommand(cccd);

        var result = await service.Mediator.Send(commnad, token);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }

    private async Task<IResult> EditObjectFund(
        [AsParameters] CatalogFundService service,
        ObjectDtoFundAction objectFund,
        CancellationToken token)
    {
        var commnad = new UpdateObjectFundCommand(objectFund);

        var result = await service.Mediator.Send(commnad, token);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }

    private async Task<IResult> AddObjectFund(
        [AsParameters] CatalogFundService service,
        ObjectDtoFundAction objectFund,
        CancellationToken token)
    {
        var commnad = new AddObjectFundCommand(objectFund);

        var result = await service.Mediator.Send(commnad, token);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }

    private async Task<IResult> GetObjectByCccdDetail(
        [AsParameters] CatalogFundService service,
        string cccd,
        CancellationToken token)
    {
        var query = new GetObjectByCccdQuery(cccd);
        var result = await service.Mediator.Send(query, token);

        if (result.IsSuccess)
            return Results.Ok(result);

        return Results.BadRequest(result);
    }

    private async Task<IResult> GetInfoMemmber(
        [AsParameters] CatalogFundService service,
        GetInfoMemmberQuery query,
        CancellationToken token)
    {
        var result = await service.Mediator.Send(query, token);

        return Results.Ok(result);
    }
}