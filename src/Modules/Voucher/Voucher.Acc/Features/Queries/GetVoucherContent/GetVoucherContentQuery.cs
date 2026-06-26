using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.GetVoucherContent;

public record GetVoucherContentQuery(Guid IdVoucher) : IQuery<Result<List<SmartContentsData>>>
{

}