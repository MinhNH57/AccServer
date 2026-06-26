using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MassTransit;
using MediatR;
using Voucher.Acc.Features.Commands.GenNoInv;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.SalesTemp;


public record CreateSalesSmartDataTempCommand(
    SalesSmartDataTemp SalesSmartDataTemp,
    List<SalesSmartContentsDataTemp> SalesSmartContentsDataTemps) : ICommand<Result>;


public class CreateSalesSmartDataTempCommandHandler(VoucherDbContext dbContext, ICurrentUser currentUser, IMediator mediator, IPublishEndpoint publishEndpoint) : ICommandHandler<CreateSalesSmartDataTempCommand, Result>
{
    public async Task<Result> Handle(CreateSalesSmartDataTempCommand request, CancellationToken cancellationToken)
    {
        var command = new GenNoInvComnand(new GenNoInvRequest()
        {
            UserCode = currentUser.CodeUser,
            DataType = "Inv"
        });
        var data = await mediator.Send(command, cancellationToken);
        request.SalesSmartDataTemp.Id = Guid.NewGuid();
        request.SalesSmartDataTemp.NumberOfVouchers = data.Data.SmartCode;
        foreach (var item in request.SalesSmartContentsDataTemps)
        {
            item.IdContents = request.SalesSmartDataTemp.Id;
        }

        await dbContext.AddAsync(request.SalesSmartDataTemp, cancellationToken);
        await dbContext.AddRangeAsync(request.SalesSmartContentsDataTemps, cancellationToken);
        if (dbContext.ChangeTracker.HasChanges())
        {
            await dbContext.SaveChangesAsync(cancellationToken);

            var eventQc = new CreateDataQuanlityControlEvent(
                currentUser.CodeUnit,
                currentUser.CodeUser!,
                "", "", "", true, Guid.Empty);

            await publishEndpoint.Publish(eventQc, (PublishContext publishContext) =>
            {
                publishContext.Headers.Set("X-Tenant-Id", currentUser.TenantId);
                publishContext.Headers.Set("Type", "SalesAdmin");
            }, cancellationToken);

            return Result.Success(true);
        }
        return Result.Success(false);
    }
}