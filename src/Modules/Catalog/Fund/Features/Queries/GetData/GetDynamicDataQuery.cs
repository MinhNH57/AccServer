using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using BuildingBlocks.Pagination.Version2;
using BuildingBlocks.Response;
using PaginationRequest = BuildingBlocks.Pagination.Version1.PaginationRequest;

namespace Catalog.Fund.Features.Queries.GetData;


public record GetDynamicDataQuery(FilteringRequest Filtering,
    SortRequest Sorting,
    PaginationRequest Pagination,
    string TableName) : IQuery<Result<PagedResult<object>>>;