using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.AdminAccountTransfer;

public class AdminAccountTransferRequest
{
    public string Parameter { get; set; } = string.Empty;
    public string AccountSymbol { get; set; } = string.Empty;
    public string BeginDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string WareHouseCode { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
}

public record AdminAccountTransferCommand(AdminAccountTransferRequest Request) : ICommand<Result>;

public class AdminAccountTransferCommandHandler(SmartDataServices dataServices, ICurrentUser currentUser, VoucherDbContext dbContext, IPublishEndpoint publishEndpoint)
    : ICommandHandler<AdminAccountTransferCommand, Result>
{
    public async Task<Result> Handle(AdminAccountTransferCommand command, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(command.Request.Parameter);
        var comfirmVoucherStore = new AdminAccountTransferStore(command.Request.Parameter ?? "", currentUser.CodeUser,currentUser.CodeUnit, command.Request.AccountSymbol, command.Request.BeginDate.ToString(), command.Request.EndDate, command.Request.WareHouseCode, command.Request.ProductCode, "");

        var result = await dataServices.ExecQueryListAsync<object>(comfirmVoucherStore.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, comfirmVoucherStore.Parameters, cancellationToken);


        return Result.Success(result);
    }
}