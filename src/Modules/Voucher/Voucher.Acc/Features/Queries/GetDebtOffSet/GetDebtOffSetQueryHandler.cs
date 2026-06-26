using System.Linq.Dynamic.Core;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Features.Queries.GetVoucherContent;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;
using Voucher.Acc.Model.DebtOffSet;
using MapsterMapper;
using Voucher.Acc.Features.Commands.SmartData.CreateVoucher.Models;
namespace Voucher.Acc.Features.Queries.GetDebtOffSet;
public class GetDebtOffSetQueryHandler(VoucherDbContext dbContext) : IQueryHandler<GetDebtOffSetQuery, Result<List<SmartDebtOffSetContents>>>
{
    public async Task<Result<List<SmartDebtOffSetContents>>> Handle(GetDebtOffSetQuery query, CancellationToken cancellationToken)
    {
        var queryData = dbContext.SmartDebtOffSetContents
            .AsNoTracking()
            .Where(c => c.IdContents == query.IdVoucher);

        var data = await queryData.ToListAsync(cancellationToken: cancellationToken);

        return Result.Success(data);

    }
}
public class UpdateDebtOffSetCommandHandler(
    VoucherDbContext dbContext,
    IMapper mapper,
    ICurrentUser currentUser) : ICommandHandler<UpdateDebtOffSetCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateDebtOffSetCommand command, CancellationToken cancellationToken)
    {
        var smartDebtOffSet = await dbContext.SmartDebtOffSets.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.Request.Id, cancellationToken: cancellationToken);

        if (smartDebtOffSet is null)
            return Result.Failure<bool>(new Error("5", "Not found"));

        CalculateTotalMoney(command);


        var (lstUpdate, lstCreate, lstRemove) = await UpdateSmartContentProcess(command, cancellationToken);

        smartDebtOffSet = mapper.Map<SmartDebtOffSet>(command.Request.SmartDebtOffSet);
        smartDebtOffSet.SmartDebtOffSetContents.Clear();
        var lstSmartContentDb = mapper.Map<List<SmartDebtOffSetContents>>(lstUpdate);
        lstCreate.ForEach(c => c.IdContents = command.Request.Id);

        await using var trx = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.SmartDebtOffSets.Update(smartDebtOffSet);
        await dbContext.SmartDebtOffSetContents.AddRangeAsync(lstCreate, cancellationToken);
        dbContext.SmartDebtOffSetContents.UpdateRange(lstSmartContentDb);
        dbContext.SmartDebtOffSetContents.RemoveRange(lstRemove);
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
    private static void CalculateTotalMoney(UpdateDebtOffSetCommand command)
    {
        if (command.Request.SmartDebtOffSet.SmartDebtOffSetContents is not { Count: > 0 })
        {
            return;
        }

        decimal totalMoney = 0;
        //command.Request.SmartDebtOffSet.SmartDebtOffSetContents.ForEach(c =>
        //{
        //    totalMoney += c.AmountOfMoney ?? 0;
        //});
        //command.Request.SmartData.TotalMoney = (double)totalMoney;
    }



    private async Task<(List<SmartDebtOffSetContents> lstUpdate, List<SmartDebtOffSetContents> lstCreate, List<SmartDebtOffSetContents> lstRemove)> UpdateSmartContentProcess(
        UpdateDebtOffSetCommand command, CancellationToken cancellationToken)
    {
        var lstSmartContentDb = await dbContext.SmartDebtOffSetContents
            .AsNoTracking()
            .Where(c => c.IdContents == command.Request.Id)
            .ToListAsync(cancellationToken);

        var lstUpdate = command.Request.SmartDebtOffSet.SmartDebtOffSetContents
            .Where(c => lstSmartContentDb.Any(r => r.IdSource == c.IdSource))
            .ToList();

        var lstCreate = command.Request.SmartDebtOffSet.SmartDebtOffSetContents
            .Where(x => lstSmartContentDb.All(y => y.IdSource != x.IdSource))
            .ToList();

        var lstRemove = lstSmartContentDb
            .Where(y => command.Request.SmartDebtOffSet.SmartDebtOffSetContents.All(x => x.IdSource != y.IdSource))
            .ToList();

        return (lstUpdate, lstCreate, lstRemove);
    }

}