using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Catalog.Base.Infrastructure;

namespace Catalog.Features.Web.Catalog.Queries.GetCodeVillage;

public class GetCodeVillageRequest
{
    public string? CodeVillage { get; set; }
}


public record GetCodeVillagCommand(GetCodeVillageRequest GetCodeVillageRequest) : ICommand<Result>;

public class GetDebitBlancCommandHandler(SmartDataServices dataServices, CatalogDbContext dbContext)
    : ICommandHandler<GetCodeVillagCommand, Result>
{
    public async Task<Result> Handle(GetCodeVillagCommand command, CancellationToken cancellationToken)
    {

        var result = await dataServices.GetSingleObject<string>($"select ISNULL(MAX(RIGHT(CodeVillage,LEN(CodeVillage)-LEN(CodeWards))),0)+1 from CatalogVillage where CodeWards=N'{command.GetCodeVillageRequest.CodeVillage}'", dbContext.Database.GetConnectionString());
        return Result.Success(result.ToString());
    }
}
