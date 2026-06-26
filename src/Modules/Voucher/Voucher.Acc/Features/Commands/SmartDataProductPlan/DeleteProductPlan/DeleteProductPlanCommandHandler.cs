using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Infrastructure.StoredProcedures;

namespace Voucher.Acc.Features.Commands.SmartDataProductPlan.DeleteProductPlan;

public class DeleteProductPlanCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser, SmartDataServices smartDataServices) : ICommandHandler<DeleteProductPlanCommand, Result>
{
    public async Task<Result> Handle(DeleteProductPlanCommand command, CancellationToken cancellationToken)
    {

        var deleteDataStore = new DeleteVoucherStore(command.TableName, "", command.Id.ToString(), "",
            currentUser.CodeUser!, currentUser.CodeUnit);
        var result = await smartDataServices.ExcuteNonQuery(deleteDataStore.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, deleteDataStore.Parameters);

        if (result <= 0)
        {
            return Result.Failure(new Error("400", "Deleted fail"));
        }

        Log.Information("User: {0} Deleted voucher with Id: {1}", currentUser.CodeUser, command.Id);

        return Result.Success(true);
    }
}