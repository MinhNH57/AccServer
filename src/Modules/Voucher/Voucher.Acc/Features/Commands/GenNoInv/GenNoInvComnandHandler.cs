using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.GenNoInv;

public class GenNoInvComnandHandler (SmartDataServices dastaServices, VoucherDbContext dbContext): ICommandHandler<GenNoInvComnand, Result<GenNoInvReponse>>
{
    public async Task<Result<GenNoInvReponse>> Handle(GenNoInvComnand command, CancellationToken cancellationToken)
    {
        string queryStr =
            $"Select dbo.GenNoInv('{command.Request.UserCode ?? ""}', '{command.Request.CodeUnit}', '{command.Request.IsDate}', '{command.Request.Date}', '{command.Request.TableName}', '{command.Request.DataType ?? ""}') as SmartCode";

        var result = await dastaServices
            .GetSingleObject<GenNoInvReponse>(queryStr, dbContext.Database.GetConnectionString())
            .ConfigureAwait(false);

        return Result.Success(result);
    }
}