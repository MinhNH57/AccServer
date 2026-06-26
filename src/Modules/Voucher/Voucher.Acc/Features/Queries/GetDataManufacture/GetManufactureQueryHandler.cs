using System.Linq.Dynamic.Core;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination.Version1;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Queries.GetDataManufacture;

public class GetManufactureQueryHandler(VoucherDbContext dbContext) : IQueryHandler<GetManufactureQuery, ApiResponse<List<SmartDataManufacture>>>
{
    public async Task<ApiResponse<List<SmartDataManufacture>>> Handle(GetManufactureQuery query, CancellationToken cancellationToken)
    {
        var root = (IQueryable<SmartDataManufacture>)dbContext.SmartDataManufactures.AsNoTracking().OrderByDescending(c=>c.NumberOfVouchers);
        //.Include(x => x.SmartContentsDatas.OrderBy(y => y.CreateDate));
        //.Include(x => x.SmartFileAttaches);

        if (!string.IsNullOrEmpty(query.Filtering?.Ids))
        {
            root = root.Where(x => query.Filtering.Ids.Contains(x.Id.ToString()));
        }

        if (!string.IsNullOrEmpty(query.Filtering?.Search))
        {
            root = root.Where(x => x.NumberOfVouchers.Contains(query.Filtering.Search));
        }

        if (query.Filtering is { Filter.Length: > 0 })
        {
            var filterClauses = string.Empty;
            foreach (var filter in query.Filtering.Filter)
            {
                var filterSplit = filter.Split(":");
                if (filterSplit.Length == 3)
                {
                    filterClauses += $"{filterSplit[0]} == \"{filterSplit[2]}\" && ";
                }
                else if (filterSplit.Length == 2)
                {
                    filterClauses += $"{filterSplit[0]} == \"{filterSplit[1]}\" && ";
                }
                else
                {
                    throw new Exception("Điều kiện lọc không hợp lệ.");
                }
            }

            filterClauses = filterClauses[..^4];

            root = root.Where(filterClauses);
        }

        if (query.Sorting is { Sort.Length: > 0 })
        {
            root = root.OrderByDescending(c => c.RecordDate);
        }

        if (!string.IsNullOrEmpty(query.Filtering?.OwnerId))
        {
            root = root.Where(x => x.CreateBy == query.Filtering.OwnerId);
        }

        //if (!string.IsNullOrEmpty(query.Filtering?.Status))
        //{
        //    root = root.Where(x => x.Status == query.Filtering.Status);
        //}

        var page = query.Pagination?.Page > 0 ? (int)query.Pagination.Page : 1;
        var pageSize = query.Pagination?.PageSize > 1 ? query.Pagination.PageSize > 250 ? 250 : (int)query.Pagination.PageSize : 50;
        root = root.Skip((page - 1) * pageSize).Take(pageSize);

        if (!string.IsNullOrEmpty(query.Filtering.Fields))
        {
            var fields = $"new {query.Filtering.Fields}";
            root = (IQueryable<SmartDataManufacture>)root.Select(fields);
        }

        var smartDatas = await root.ToListAsync(cancellationToken: cancellationToken);

        return ApiResponseFactory<List<SmartDataManufacture>>.Ok(smartDatas);

    }
}