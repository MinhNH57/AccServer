using System.Linq.Dynamic.Core;
using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Catalog.Base.Infrastructure;
using PaginationRequest = BuildingBlocks.Pagination.Version1.PaginationRequest;

namespace Catalog.Features.Web.Catalog.Queries.GetAccountSymbol;

public class AccountSymbolVm
{
    public string AccountSymbol { get; set; }
    public string AccountName { get; set; }
    public int? AccountLevel { get; set; }
    public string? AccountParent { get; set; }
    public string AccountType { get; set; }
    public bool Obligatory { get; set; }
    public string? Notes { get; set; }
    public bool IsParent { get; set; }
}

public record GetAccountSymbolQuery(FilteringRequest Filtering, PaginationRequest Pagination) : IQuery<Result>;

public class GetAccountSymbolQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetAccountSymbolQuery, Result>
{
    public async Task<Result> Handle(GetAccountSymbolQuery query, CancellationToken cancellationToken)
    {
        var lstData = await dbContext.CatalogAccountSymbol.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        var root = dbContext.CatalogAccountSymbol
            .AsNoTracking();

        if (query.Filtering.Filter is { Length: > 0 })
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

        var total = await root.CountAsync(cancellationToken: cancellationToken);
        var data = root
            .Skip(((query.Pagination.Page ?? 0) - 1) * query.Pagination.PageSize ?? 1000).Take(query.Pagination.PageSize ?? 1000)
            .Select(c => new AccountSymbolVm
            {
                AccountParent = c.AccountParent,
                AccountLevel = c.AccountLevel,
                AccountName = c.AccountName,
                AccountSymbol = c.AccountSymbol,
                AccountType = c.AccountType,
                Notes = c.Notes,
                IsParent = dbContext.CatalogAccountSymbol.Any(x => x.AccountParent == c.AccountSymbol)
            });

        return Result.Success(new BuildingBlocks.Pagination.Version2.PagedResult<AccountSymbolVm>()
        {
            PageNumber = query.Pagination.Page ?? 1,
            PageSize = query.Pagination.PageSize ?? 1000,
            Items = data.ToList(),
            TotalRecode = total
        });
    }
}