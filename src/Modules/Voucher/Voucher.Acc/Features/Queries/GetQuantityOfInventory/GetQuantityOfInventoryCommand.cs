using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Queries.GetQuantityOfInventory;

public class GetQuantityOfInventoryRequest
{
    public string? CommodityCode { get; set; } = "";
    public string Date { get; set; } = DateTime.Now.ToString("MM-dd-yyyy");
    public string? WarehoseCode { get; set; } = "";

}



public record GetQuantityOfInventoryCommand(GetQuantityOfInventoryRequest Request) : ICommand<Result>;

public class GetQuantityOfInventoryCommandHandler(SmartDataServices dataServices,VoucherDbContext dbContext)
    : ICommandHandler<GetQuantityOfInventoryCommand, Result>
{
    public async Task<Result> Handle(GetQuantityOfInventoryCommand command, CancellationToken cancellationToken)
    {

        var result = await dataServices.GetValue($"select [dbo].[GetInventoryByWarehoseDate]('Web','{command.Request.Date}','{command.Request.WarehoseCode}','{command.Request.CommodityCode}')", dbContext.Database.GetConnectionString());

        return Result.Success(result);
    }
}