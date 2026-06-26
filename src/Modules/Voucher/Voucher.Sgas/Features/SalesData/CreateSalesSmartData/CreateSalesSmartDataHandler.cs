using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.SmartMapper;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Voucher.Sgas.Entities;
using Voucher.Sgas.Infrastructure;
using Voucher.Sgas.Model.Sales;

namespace Voucher.Sgas.Features.SalesData.CreateSalesSmartData;
public record CreateSalesModelAction(
    SalesModelAction Model) : IQuery<Result>;
public class UpdateSalesSmartDataHandler(VoucherSgasDbContext dbContext, ICurrentUser currentUser, IMappingService mapping)
    : IQueryHandler<CreateSalesModelAction, Result>
{
    public async Task<Result> Handle(CreateSalesModelAction request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var headerData = await dbContext.SalesSmartData.FirstOrDefaultAsync(x =>
            x.NumberOfVouchers == request.Model.SmartHead.NumberOfVouchers, cancellationToken: cancellationToken);
        if (headerData is not null)
        {
            return Result.Failure(new Error(ErrorCode.Conflict, "Số chứng từ đã tồn tại"));
        }

        SalesSmartData headData = new();
        List<SalesSmartContentsData> contentsData = new();
        List<SalesSmartProductInventory> iventoryContentsData = new();

        headData = mapping.Map<SalesSmartDataDto, SalesSmartData>(request.Model.SmartHead);
        headData.CodeUnit = currentUser.CodeUnit;
        headData.Id = Guid.CreateVersion7(DateTimeOffset.Now);

        contentsData = mapping.MapList<SalesSmartContentsDataDto, SalesSmartContentsData>(request.Model.SmartContents);
        contentsData.ForEach(x =>
        {
            x.IdContents = headData.Id;
            x.DataType = headData.DataType;
        });

        iventoryContentsData = mapping.MapList<SalesSmartProductInventoryDto, SalesSmartProductInventory>(request.Model.SalesSmartProductInventory);
        iventoryContentsData.ForEach(x =>
        {
            x.IdContents = headData.Id; 
        });

        await dbContext.SalesSmartData.AddAsync(headData, cancellationToken);
        await dbContext.SalesSmartContentsData.AddRangeAsync(contentsData, cancellationToken);
        await dbContext.SalesSmartProductInventory.AddRangeAsync(iventoryContentsData, cancellationToken);

        var count = await dbContext.SaveChangesAsync(cancellationToken);

        if (count > 0)
        {
            var user = currentUser;
            Log.Information($"[ADD] User: {currentUser.CodeUser} added data in the table SalesSmartData: {headData.NumberOfVouchers}");

            return Result.Success(headData.Id);
        }
        return Result.Failure(new Error(ErrorCode.InternalServerError, "Lưu phiếu thất bại"));

    }
}
