using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Commands.SmartData.UpdateVoucher;

public class UpdateVoucherRequest
{
    public Guid Id { get; set; }
    public Model.SmartData SmartData { get; set; }
}

public record UpdateSalesVoucherCommand(UpdateVoucherRequest Request) : ICommand<Result<bool>>;