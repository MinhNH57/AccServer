using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version2;
using BuildingBlocks.Response;
using PaginationRequest = BuildingBlocks.Pagination.Version1.PaginationRequest;

namespace Catalog.HRM.Features.Queries.GetCatalogDynamic;

public record GetDynamicCatalogQuery(
    FilteringRequest Filtering,
    SortRequest Sorting,
    PaginationRequest Pagination,
    string Catalog,
    string? TypeView) : ICacheQuery<Result<PagedResult<object>>>
{
    public string CacheKey => $"KT:0109393461:catalog:{Catalog}:{TypeView}";
    public string TypeName => Catalog;
    public TimeSpan Expiration => TimeSpan.FromMinutes(5);
}

public class GetDynamicCatalogQueryHandler(HrmDbContext dbContext, IDistributedCache cache) : IQueryHandler<GetDynamicCatalogQuery, Result<PagedResult<object>>>
{
    public async Task<Result<PagedResult<object>>> Handle(GetDynamicCatalogQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<object> data;

        var result = await cache.GetDataCacheAsync<Result<PagedResult<object>>>(query.CacheKey, token: cancellationToken);
        if (result is not null)
        {
            return (Result<PagedResult<object>>)result;
        }
      
        var page = query.Pagination?.Page > 0 ? (int)query.Pagination.Page : 1;
        //var pageSize = query.Pagination?.PageSize > 1 ? (pagination.PageSize > 250 ? 250 : (int)pagination.PageSize) : 50;  
        var pageSize = query.Pagination?.PageSize ?? 1000;
        var filter = query.Filtering.Filter.Select(x =>
        {
            if (x.Split(":").Length == 3) return new Filter { Field = x.Split(":")[0], Operator = x.Split(":")[1], Value = x.Split(":")[2] };

            return new Filter { Field = x.Split(":")[0], Value = x.Split(":").Length > 1 ? x.Split(":")[1] : "" };
        }).ToList();

        var sort = query.Sorting.Sort
            .Select(x => new Sort() { Name = x.Split(":")[0], Direction = x.Split(":").Length > 1 ? x.Split(":")[1] : "asc" }).ToList();
        
        if (query.Pagination is null)
        {
        }

        var connection = dbContext.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        var storedProcedure = new FindCatalogStoredProcedure(query.Catalog, query.Filtering.Ids, query.Filtering.Search, query.Filtering.Fields, filter, sort, page, pageSize, query.TypeView);

        var multipleResult = await connection.QueryMultipleAsync(storedProcedure.StoredProcedureName, storedProcedure.Parameters);
        var totalItems = await multipleResult.ReadSingleAsync<long>();
        data = await multipleResult.ReadAsync<object>();
        await cache
            .SetStringAsync(query.CacheKey,
                JsonConvert.SerializeObject(new PagedResult<object>()
                    { TotalRecode = (int)totalItems, Items = data.ToList(), PageNumber = page, PageSize = pageSize }),
                token: cancellationToken);
        return Result<PagedResult<object>>.Success(new PagedResult<object>() { TotalRecode = (int)totalItems, Items = data.ToList() , PageNumber = page, PageSize = pageSize});
    }
}