using Carter;
using Report.Features.Mobile.Fund.CollectionImplementation;
using Report.Features.Mobile.Fund.CompulsorySavings;
using Report.Features.Mobile.Fund.FundSavingsSummaryBook;
using Report.Features.Mobile.Fund.FunRepaymentPlan;

namespace Report.Features.Mobile.Fund.Apis;

public class FundReportApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Report");

        var api = vApi.MapGroup("report/fund/")
            .HasApiVersion(1.0);

        api.RequireAuthorization();

        api.MapGet("saving-contract", CreditContract)
            .WithSummary("Hợp đồng tín dụng, hợp đồng tiết kiệm")
            .WithTags("Fund");

        api.MapGet("fund-repayment-plan", FunRepaymentPlan)
            .WithSummary("Kế hoạch trả nợ gốc, lãi")
            .WithTags("Fund");

        api.MapGet("collection-implementation", CollectionImplementation)
            .WithSummary("Kế hoạch dự thu theo thời gian")
            .WithTags("Fund");

        api.MapGet("fund-contract", CompulsorySavings)
            .WithSummary("Tiết kiệm bắt buộc theo công đoàn")
            .WithTags("Fund");
    }

    private async Task<IResult> CompulsorySavings([AsParameters] ReportService service,
        [AsParameters] SmartCompulsorySavingsQuery request,
        CancellationToken token)
    {
        var result = await service.Mediator.Send(request, token);
        return TypedResults.Ok(result);
    }

    private async Task<IResult> CollectionImplementation([AsParameters] ReportService service, [AsParameters] CollectionImplementationQuery query)
    {
        var queryData = new CollectionImplementationQuery(query.Parameter, query.Code, query.BeginDate, query.EndDate,
            query.Type, query.FundSourceCode);

        var result = await service.Mediator.Send(queryData);
        return TypedResults.Ok(result);
    }

    private async Task<IResult> FunRepaymentPlan(string searchTerm, string? sortTerm, [AsParameters] ReportService service, CancellationToken token)
    {
        var query = new FunRepaymentPlanQuery(searchTerm, sortTerm);
        var result = await service.Mediator.Send(query, token);

        return TypedResults.Ok(result);
    }

    private async Task<IResult> CreditContract(
      [AsParameters] FundSavingsSummaryBookQuery query,
        [AsParameters] ReportService service, CancellationToken token)
    {
        var result = await service.Mediator.Send(query, token);

        return TypedResults.Ok(result);
    }


}