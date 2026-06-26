using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.GetVoucherContent;

public class GetVoucherContentQueryHandler(VoucherDbContext dbContext) : IQueryHandler<GetVoucherContentQuery, Result<List<SmartContentsData>>>
{
    public async Task<Result<List<SmartContentsData>>> Handle(GetVoucherContentQuery query, CancellationToken cancellationToken)
    {
        var queryData = dbContext.SmartContentsDatas
            .AsNoTracking()
            .Where(c => c.IdContents == query.IdVoucher);

        var data = await queryData.ToListAsync(cancellationToken: cancellationToken);

        return Result.Success(data);

    }
}