
using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.Sales.UpdateSalesVoucher;

public class UpdateSalesVoucherRequest
{
    public Guid Id { get; set; }
    public SalesSmartData? SalesSmartData { get; set; }
    public List<SalesSmartContentsData>? SalesSmartContentsDatas { get; set; }
    public List<SmartProductAttributeByOrder>? SmartProductAttributeByOrders { get; set; }
}

public record UpdateSalesVoucherCommand(UpdateSalesVoucherRequest Request) : ICommand<Result<bool>>;