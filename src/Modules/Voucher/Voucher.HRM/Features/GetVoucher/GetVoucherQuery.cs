using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Voucher.HRM.Entities;

namespace Voucher.HRM.Features.Queries.GetVoucher;

public record GetVoucherQuery(
    FilteringRequest? Filtering,
     SortRequest? Sorting,
     PaginationRequest? Pagination) : IQuery<ApiResponse<List<SmartDataApplication>>>;