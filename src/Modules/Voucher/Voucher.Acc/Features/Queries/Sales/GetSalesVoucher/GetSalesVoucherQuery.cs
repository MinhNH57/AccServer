using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.Sales.GetSalesVoucher;

public record GetSalesVoucherQuery(FilteringRequest? Filtering,
    SortRequest? Sorting,
PaginationRequest? Pagination) : IQuery<ApiResponse<List<SalesSmartData>>>;

