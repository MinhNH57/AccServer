using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.Sales.DeleteSalesVoucher;

public class DeleteSalesVocherCommandHandler(VoucherDbContext dbContext) : ICommandHandler<DeleteSalesVocherCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteSalesVocherCommand command, CancellationToken cancellationToken)
    {
        var removeData = await dbContext.SalesSmartData
            .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken: cancellationToken);

        if (removeData is null)
            return Result.Failure<bool>(new Error("404", "Not found"));

        await dbContext.SalesSmartData
            .Where(c => c.Id == command.Id)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(true);
    }
}