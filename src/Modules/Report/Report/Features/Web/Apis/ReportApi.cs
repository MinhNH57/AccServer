using Carter;
using Report.Features.Web.GetDataReport;
using Report.Infrastructure;
using Report.Models;

namespace Report.Features.Web.Apis;

public class ReportApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");
        var api = vApi.MapGroup("report/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPost("/get-report", GetReportWithStore)
            .WithName("GetReportWithStore")
            .WithSummary("Lấy báo cáo từ store.")
            .WithTags("Reports"); 
        api.MapPost("/get-report-v2", GetReportWithStoreV2)
            .WithSummary("Lấy báo cáo từ store.")
            .WithTags("Reports"); 
        api.MapPost("/get-arising-balance", GetListDataArisingBalanceWithStore)
            .WithName("GetListDataArisingBalanceWithStore")
            .WithSummary("Liệt kê chứng từ chi tiết theo số tiền của Bảng cân đối số phát sinh.")
            .WithTags("Reports");
        api.MapPost("/get-report-with-filter", GetReportWithFilterStore)
            .WithName("GetReportWithFilterStore")
            .WithSummary("Lấy báo cáo từ store với bộ lọc.")
            .WithTags("Reports");
    }

    private async Task<IResult> GetReportWithStore(
        [AsParameters] ReportService services,
        GetDataReportRequest request)
    {
        var command = new GetDataReportComnand(request);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }

    private async Task<IResult> GetReportWithStoreV2(
        [AsParameters] ReportService services,
        GetDataReportRequest request)
    {
        var command = new GetDataReportComnand(request);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }

    private async Task<IResult> GetReportWithFilterStore(
        [AsParameters] ReportService services,
        GetDataReportFilterRequest request)
    {
        var command = new GetReportWithFilterQuery(request);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    }
    /// <summary>
    /// Liệt kê chứng từ chi tiết theo số tiền của Bảng cân đối số phát sinh
    /// </summary>
    /// <param name="services"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    private async Task<IResult> GetListDataArisingBalanceWithStore(
        [AsParameters] ReportService services,
        GetDataReportRequest request)
    {
        var command = new GetListDataArisingBalanceCommand(request);
        var result = await services.Mediator.Send(command);

        return Results.Ok(result);
    } 
}
