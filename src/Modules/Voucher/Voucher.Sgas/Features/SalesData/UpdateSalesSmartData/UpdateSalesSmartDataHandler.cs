using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.SmartMapper;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Voucher.Sgas.Entities;
using Voucher.Sgas.Infrastructure;
using Voucher.Sgas.Model.Sales;

namespace Voucher.Sgas.Features.SalesData.UpdateSalesSmartData;
public record UpdateSalesModelAction(
    SalesModelAction Model) : IQuery<Result>;
public class UpdateSalesSmartDataHandler(VoucherSgasDbContext dbContext, ICurrentUser currentUser, IMappingService mapping)
    : IQueryHandler<UpdateSalesModelAction, Result>
{
    public async Task<Result> Handle(UpdateSalesModelAction request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var headerData = await dbContext.SalesSmartData.FirstOrDefaultAsync(x =>
            x.Id == request.Model.IdVoucher, cancellationToken: cancellationToken);
        if (headerData is null)
        {
            return Result.Failure(new Error(ErrorCode.NotFound, "Không tìm thấy phiếu"));
        }

        mapping.MapExistingModels(request.Model.SmartHead, headerData);

        // Sales Content
        var lstContent = await dbContext.SalesSmartContentsData
            .AsNoTracking()
            .Where(c => c.IdContents == headerData.Id)
            .ToListAsync(cancellationToken: cancellationToken);


        var lstUpdate = request.Model.SmartContents
            .Where(c => lstContent.Any(r => r.Id == c.Id))
            .ToList();

        var lstCreate = request.Model.SmartContents!
            .Where(x => lstContent.All(y => y.Id != x.Id))
            .ToList();

        var lstRemove = lstContent
            .Where(y => request.Model.SmartContents!.All(x => x.Id != y.Id))
            .ToList();

        mapping.MapListExistingUpdate(lstContent, lstUpdate, x => x.Id, x => x.Id);


        // Iventory Content
        var lstContentIventory = await dbContext.SalesSmartProductInventory
            .AsNoTracking()
            .Where(c => c.IdContents == headerData.Id)
            .ToListAsync(cancellationToken: cancellationToken);


        var lstUpdateIventory = request.Model.SalesSmartProductInventory
            .Where(c => lstContentIventory.Any(r => r.Id == c.Id))
            .ToList();

        var lstCreateIventory = request.Model.SalesSmartProductInventory!
            .Where(x => lstContentIventory.All(y => y.Id != x.Id))
            .ToList();

        var lstRemoveIventory = lstContentIventory
            .Where(y => request.Model.SalesSmartProductInventory!.All(x => x.Id != y.Id))
            .ToList();

        mapping.MapListExistingUpdate(lstContentIventory, lstUpdateIventory, x => x.Id, x => x.Id);

        //foreach (var salesSmartContentsData in lstRemove)
        //{
        //    lstContent.Remove(salesSmartContentsData);
        //}

        var contentCreate = mapping.MapList<SalesSmartContentsDataDto, SalesSmartContentsData>(lstCreate);

        contentCreate.ForEach(c =>
        {
            c.IdContents = headerData.Id;
            c.DataType = headerData.DataType;
        });

        var contentCreateIventory = mapping.MapList<SalesSmartProductInventoryDto, SalesSmartProductInventory>(lstCreateIventory);

        contentCreateIventory.ForEach(c =>
        {
            c.IdContents = headerData.Id; 
        });


        dbContext.SalesSmartData.Update(headerData);

        dbContext.SalesSmartContentsData.UpdateRange(lstContent);

        dbContext.SalesSmartProductInventory.UpdateRange(lstContentIventory);

        await dbContext.SalesSmartContentsData.AddRangeAsync(contentCreate, cancellationToken);

        await dbContext.SalesSmartProductInventory.AddRangeAsync(contentCreateIventory, cancellationToken);

        dbContext.SalesSmartContentsData.RemoveRange(lstRemove);

        dbContext.SalesSmartProductInventory.RemoveRange(lstRemoveIventory);


        var count = await dbContext.SaveChangesAsync(cancellationToken);

        if (count > 0)
        {
            var user = currentUser;
            Log.Information($"[ADD] User: {currentUser.CodeUser} added update in the table SalesSmartData: {headerData.NumberOfVouchers}");

            return Result.Success(headerData.Id);
        }
        return Result.Failure(new Error(ErrorCode.InternalServerError, "Lưu phiếu thất bại"));

    }
}
