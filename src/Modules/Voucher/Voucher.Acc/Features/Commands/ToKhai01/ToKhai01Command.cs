using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.ToKhai01;

public class ToKhai01Request
{
    public string Parameter { get; set; } = string.Empty;
    public string AccountSymbol { get; set; } = string.Empty;
    public string BeginDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
}


public record ToKhai01Command(ToKhai01Request Request) : ICommand<Result>;

public class ToKhai01CommandHandler(SmartDataServices dataServices, ICurrentUser currentUser, VoucherDbContext dbContext, IPublishEndpoint publishEndpoint)
    : ICommandHandler<ToKhai01Command, Result>
{
    public async Task<Result> Handle(ToKhai01Command command, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(command.Request.Parameter);
        var comfirmVoucherStore = new ToKhai01Store(command.Request.Parameter ?? "","", currentUser.CodeUser,currentUser.CodeUnit,"1111", command.Request.BeginDate.ToString(), command.Request.EndDate, command.Request.Date, "PathImages", "PathLogo", "", "", "SmartTableData");

        var result = await dataServices.ExecQueryListAsync<object>(comfirmVoucherStore.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, comfirmVoucherStore.Parameters, cancellationToken);


        return Result.Success(result);
    }
}