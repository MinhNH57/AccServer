using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Response;
using Catalog.Fund.Entities;
using Catalog.Fund.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Features.Queries;

public record GetExcelCatalogObjByIdQuery(Guid Id) : IQuery<Result>;


public class GetExcelCatalogObjByIdQueryHandler(CatalogFundContext dbContext) : IQueryHandler<GetExcelCatalogObjByIdQuery, Result>
{
    public async Task<Result> Handle(GetExcelCatalogObjByIdQuery query, CancellationToken cancellationToken)
    {
        var data = await dbContext.ExcelCatalogObject.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == query.Id,
            cancellationToken: cancellationToken);

        if (data is null) throw new NotFoundException(nameof(ExcelCatalogObject), query.Id);

        return Result.Success(data);
    }
}