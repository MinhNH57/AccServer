using System.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using BuildingBlocks.Pagination.Version2;
using BuildingBlocks.Pagination.Version2.Enums;
using BuildingBlocks.Response;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Voucher.HRM.Infrastructure;
using Voucher.HRM.Infrastructure.StoredProcedures;

namespace Voucher.HRM.Features.Queries.GetDataSynGrid;

internal sealed class GetDataSynGridQueryHandler(VoucherHrmDbContext dbContext) : IQueryHandler<GetDataSynGridQuery, Result>
{
    public async Task<Result> Handle(GetDataSynGridQuery query, CancellationToken cancellationToken)
    {
        List<Filter> filters = new List<Filter>();
        List<Sort> sorts = new();
        int page = 1;
        int pageSize = 0;
        object? typeView = null;
        object? tableName = null;
        long totalItems = 0;
        IDictionary<string, double>? summaryData = null;
        string searchTemp = string.Empty;

        var dataRequest = query.GetDataSynGridRequest;
        if (dataRequest.FilterSyn is { Count: > 0 })
        {
            foreach (var filter in dataRequest.FilterSyn)
            {
                var listFilter = filter.Predicates.Select(c => new Filter()
                {
                    Field = c.Field,
                    Operator = c.Operator,
                    Value = c.Value.ToString() ?? string.Empty
                }).ToList();
                filters.AddRange(listFilter);
            }
        }

        if (dataRequest.SortSyn is { Count: > 0 })
        {
            sorts = dataRequest.SortSyn.Select(c => new Sort()
            {
                Name = c.Name,
                Direction = c.Direction == "Ascending" ? nameof(SortDirection.asc) : nameof(SortDirection.desc),
            }).ToList();
        }
        if (dataRequest.Search is { Count: > 0 })
        {
            searchTemp = dataRequest.Search[0].Key;
        }

        dataRequest.Params?.TryGetValue("TypeView", out typeView);
        dataRequest.Params?.TryGetValue("TableName", out tableName);

        ArgumentNullException.ThrowIfNull(tableName);
        dataRequest.Take = dataRequest.Take == 0 ? 100 : dataRequest.Take;
        page = dataRequest.Skip / dataRequest.Take + 1;
        pageSize = dataRequest.Take;
        var storedProcedure = new FindVoucherStoredProcedure(tableName.ToString()!, null, searchTemp, null, filters, sorts, page, pageSize, typeView?.ToString());

        var connection = dbContext.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        var multipleResult = await connection.QueryMultipleAsync(storedProcedure.StoredProcedureName, storedProcedure.Parameters).ConfigureAwait(false);

        totalItems = await multipleResult.ReadSingleAsync<long>().ConfigureAwait(false);
        var data = await multipleResult.ReadAsync<object>().ConfigureAwait(false);
        if (!multipleResult.IsConsumed)
        {
            var test = await multipleResult.ReadSingleAsync<object>().ConfigureAwait(false);
            summaryData = ((IDictionary<string, object?>)test).ToDictionary(c => c.Key,
            c => c.Value is null ? 0 : Convert.ToDouble(c.Value));
        }
        var result = new PagedResult<object>()
        {
            PageNumber = page,
            PageSize = pageSize,
            Items = data.ToList(),
            SummaryData = summaryData,
            TotalRecode = totalItems
        };

        return Result.Success(result);
    }
}