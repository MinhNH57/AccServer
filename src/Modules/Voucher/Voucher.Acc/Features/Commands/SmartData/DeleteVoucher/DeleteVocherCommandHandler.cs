using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Infrastructure.StoredProcedures;

namespace Voucher.Acc.Features.Commands.SmartData.DeleteVoucher;

public class DeleteVocherCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser, SmartDataServices smartDataServices) : ICommandHandler<DeleteVocherCommand, Result>
{
    public async Task<Result> Handle(DeleteVocherCommand command, CancellationToken cancellationToken)
    {

        var deleteDataStore = new DeleteVoucherStore(command.TableName, "", command.Id.ToString(), "",
            currentUser.CodeUser!, currentUser.CodeUnit);
        var result = await smartDataServices.ExcuteNonQuery(deleteDataStore.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, deleteDataStore.Parameters);

        if (result <= 0)
        {
            return Result.Failure(new Error("400", "Deleted fail"));
        }
        //var removeData = await dbContext.SmartDatas
        //    .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken: cancellationToken);

        //if (removeData is null)
        //    return Result.Failure<bool>(new Error("404", "Not found"));

        //await dbContext.SmartDatas
        //    .Where(c => c.Id == command.Id)
        //    .ExecuteDeleteAsync(cancellationToken: cancellationToken);
        //await dbContext.SaveChangesAsync(cancellationToken);
        Log.Information("User: {0} Deleted voucher with Id: {1}", currentUser.CodeUser, command.Id);

        return Result.Success(true);
    }
}