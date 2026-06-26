using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Catalog.Base.Infrastructure;

namespace Catalog.Features.Web.Catalog.Queries.GetCatalogDistinctBy;

public class GetCatalogDistintByQuery()
{
    public string Prop { get; set; }
    public string TableName { get; set; }
    public string? Conditon { get; set; } = "";
}

public record GetCatalogDistintByCommand(GetCatalogDistintByQuery query) : ICommand<Result>;

public class GetDebitBlancCommandHandler(SmartDataServices dataServices, CatalogDbContext dbContext)
    : ICommandHandler<GetCatalogDistintByCommand, Result>
{
    public async Task<Result> Handle(GetCatalogDistintByCommand command, CancellationToken cancellationToken)
    {
        var _condition = string.IsNullOrEmpty(command.query.Conditon) ? "" : $"{command.query.Conditon}".ToString();
        var result = await dataServices.GetListObject<object>($"WITH CTE AS (SELECT *, ROW_NUMBER() OVER (PARTITION BY {command.query.Prop} ORDER BY IdAsc) AS rn FROM  {command.query.TableName} {_condition}) SELECT TOP 12000  * FROM CTE WHERE rn = 1 ORDER BY {command.query.Prop} ASC", dbContext.Database.GetConnectionString());
        return Result.Success(result.ToList());
    }
}
