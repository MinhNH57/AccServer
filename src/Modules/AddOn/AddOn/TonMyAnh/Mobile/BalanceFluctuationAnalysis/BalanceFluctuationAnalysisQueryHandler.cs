using AddOn.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;

namespace AddOn.TonMyAnh.Mobile.BalanceFluctuationAnalysis;

public class BalanceFluctuationAnalysisQueryHandler(AddOnDbContext dbContext) : IQueryHandler<BalanceFluctuationAnalysisQuery, Result<List<BalanceFluctuationAnalysisResponse>>>
{
    public async Task<Result<List<BalanceFluctuationAnalysisResponse>>> Handle(BalanceFluctuationAnalysisQuery query, CancellationToken cancellationToken)
    {
        var queryData = (from d in dbContext.DataBalanceFluctuations.AsNoTracking()
                         join c in dbContext.CatalogBankOfAccount.AsNoTracking()
                         on d.BankOfAccount equals c.AccountNumber
                         select new
                         {
                             c.AccountNumber,
                             c.BankName,
                             c.BankCode,
                             d.RecordDate,
                             d.Amount,
                             d.BankOfAccountReceive
                         });

        if (!string.IsNullOrEmpty(query.BankName))
        {
            queryData = queryData.Where(c => c.BankName == query.BankName);
        }

        if (!string.IsNullOrEmpty(query.BankOfAccountReceive))
        {
            queryData = queryData.Where(c => c.BankOfAccountReceive == query.BankOfAccountReceive);
        }

        if (query.StartDate is not null && query.EndDate is not null)
        {
            queryData = queryData.Where(c => c.RecordDate >= query.StartDate && c.RecordDate <= query.EndDate);
        }

        var result = await (from q in queryData
                            group q by new { q.BankName, q.AccountNumber, q.BankCode }
            into grp
                            select new BalanceFluctuationAnalysisResponse()
                            {
                                BankCode = grp.Key.BankCode,
                                BankName = grp.Key.BankName,
                                AccountNumber = grp.Key.AccountNumber,
                                TotalAmountDebit = grp
                                    .Where(c=>c.BankOfAccountReceive == "BAONO").Select(c=>c.Amount).Sum(),
                                TotalAmountCredit = grp
                                    .Where(c=>c.BankOfAccountReceive == "BAOCO").Select(c=>c.Amount).Sum(),
                            }).ToListAsync(cancellationToken: cancellationToken);

        return Result<List<BalanceFluctuationAnalysisResponse>>.Success(result);
    }
}