using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Acc.Features.Queries.CreateOtherVoucher;

internal record CreateOtherVoucherQuery(string DataType, string Id,string NumberOfVouchers) : IQuery<Result>;
