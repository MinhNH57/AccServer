using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.SalesTemp;

public record UpdateSalesSmartDataTempCommand(SalesSmartDataTemp SalesSmartDataTemp,
    List<SalesSmartContentsDataTemp> SalesSmartContentsDataTemps) : ICommand<Result>;

public class UpdateSalesSmartDataTempCommandHandler(VoucherDbContext dbContext, IMapper mapper) : ICommandHandler<UpdateSalesSmartDataTempCommand, Result>
{
    public async Task<Result> Handle(UpdateSalesSmartDataTempCommand command, CancellationToken cancellationToken)
    {
        var data = await dbContext.SalesSmartDataTemp
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.SalesSmartDataTemp.Id, cancellationToken: cancellationToken);

        if (data is null)
            return Result.Failure(new Error("404", "Not found"));

        data = mapper.Map<SalesSmartDataTemp>(command.SalesSmartDataTemp);

        var lstContent = await dbContext.SalesSmartContentsDataTemp
            .AsNoTracking()
            .Where(c => c.IdContents == command.SalesSmartDataTemp.Id)
            .ToListAsync(cancellationToken: cancellationToken);

        var lstUpdate = command.SalesSmartContentsDataTemps
            .Where(c => lstContent.Any(r => r.IdAsc == c.IdAsc))
            .ToList();

        var lstCreate = command.SalesSmartContentsDataTemps
            .Where(x => lstContent.All(y => y.IdAsc != x.IdAsc))
            .ToList();

        var lstRemove = lstContent
            .Where(y => command.SalesSmartContentsDataTemps.All(x => x.IdAsc != y.IdAsc))
            .ToList();

        lstContent = mapper.Map<List<SalesSmartContentsDataTemp>>(lstUpdate);
        lstCreate.ForEach(c => c.IdContents = command.SalesSmartDataTemp.Id);
        dbContext.SalesSmartDataTemp.Update(data);
        await dbContext.SalesSmartContentsDataTemp.AddRangeAsync(lstCreate, cancellationToken);
        dbContext.SalesSmartContentsDataTemp.UpdateRange(lstContent);
        dbContext.SalesSmartContentsDataTemp.RemoveRange(lstRemove);

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(true);
    }
}