using System.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Pagination.Version1;
using BuildingBlocks.Pagination.Version2;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Catalog.Fund.Infrastructure;
using Catalog.Fund.Infrastructure.StoredProcedures;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Features.Queries.GetData;

public class GetDynamicDataQueryHandler(CatalogFundContext dbContext, ICurrentUser currentUser) : IQueryHandler<GetDynamicDataQuery, Result<PagedResult<object>>>
{
    public async Task<Result<PagedResult<object>>> Handle(GetDynamicDataQuery query, CancellationToken cancellationToken)
    {
        long totalItems;
 
        IEnumerable<object> data;
        var page = query.Pagination?.Page > 0 ? (int)query.Pagination.Page : 1;
        //var pageSize = pagination?.PageSize > 1 ? (pagination.PageSize > 250 ? 250 : (int)pagination.PageSize) : 50;  
        var pageSize = query.Pagination?.PageSize ?? 1000;

        var filter = query.Filtering.Filter.Select(x =>
        {
            if (x.Split(":").Length == 3)
                return new Filter { Field = x.Split(":")[0], Operator = x.Split(":")[1], Value = x.Split(":")[2] };

            return new Filter { Field = x.Split(":")[0], Value = x.Split(":").Length > 1 ? x.Split(":")[1] : "" };
        }).ToList();
        var sort = query.Sorting.Sort.Select(x => new Sort()
        { Name = x.Split(":")[0], Direction = x.Split(":").Length > 1 ? x.Split(":")[1] : "asc" }).ToList();

        if (query.Pagination is null)
        {

        }

        try
        {
            var connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }

            FindCatalogStoredProcedure  storedProcedure;
            if (currentUser.Role != "Member")
            {
                 storedProcedure = new FindCatalogStoredProcedure(query.TableName, query.Filtering.Ids,
                    query.Filtering.Search, query.Filtering.Fields, filter, sort, page, pageSize, codeUser: currentUser.CodeUser);
            }
            else
            {
                storedProcedure = new FindCatalogStoredProcedure(query.TableName, query.Filtering.Ids,
                    query.Filtering.Search, query.Filtering.Fields, filter, sort, page, pageSize);
            }

            var test = currentUser.TenantId;
            var multipleResult =
                await connection.QueryMultipleAsync(storedProcedure.StoredProcedureName, storedProcedure.Parameters);
            totalItems = await multipleResult.ReadSingleAsync<long>();
            data = await multipleResult.ReadAsync<object>();
        }
        catch (Exception ex)
        {
            throw new BadRequestException(ex.Message);
        }

        return Result.Success(new PagedResult<object>() { TotalRecode = (int)totalItems, Items = data.ToList(), PageNumber = page, PageSize = pageSize });
    }
}