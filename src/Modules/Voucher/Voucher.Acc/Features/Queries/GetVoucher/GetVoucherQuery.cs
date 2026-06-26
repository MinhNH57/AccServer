using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.GetVoucher;

public record GetVoucherQuery(
    FilteringRequest? Filtering,
     SortRequest? Sorting,
     PaginationRequest? Pagination) : IQuery<ApiResponse<List<SmartData>>>;