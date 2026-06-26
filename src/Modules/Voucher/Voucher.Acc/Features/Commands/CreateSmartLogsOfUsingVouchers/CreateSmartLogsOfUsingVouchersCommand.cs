using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Features.Commands.ConfirmVoucher;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.CreateSmartLogsOfUsingVouchers;

public record CreateSmartLogsOfUsingVouchersCommand (SmartLogsOfUsingVouchers Data): ICommand<Result>;


public class CreateSmartLogsOfUsingVouchersCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser, IPublishEndpoint publishEndpoint, IMediator mediator) : ICommandHandler<CreateSmartLogsOfUsingVouchersCommand, Result>
{
    public async Task<Result> Handle(CreateSmartLogsOfUsingVouchersCommand command, CancellationToken cancellationToken)
    {
        await dbContext.SmartLogsOfUsingVouchers.AddAsync(command.Data, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var data = await (from s in dbContext.SmartConclusionQualityControl
            join e in dbContext.SalesSmartData on s.IdContents equals e.Id where e.Id == command.Data.IdContents
            select new
            {
                s.DataType,
                e.NumberOfVouchers,
                s.Notes,
                s.FailedRejectReturnPending,
                e.Id
            }).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        //Duyệt phiếu
        //var comfirmVoucherStore = new ConfirmVoucherStore("", command.Request.TableName, command.Request.Id, useName?? "", 100,
        //    command.Request.Status, command.Request.Reason ?? "",command.Request.ConfirmVoucher);

        var commands = new ConfirmVoucherCommand(new ConfirmVoucherRequest(){Id = data.Id.ToString(),TableName ="SalesSmartData", Reason = data.DataType});
        await mediator.Send(command, cancellationToken);
        // Gửi thông báo 

        var eventQc = new CreateDataQuanlityControlEvent(
            currentUser.CodeUnit,
            currentUser.CodeUser!,
            data.DataType,
            data.NumberOfVouchers ?? "",
            data.Notes,
            data.FailedRejectReturnPending,
            data.Id);

        await publishEndpoint.Publish(eventQc, (PublishContext publishContext) =>
        {
            publishContext.Headers.Set("X-Tenant-Id", currentUser.TenantId);
        }, cancellationToken);

        return Result.Success("Create data successfully");
    }
}