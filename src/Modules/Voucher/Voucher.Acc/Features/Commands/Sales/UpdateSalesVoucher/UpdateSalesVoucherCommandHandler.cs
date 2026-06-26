using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.Sales.UpdateSalesVoucher;

public class UpdateSalesVoucherCommandHandler(
    VoucherDbContext dbContext,
    IMultiTenantContextAccessor tenantContextAccessor,
    IMapper mapper,
    ICurrentUser currentUser) : ICommandHandler<UpdateSalesVoucherCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateSalesVoucherCommand command, CancellationToken cancellationToken)
    {
        var smartData = await dbContext.SalesSmartData.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.Request.Id, cancellationToken: cancellationToken);

        if (smartData is null)
            return Result.Failure<bool>(new Error("404", "Not found"));

        var lstContent = await dbContext.SalesSmartContentsData
            .AsNoTracking()
            .Where(c => c.IdContents == command.Request.Id)
            .ToListAsync(cancellationToken: cancellationToken);

        var lstUpdate = command.Request.SalesSmartContentsDatas
            .Where(c => lstContent.Any(r => r.IdSource == c.IdSource))
            .ToList();

        var lstCreate = command.Request.SalesSmartContentsDatas!
            .Where(x => lstContent.All(y => y.IdSource != x.IdSource))
            .ToList();

        var lstRemove = lstContent
            .Where(y => command.Request.SalesSmartContentsDatas!.All(x => x.IdSource != y.IdSource))
            .ToList();
        smartData = mapper.Map<SalesSmartData>(command.Request.SalesSmartData);
        smartData.ModifiedBy = currentUser.CodeUser;
        smartData.ModifiedDate = DateTime.Now;
        //smartData.SalesSmartContentsDatas!.Clear();
        dbContext.SalesSmartData.Update(smartData);
        lstContent = mapper.Map<List<SalesSmartContentsData>>(lstUpdate);
        lstCreate.ForEach(c => c.IdContents = command.Request.Id);
        await dbContext.SalesSmartContentsData.AddRangeAsync(lstCreate, cancellationToken);
        dbContext.SalesSmartContentsData.UpdateRange(lstContent);
        dbContext.SalesSmartContentsData.RemoveRange(lstRemove);
        if (command.Request.SmartProductAttributeByOrders != null)
        {
            var lstManufacturing = await dbContext.SmartProductAttributeByOrders
         .AsNoTracking()
         .Where(c => c.IdContents == command.Request.Id)
         .ToListAsync(cancellationToken: cancellationToken);
            var lstManufacturingUpdate = command.Request.SmartProductAttributeByOrders
                .Where(c => lstManufacturing.Any(r => r.IdSource == c.IdSource))
                .ToList();

            var lstManufacturingCreate = command.Request.SmartProductAttributeByOrders!
                .Where(x => lstManufacturing.All(y => y.IdSource != x.IdSource))
                .ToList();

            var lstManufacturingRemove = lstManufacturing
                .Where(y => command.Request.SmartProductAttributeByOrders!.All(x => x.IdSource != y.IdSource))
                .ToList();
            lstManufacturing = mapper.Map<List<SmartProductAttributeByOrder>>(lstManufacturingUpdate);
            lstManufacturingCreate.ForEach(c => { c.IdContents = command.Request.Id;c.DataType=command.Request.SalesSmartData.DataType ; c.CodeUnit=currentUser.CodeUnit; });
            await dbContext.SmartProductAttributeByOrders.AddRangeAsync(lstManufacturingCreate, cancellationToken);
            lstManufacturing.ForEach(c => c.CodeUnit = currentUser.CodeUnit);
            dbContext.SmartProductAttributeByOrders.UpdateRange(lstManufacturing);
            dbContext.SmartProductAttributeByOrders.RemoveRange(lstManufacturingRemove);
        }
        var rsl = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        return Result.Success(true);
    }

}