using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Report.Features.Mobile.Fund.FundContract;

public record SmartFundContractSummaryQuery() : ICommand<Result>;

//public class SmartFundContractSummaryQueryHandler (ReportDbContext dbContext) : ICommandHandler<SmartFundContractSummaryQuery, Result>
//{
//    public Task<Result> Handle(SmartFundContractSummaryQuery request, CancellationToken cancellationToken)
//    {

//    }
//}