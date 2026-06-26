using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.GetTaxListVoucher;

public record GetTaxListVoucherQuery (Guid IdVoucher) : IQuery<Result<List<SmartVatTaxList>>>
{

}