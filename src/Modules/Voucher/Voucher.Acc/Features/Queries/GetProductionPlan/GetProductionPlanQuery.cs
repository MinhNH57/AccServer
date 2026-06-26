using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Voucher.Acc.Model.ProductPlant;

namespace Voucher.Acc.Features.Queries.GetProductionPlan;

public record GetProductionPlanQuery(
    FilteringRequest? Filtering,
     SortRequest? Sorting,
     PaginationRequest? Pagination) : IQuery<ApiResponse<List<SmartProductionPlan>>>;