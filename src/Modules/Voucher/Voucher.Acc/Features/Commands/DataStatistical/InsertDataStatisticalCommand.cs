using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.DataStatistical;

internal record InsertDataStatisticalCommand(string Parameter, List<DataStatisticalReceive> DataStatisticalReceives) : ICommand<Result>;

internal class InsertDataStatisticalCommandHandler(SmartDataServices smartDataServices, VoucherDbContext dbContext, ICurrentUser currentUser)
    : ICommandHandler<InsertDataStatisticalCommand, Result>
{
    public async Task<Result> Handle(InsertDataStatisticalCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.DataStatisticalReceives)
        {
            item.UserCode = currentUser.CodeUser;
            //item.RecordDate = DateTime.Now;
        }
        var listData = command.DataStatisticalReceives.ToDataTable();
        var paramStore = new
        {
            Parameter = command.Parameter,
            UserCode = currentUser.CodeUser,
            CodeUnit = currentUser.CodeUnit,
            ListStatistical = listData
        };

        var rowCount = await smartDataServices.ExcuteNonQuery("InsertDataStatistics", dbContext.Database.GetConnectionString()!, paramStore, cancellationToken);
        return Result.Success("Create data successfully");
    }
}