using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.SalesTemp;

public record DeleteSalesSmartDataTempCommand(Guid IdVoucher) : ICommand<Result>;


public class DeleteSalesSmartDataTempCommandHandler(VoucherDbContext dbContext) : ICommandHandler<DeleteSalesSmartDataTempCommand, Result>
{
    public async Task<Result> Handle(DeleteSalesSmartDataTempCommand command, CancellationToken cancellationToken)
    {
        var data = await dbContext.SalesSmartDataTemp.FirstOrDefaultAsync(c => c.Id == command.IdVoucher,
            cancellationToken: cancellationToken);
        
        if(data is null)
            return Result.Failure(new Error("404", "Not found"));
        dbContext.SalesSmartDataTemp.Remove(data);

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(true);
    }
}