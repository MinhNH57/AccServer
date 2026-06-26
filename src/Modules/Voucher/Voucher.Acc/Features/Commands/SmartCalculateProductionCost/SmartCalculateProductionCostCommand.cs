using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.SmartCalculateProductionCost;

public class SmartCalculateProductionCostRequest
{
    public string Parameter { get; set; } = string.Empty;
    public string AccountSymbol { get; set; } = string.Empty;
    public string BeginDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string WareHouseCode { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public bool IsUpdate { get; set; }
}
//@Parameter nvarchar(50),@UserCode nvarchar(20),@CodeUnit int,@AccountSymbol nvarchar(20) ,@BeginDate nvarchar(10),@EndDate nvarchar(10),@WarehoseCode nvarchar(20),@ProductCode nvarchar(70),@SmartTable nvarchar(100)

public record SmartCalculateProductionCostCommand(SmartCalculateProductionCostRequest Request) : ICommand<Result>;

public class SmartCalculateProductionCostCommandHandler(SmartDataServices dataServices, ICurrentUser currentUser, VoucherDbContext dbContext, IPublishEndpoint publishEndpoint)
    : ICommandHandler<SmartCalculateProductionCostCommand, Result>
{
    public async Task<Result> Handle(SmartCalculateProductionCostCommand command, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(command.Request.Parameter);
        var comfirmVoucherStore = new SmartCalculateProductionCostStore(command.Request.Parameter ?? "", currentUser.CodeUser,currentUser.CodeUnit, command.Request.AccountSymbol, command.Request.BeginDate.ToString(), command.Request.EndDate, command.Request.WareHouseCode, command.Request.ProductCode, "");

        var result = await dataServices.ExecQueryListAsync<object>(comfirmVoucherStore.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, comfirmVoucherStore.Parameters, cancellationToken);


        return Result.Success(result);
    }
}