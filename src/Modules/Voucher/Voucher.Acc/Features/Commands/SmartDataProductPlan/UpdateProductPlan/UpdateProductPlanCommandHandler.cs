

using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model.ProductPlant;

namespace Voucher.Acc.Features.Commands.SmartDataProductPlan.UpdateProductPlan;


public class UpdateProductPlanCommandHandler(
    VoucherDbContext dbContext,
    IMapper mapper,
    ICurrentUser currentUser) : ICommandHandler<UpdateProductPlanCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateProductPlanCommand command, CancellationToken cancellationToken)
    {
        var smartData = await dbContext.SmartProductionPlans.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.Request.Id, cancellationToken: cancellationToken);

        if (smartData is null)
            return Result.Failure<bool>(new Error("5", "Not found"));


        var (lstUpdate, lstCreate, lstRemove) = await UpdateSmartContentProcess(command, cancellationToken);

        smartData = mapper.Map<Model.ProductPlant.SmartProductionPlan>(command.Request.SmartProductionPlan);
        smartData.SmartDataProductionPlans.Clear();
        smartData.CreateBy = currentUser.CodeUser;
        var lstSmartContentDb = mapper.Map<List<SmartDataProductionPlan>>(lstUpdate);

        lstCreate.ForEach(c => c.IdData = command.Request.Id);

        await using var trx = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.SmartProductionPlans.Update(smartData);
        await dbContext.SmartDataProductionPlans.AddRangeAsync(lstCreate, cancellationToken);
        dbContext.SmartDataProductionPlans.UpdateRange(lstSmartContentDb);
        dbContext.SmartDataProductionPlans.RemoveRange(lstRemove);
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

   

    private async Task<(List<SmartDataProductionPlan> lstUpdate, List<SmartDataProductionPlan> lstCreate, List<SmartDataProductionPlan> lstRemove)> UpdateSmartContentProcess(
        UpdateProductPlanCommand command, CancellationToken cancellationToken)
    {
        var lstSmartContentDb = await dbContext.SmartDataProductionPlans
            .AsNoTracking()
            .Where(c => c.IdData == command.Request.Id)
            .ToListAsync(cancellationToken);

        var lstUpdate = command.Request.SmartProductionPlan.SmartDataProductionPlans
            .Where(c => lstSmartContentDb.Any(r => r.IdSource == c.IdSource))
            .ToList();

        var lstCreate = command.Request.SmartProductionPlan.SmartDataProductionPlans
            .Where(x => lstSmartContentDb.All(y => y.IdSource != x.IdSource))
            .ToList();

        var lstRemove = lstSmartContentDb
            .Where(y => command.Request.SmartProductionPlan.SmartDataProductionPlans.All(x => x.IdSource != y.IdSource))
            .ToList();

        return (lstUpdate, lstCreate, lstRemove);
    }
}