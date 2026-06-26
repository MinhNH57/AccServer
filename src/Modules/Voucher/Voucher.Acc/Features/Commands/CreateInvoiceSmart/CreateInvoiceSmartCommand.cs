using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc.Features.Commands.CreateInvoiceSmart;

public record CreateInvoiceSmartCommand(string Parameter, string IdVoucher) : ICommand<Result>;

public class CreateInvoiceSmartCommandHandler(
    VoucherDbContext dbContext,
    SmartDataServices smartDataServices,
    ICurrentUser currentUser,
    IPublishEndpoint publishEndpoint) : ICommandHandler<CreateInvoiceSmartCommand, Result>
{
    public async Task<Result> Handle(CreateInvoiceSmartCommand command, CancellationToken cancellationToken)
    {
        var paramStore = new { Parameter = command.Parameter, Id = command.IdVoucher, UserCode = "SMAPI", CodeUnit = 888 };

        var data = await smartDataServices.GetSingleObjectStore("CreateDataVouchersFromTrading", dbContext.Database.GetConnectionString()!, paramStore);

        if (data is null)
        {
            return Result.Failure(new("400", "Tạo hóa đơn thất bại!"));
        }
        var confirmVoucherEvent = new ConfirmVoucherEvent("CreateInvoice", command.IdVoucher, currentUser.CodeUnit, currentUser.CodeUser!);
        await publishEndpoint.Publish(confirmVoucherEvent, (PublishContext publishContext) =>
        {
            publishContext.Headers.Set(TenantConstant.TenantIdHeader, currentUser.TenantId);
        }, cancellationToken: cancellationToken);
        return Result.Success(data);
    }
}