using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Voucher.Sgas.Infrastructure;

namespace Voucher.Sgas.Features.SalesData.GetSalesSmartData;

public record GetGetSaleSmartDataQuery(
    Guid Id) : IQuery<Result>;
public class GetSalesSmartDataHandler(VoucherSgasDbContext dbContext, ICurrentUser currentUser)
    : IQueryHandler<GetGetSaleSmartDataQuery, Result>
{

    public async Task<Result> Handle(GetGetSaleSmartDataQuery query, CancellationToken cancellationToken)
    {
        var saleSmart = await dbContext.SalesSmartData
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == query.Id,
                cancellationToken: cancellationToken);

        if (saleSmart is null) return Result.Failure(new Error("400", "Không tìm thấy phiếu mua hàng"));

        var saleSmartContents = await dbContext.SalesSmartContentsData
            .AsNoTracking()
            .Where(c => c.IdContents == saleSmart.Id).ToListAsync(cancellationToken); 

        var saleSmartIventoryContents = await dbContext.SalesSmartProductInventory
            .AsNoTracking()
            .Where(c => c.IdContents == saleSmart.Id).ToListAsync(cancellationToken); 
         
        var salesData = new Model.Response.SalesData()
        {
            SalesSmartData = saleSmart,
            SalesSmartContentsData = saleSmartContents,
            SalesSmartProductInventory = saleSmartIventoryContents
        };
        return Result.Success(salesData);

    }
}