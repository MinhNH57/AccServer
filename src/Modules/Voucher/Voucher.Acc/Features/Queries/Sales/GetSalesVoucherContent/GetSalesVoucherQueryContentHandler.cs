using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.Sales.GetSalesVoucherContent;

public class GetSalesVoucherQueryContentHandler(VoucherDbContext dbContext) : IQueryHandler<GetSalesVoucherContentQuery, Result<List<SalesSmartContentsData>>>
{
    public async Task<Result<List<SalesSmartContentsData>>> Handle(GetSalesVoucherContentQuery query, CancellationToken cancellationToken)
    {
        var queryData = dbContext.SalesSmartContentsData
            .AsNoTracking()
            .Where(c => c.IdContents == query.IdVoucher);

        var data = await queryData.ToListAsync(cancellationToken: cancellationToken);

        return Result.Success(data);
    }
}