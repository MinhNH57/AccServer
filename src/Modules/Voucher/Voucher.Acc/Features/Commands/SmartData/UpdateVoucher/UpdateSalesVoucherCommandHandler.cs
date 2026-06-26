using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Features.Commands.SmartData.CreateVoucher.Models;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.SmartData.UpdateVoucher;

public class UpdateSalesVoucherCommandHandler(
    VoucherDbContext dbContext,
    IMapper mapper,
    ICurrentUser currentUser) : ICommandHandler<UpdateSalesVoucherCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateSalesVoucherCommand command, CancellationToken cancellationToken)
    {
        var smartData = await dbContext.SmartDatas.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.Request.Id, cancellationToken: cancellationToken);

        if (smartData is null)
            return Result.Failure<bool>(new Error("5", "Not found"));

        CalculateTotalMoney(command);


        var (lstUpdate, lstCreate, lstRemove) = await UpdateSmartContentProcess(command, cancellationToken);

        var (lstUpdateVatTaxList, lstCreateVatTaxList, lstRemoveVatTaxList) = await UpdateSmartVatProccess(command, cancellationToken);
        var (lstUpdatePaymentVendor, lstCreatePaymentVendor, lstRemovePaymentVendor) = await UpdateSmartPaymentVendor(command, cancellationToken);
        smartData = mapper.Map<Model.SmartData>(command.Request.SmartData);
        smartData.SmartContentsDatas.Clear();
        smartData.SmartVatTaxLists.Clear();
        smartData.SmartPaymentVendors.Clear();
        smartData.SmartFileAttaches.Clear();
        var lstSmartContentDb = mapper.Map<List<SmartContentsData>>(lstUpdate);
        var lstVatTaxList = mapper.Map<List<SmartVatTaxList>>(lstUpdateVatTaxList);
        var lstPaymentVendors = mapper.Map<List<SmartPaymentVendor>>(lstUpdatePaymentVendor);
        lstCreate.ForEach(c => c.IdContents = command.Request.Id);
        lstCreateVatTaxList.ForEach(c => c.IdContents = command.Request.Id);
        lstCreatePaymentVendor.ForEach(c => c.IdContents = command.Request.Id);
        await using var trx = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.SmartDatas.Update(smartData);
        await dbContext.SmartContentsDatas.AddRangeAsync(lstCreate, cancellationToken);
        dbContext.SmartContentsDatas.UpdateRange(lstSmartContentDb);
        dbContext.SmartContentsDatas.RemoveRange(lstRemove);
        await dbContext.SmartVatTaxList.AddRangeAsync(lstCreateVatTaxList, cancellationToken);
        dbContext.SmartVatTaxList.UpdateRange(lstVatTaxList);
        dbContext.SmartVatTaxList.RemoveRange(lstRemoveVatTaxList);
        await dbContext.SmartPaymentVendors.AddRangeAsync(lstCreatePaymentVendor, cancellationToken);
        dbContext.SmartPaymentVendors.UpdateRange(lstPaymentVendors);
        dbContext.SmartPaymentVendors.RemoveRange(lstRemovePaymentVendor);
        try
        {
            var rsl = await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
        catch (DbUpdateException ex)
        {

            foreach (var entry in ex.Entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}");
                foreach (var prop in entry.CurrentValues.Properties)
                {
                    Console.WriteLine($"{prop.Name} = {entry.CurrentValues[prop]}");
                }
            }
            throw;
        }

        await trx.CommitAsync(cancellationToken);
        return Result.Success(true);
    }
    private static void CalculateTotalMoney(UpdateSalesVoucherCommand command)
    {
        if (command.Request.SmartData.SmartContentsDatas is not { Count: > 0 })
        {
            return;
        }

        decimal totalMoney = 0;
        command.Request.SmartData.SmartContentsDatas.ForEach(c =>
        {
            totalMoney += c.AmountOfMoney ?? 0;
        });
        command.Request.SmartData.TotalMoney = (double)totalMoney;
    }

    private async Task<(List<SmartVatTaxList> lstUpdateVatTaxList, List<SmartVatTaxList> lstCreateVatTaxList, List<SmartVatTaxList> lstRemoveVatTaxList)>
        UpdateSmartVatProccess(UpdateSalesVoucherCommand command, CancellationToken cancellationToken)
    {
        var lstVatTaxList = await dbContext.SmartVatTaxList
            .AsNoTracking()
            .Where(c => c.IdContents == command.Request.Id)
            .ToListAsync(cancellationToken: cancellationToken);

        var lstUpdateVatTaxList = (command.Request.SmartData.SmartVatTaxLists ?? [])
            .Where(c => lstVatTaxList.Any(r => r.IdSource == c.IdSource))
            .ToList();

        var lstCreateVatTaxList = (command.Request.SmartData.SmartVatTaxLists ?? [])
            .Where(x => lstVatTaxList.All(y => y.IdSource != x.IdSource))
            .ToList();

        var lstRemoveVatTaxList = lstVatTaxList
            .Where(y => command.Request.SmartData.SmartVatTaxLists.All(x => x.IdSource != y.IdSource))
            .ToList();

        return (lstUpdateVatTaxList, lstCreateVatTaxList, lstRemoveVatTaxList);
    }

    private async Task<(List<SmartContentsData> lstUpdate, List<SmartContentsData> lstCreate, List<SmartContentsData> lstRemove)> UpdateSmartContentProcess(
        UpdateSalesVoucherCommand command, CancellationToken cancellationToken)
    {
        var lstSmartContentDb = await dbContext.SmartContentsDatas
            .AsNoTracking()
            .Where(c => c.IdContents == command.Request.Id)
            .ToListAsync(cancellationToken);

        var lstUpdate = command.Request.SmartData.SmartContentsDatas
            .Where(c => lstSmartContentDb.Any(r => r.IdSource == c.IdSource))
            .ToList();

        var lstCreate = command.Request.SmartData.SmartContentsDatas
            .Where(x => lstSmartContentDb.All(y => y.IdSource != x.IdSource))
            .ToList();

        var lstRemove = lstSmartContentDb
            .Where(y => command.Request.SmartData.SmartContentsDatas.All(x => x.IdSource != y.IdSource))
            .ToList();

        return (lstUpdate, lstCreate, lstRemove);
    }
    private async Task<(List<SmartPaymentVendor> lstUpdatePaymentVendor, List<SmartPaymentVendor> lstCreatePaymentVendor, List<SmartPaymentVendor> lstRemovePaymentVendor)>
      UpdateSmartPaymentVendor(UpdateSalesVoucherCommand command, CancellationToken cancellationToken)
    {
        var lstPaymentVendor = await dbContext.SmartPaymentVendors
            .AsNoTracking()
            .Where(c => c.IdContents == command.Request.Id)
            .ToListAsync(cancellationToken: cancellationToken);

        var lstUpdatePaymentVendor = (command.Request.SmartData.SmartPaymentVendors ?? [])
            .Where(c => lstPaymentVendor.Any(r => r.IdSource == c.IdSource))
            .ToList();

        var lstCreatePaymentVendor = (command.Request.SmartData.SmartPaymentVendors ?? [])
            .Where(x => lstPaymentVendor.All(y => y.IdSource != x.IdSource))
            .ToList();

        var lstRemovePaymentVendor = lstPaymentVendor
            .Where(y => command.Request.SmartData.SmartPaymentVendors.All(x => x.IdSource != y.IdSource))
            .ToList();

        return (lstUpdatePaymentVendor, lstCreatePaymentVendor, lstRemovePaymentVendor);
    }
}