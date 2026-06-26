using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MassTransit;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.CreateSmartContentsQuanlityControl;

public class QuanlityControlRequest
{
    public Guid IdVoucher { get; set; }
    public string? NumberOfVoucher { get; set; }
    public List<SmartContentsQuanlityControl> SmartContentsQuanlityControl { get; set; }
    public SmartConclusionQualityControl SmartConclusionQualityControl { get; set; }
}

public record CreateSmartContentsQuanlityControlCommand(QuanlityControlRequest Request) : ICommand<Result>;

public class CreateSmartContentsQuanlityControlCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser, IPublishEndpoint publishEndpoint) : ICommandHandler<CreateSmartContentsQuanlityControlCommand, Result>
{
    public async Task<Result> Handle(CreateSmartContentsQuanlityControlCommand command, CancellationToken cancellationToken)
    {
        command.Request.SmartContentsQuanlityControl.ForEach(c =>
        {
            c.IdContents = command.Request.IdVoucher;
            c.CodeUnit = currentUser.CodeUnit;
        });
        command.Request.SmartConclusionQualityControl.IdContents = command.Request.IdVoucher;
        command.Request.SmartConclusionQualityControl.CodeUnit = currentUser.CodeUnit;

        await dbContext.SmartConclusionQualityControl.AddAsync(command.Request.SmartConclusionQualityControl, cancellationToken);
        await dbContext.SmartContentsQuanlityControl.AddRangeAsync(command.Request.SmartContentsQuanlityControl, cancellationToken);

        var count = await dbContext.SaveChangesAsync(cancellationToken);

        if (count > 0)
        {
            // Gửi thông báo 

            var eventQc = new CreateDataQuanlityControlEvent(
                currentUser.CodeUnit,
                currentUser.CodeUser!,
                command.Request.SmartConclusionQualityControl.DataType,
                command.Request.NumberOfVoucher?? "",
                command.Request.SmartConclusionQualityControl.Notes,
                command.Request.SmartConclusionQualityControl.FailedRejectReturnPending,
                command.Request.IdVoucher);
            try
            {
                await publishEndpoint.Publish(eventQc, (PublishContext publishContext) =>
                {
                    publishContext.Headers.Set("X-Tenant-Id", currentUser.TenantId);
                }, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
         
        }

        return Result.Success("Create data successfully");
    }
}