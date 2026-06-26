using System.Text.Json;
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.ConfirmVoucher;

public class ConfirmVoucherRequest
{
    public string Parameter { get; set; } = string.Empty;
    public string? TableName { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string? Status { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public int? ConfirmVoucher { get; set; }
}


public record ConfirmVoucherCommand(ConfirmVoucherRequest Request, int SendNoti = 1) : ICommand<Result>;

public class ConfirmVoucherCommandHandler(SmartDataServices dataServices, ICurrentUser currentUser, VoucherDbContext dbContext, IPublishEndpoint publishEndpoint)
    : ICommandHandler<ConfirmVoucherCommand, Result>
{
    public async Task<Result> Handle(ConfirmVoucherCommand command, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(command.Request.Id);
        Log.Information(JsonSerializer.Serialize(command));
        var useName = currentUser.CodeUser;
        var comfirmVoucherStore = new ConfirmVoucherStore(command.Request.Parameter ?? "", command.Request.TableName, command.Request.Id, useName ?? "", currentUser.CodeUnit,
            command.Request.Status, command.Request.Reason ?? "", command.Request.ConfirmVoucher);
        var test = dbContext.Database.GetConnectionString()!;
        var result = await dataServices.ExcuteNonQuery(comfirmVoucherStore.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, comfirmVoucherStore.Parameters, cancellationToken);
        if (command.SendNoti == 1)
        {
            //Send event to RabbitMQ
            var confirmVoucherEvent = new ConfirmVoucherEvent(command.Request.Parameter!, command.Request.Id, currentUser.CodeUnit, currentUser.CodeUser!, command.Request.Reason);
            await publishEndpoint.Publish(confirmVoucherEvent, (PublishContext publishContext) =>
            {
                publishContext.Headers.Set(TenantConstant.TenantIdHeader, currentUser.TenantId);
            }, cancellationToken: cancellationToken);
        }
        return Result.Success(true);
    }
}