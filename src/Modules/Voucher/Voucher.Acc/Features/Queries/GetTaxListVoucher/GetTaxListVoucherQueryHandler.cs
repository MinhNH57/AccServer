using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.GetTaxListVoucher;

public class GetTaxListVoucherQueryHandler(VoucherDbContext dbContext) : IQueryHandler<GetTaxListVoucherQuery, Result<List<SmartVatTaxList>>>
{
    public async Task<Result<List<SmartVatTaxList>>> Handle(GetTaxListVoucherQuery query, CancellationToken cancellationToken)
    {
        var queryData = dbContext.SmartVatTaxList
            .AsNoTracking()
            .Where(c => c.IdContents == query.IdVoucher);

        return Result.Success(await queryData.ToListAsync(cancellationToken: cancellationToken));
    }
}