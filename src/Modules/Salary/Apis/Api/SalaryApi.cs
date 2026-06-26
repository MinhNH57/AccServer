using BuildingBlocks.Web;
using Carter;
using Salary.Apis.Commands;
using Salary.Apis.Commands.SalaryTimeSheet;
using Salary.Dto.SalaryTimeSheet;
using Salary.Request;
using System.Net.WebSockets;
using static Dapper.SqlMapper;

namespace Salary.Apis.Api;

public class SalaryApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Salary");

        var api = vApi.MapGroup("salary/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPut("edit-salary-time-sheet", UpdateSalaryTimeSheet)
                     .WithSummary("Sửa bảng chấm công.")
                     .WithDescription("Sửa bảng chấm công theo tháng.")
            .WithTags("Salary");

        api.MapPut("edit-salary-time-sheet-summary-details", UpdateSalaryTimeSheetDetails)
            .WithSummary("Sửa bảng công tổng hợp.")
            .WithDescription("Sửa bảng công tổng hợp.")
            .WithTags("Salary");

        api.MapPost("create-salary-time-sheet-may", CreateSalaryTimeSheetMay)
            .WithSummary("Tạo mới bảng lương may mặc.")
            .WithDescription("Tạo mới bảng lương may mặc.")
            .WithTags("Salary");

        api.MapPost("get-salary-time-sheet-may-by-condition", GetSalaryTimeSheetMayByContidion)
            .WithSummary("Get bảng lương may mặc chi tiết.")
            .WithDescription("Get bảng lương may mặc chi tiết.")
            .WithTags("Salary");

        api.MapPut("edit-salary-time-sheet-may", UpdateSalaryTimeSheetMay)
            .WithSummary("Chỉnh sửa bảng lương may mặc.")
            .WithDescription("Chỉnh sửa bảng lương may mặc.")
            .WithTags("Salary");

        api.MapGet("get-salary-time-sheet-may/{Id:guid}", GetSalaryTimeSheetMay)
            .WithSummary("Get bảng lương may mặc.")
            .WithDescription("Get bảng lương may mặc.")
            .WithTags("Salary");
    }

    private async Task<IResult> UpdateSalaryTimeSheetDetails(
       [AsParameters] SalaryServices service,
       UpdateSalaryTimeSheetSummaryDetailsCommand request,
        CancellationToken token)
    {
        var resut = await service.Mediator.Send(request, token);
        return Results.Ok(resut);
    }

    private async Task<IResult> CreateSalaryTimeSheetMay(
       [AsParameters] SalaryServices service,
       SalaryTimeSheetRequest createData,
        CancellationToken token)
    {
        //SalaryTimeSheetRequest
        var request = new CreateSalaryTimeSheetCommand(createData);
        var resut = await service.Mediator.Send(request, token);
        return Results.Ok(resut);
    }

    private async Task<IResult> GetSalaryTimeSheetMayByContidion(
        [AsParameters] SalaryServices service,
        GetSalaryTimeSheetByCondition requestCondition,
        CancellationToken token)
    {
        //SalaryTimeSheetRequest
        var request = new GetSalaryTimeSheetByConditionCommand(requestCondition);
        var resut = await service.Mediator.Send(request, token);
        return Results.Ok(resut);
    }

    private async Task<IResult> UpdateSalaryTimeSheetMay(
        [AsParameters] SalaryServices service,
        SalaryTimeSheetRequest updateData,
        CancellationToken token)
    {
        //SalaryTimeSheetRequest
        var request = new UpdateSalaryTimeSheetDetaitCommand(updateData);
        var resut = await service.Mediator.Send(request, token);
        return Results.Ok(resut);
    }

    private async Task<IResult> UpdateSalaryTimeSheet(
       [AsParameters] SalaryServices service,
       UpdateSalaryTimeSheetCommand request,
        CancellationToken token)
    {
        var resut = await service.Mediator.Send(request, token);
        return Results.Ok(resut);
    }
    private async Task<IResult> GetSalaryTimeSheetMay(
        [AsParameters] SalaryServices service,
        Guid Id,
        CancellationToken token)
    {
        var query = new FindSalaryTimeSheetQuery(Id);
        var resut = await service.Mediator.Send(query, token);
        return Results.Ok(resut);
    }
}