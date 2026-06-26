using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.Sales.GetSalesVoucherContent;

public record GetSalesVoucherContentQuery(Guid IdVoucher) : IQuery<Result<List<SalesSmartContentsData>>>
{

}
