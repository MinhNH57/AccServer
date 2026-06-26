using System.Reflection.Metadata;
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.GetDataInheritVoucherType;

public class GetDataInheritVoucherTypeRequest
{
    public string? Parameter { get; set; }
    public string? ParameterPlus { get; set; } 

}



public record GetDataInheritVoucherTypeCommand(GetDataInheritVoucherTypeRequest Request) : ICommand<Result>;

public class GetDataInheritVoucherTypeCommandHandler(SmartDataServices dataServices,VoucherDbContext dbContext)
    : ICommandHandler<GetDataInheritVoucherTypeCommand, Result>
{
    public async Task<Result> Handle(GetDataInheritVoucherTypeCommand command, CancellationToken cancellationToken)
    {
        var result = await dataServices.GetListObject<object>($"[dbo].[GetDataInheritVoucherType] '{command.Request.Parameter}','{command.Request.ParameterPlus}'", dbContext.Database.GetConnectionString());

        return Result.Success(result.ToList());
    }
}