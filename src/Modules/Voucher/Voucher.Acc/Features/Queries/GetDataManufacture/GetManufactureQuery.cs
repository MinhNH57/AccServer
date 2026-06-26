using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.GetDataManufacture;

public record GetManufactureQuery(
    FilteringRequest? Filtering,
     SortRequest? Sorting,
     PaginationRequest? Pagination) : IQuery<ApiResponse<List<SmartDataManufacture>>>;