using Carter;

namespace AddOn.TonMyAnh.Mobile.BalanceFluctuationAnalysis;

public class BalanceFluctuationAnalysisEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Steel");

        var api = vApi.MapGroup("steels").HasApiVersion(1.0);

        api.RequireAuthorization();

        api.MapGet("/analysis-balance-fluctuation", BalanceFluctuationAnalysis)
            .WithSummary("Tổng hợp thu chi theo BĐSD");

    }

    //[AllowAnonymous]
    private async Task<IResult> BalanceFluctuationAnalysis(
        [AsParameters] AddOnService service,
        [AsParameters] BalanceFluctuationAnalysisQuery query,
        CancellationToken token)
    {
        var result = await service.Mediator.Send(query, token);

        return Results.Ok(result);
    }
}