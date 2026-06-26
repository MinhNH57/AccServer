using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using Catalog.Fund.Infrastructure;
using Catalog.Fund.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Features.Queries;
public class ObjectViewMobileDetail
{
    public ObjectDtoFund Object { get; set; } = new();
    public SurveyExpertiseDto? SurveyExpertise { get; set; } = new();
    public List<CatalogRelationshipDto>? LstRelationShip { get; set; }
}

public record GetObjectByCccdQuery(string Cccd) : IQuery<Result<ObjectViewMobileDetail>>
{
    public string CacheKey => $"KT:object:{Cccd}";
    public TimeSpan Expiration => TimeSpan.FromMinutes(5);
}

public class GetObjectByCccdQueryHandler(CatalogFundContext dbContext) : IQueryHandler<GetObjectByCccdQuery, Result<ObjectViewMobileDetail>>
{
    public async Task<Result<ObjectViewMobileDetail>> Handle(GetObjectByCccdQuery request, CancellationToken cancellationToken)
    {
        ObjectViewMobileDetail result = new();
        var obj = await dbContext.CatalogObject
            .AsNoTracking()
            .ProjectToType<ObjectDtoFund>()
            .FirstOrDefaultAsync(x => x.CitizenIDNumber == request.Cccd.Trim(), cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (obj is null)
        {
            return Result.Failure<ObjectViewMobileDetail>(new("400", "Chưa có đối tượng có mã căn cước này"));
        }
        result.Object= obj;

        result.LstRelationShip = await dbContext.CatalogRelationship
            .AsNoTracking()
            .ProjectToType<CatalogRelationshipDto>()
            .Where(x => x.IdMember == obj.Id)
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        result.SurveyExpertise = await dbContext.SurveyExpertise
            .ProjectToType<SurveyExpertiseDto>()
            .FirstOrDefaultAsync(x => x.IdSource == obj.Id, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        //var objMap = mapper.Map<ObjectDtoFund>(obj);
        //var surveyMap = mapper.Map<SurveyExpertiseDto>(surveyExpertise ?? new());

        //var lstRelationShipMap = mapper.Map<List<CatalogRelationshipDto>>(lstRelationShip);

        return Result.Success(result);
    }
}