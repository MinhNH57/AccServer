
using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Catalog.Fund.Entities;
using Catalog.Fund.Infrastructure;
using Catalog.Fund.Models;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using CatalogObject = Catalog.Fund.Entities.CatalogObject;
using CatalogRelationship = Catalog.Fund.Entities.CatalogRelationship;

namespace Catalog.Fund.Features.Commands;

public record AddObjectFundCommand(ObjectDtoFundAction ObjectFund) : ICommand<Result<string>>;

public class AddObjectFundCommandHandler(
    CatalogFundContext context,
    IMapper mapper,
    ICurrentUser currentUser,
    ILogger<AddObjectFundCommandHandler> logger)
    : ICommandHandler<AddObjectFundCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddObjectFundCommand command, CancellationToken cancellationToken)
    {
        //var existsByCode = await context.CatalogObject
        //    .AsNoTracking()
        //    .ProjectToType<ObjectDtoFund>()
        //    .FirstOrDefaultAsync(x => x.CitizenIDNumber == command.ObjectFund.Object.CitizenIDNumber.Trim(), cancellationToken)
        //    .ConfigureAwait(false);

        //if (existsByCode is not null)
        //{
        //    return Result<string>.Failure(new Error("400", "Căn cước đã tồn tại trong hệ thống"), existsByCode.CitizenIDNumber);
        //}

        CatalogObject objCreate = new();
        try
        {
            command.ObjectFund.Object.ObjCode = command.ObjectFund.Object.CitizenIDNumber;
            objCreate = mapper.Map(command.ObjectFund.Object, objCreate);
            objCreate.CodeUnit = currentUser.CodeUnit;
            objCreate.DataType ??= "Temp";

            if (command.ObjectFund.SurveyExpertise is not null)
            {
                var surveyCreate = new SurveyExpertise();
                surveyCreate = mapper.Map(command.ObjectFund.SurveyExpertise, surveyCreate);
                surveyCreate.IdSource = command.ObjectFund.Object.Id;
                //surveyCreate.CreatedBy = currentUser.CodeUser;
                //surveyCreate.LastModifiedBy = currentUser.CodeUser;

                await context.SurveyExpertise.AddAsync(surveyCreate, cancellationToken).ConfigureAwait(false);
            }

            List<CatalogRelationship> lstRelationshipsCreate = new();

            lstRelationshipsCreate = mapper.Map(command.ObjectFund.LstRelationShip, lstRelationshipsCreate);
            foreach (var item in lstRelationshipsCreate)
            {
                item.IdMember = objCreate.Id;
                item.MemberCode = objCreate.Id.ToString();
                item.CodeUnit = objCreate.CodeUnit;
            }

            await context.CatalogObject.AddAsync(objCreate, cancellationToken).ConfigureAwait(false);
            await context.CatalogRelationship.AddRangeAsync(lstRelationshipsCreate, cancellationToken)
                .ConfigureAwait(false);


            var count = await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            //string objectJson = System.Text.Json.JsonSerializer.Serialize(objCreate);
            //string relationshipJson = System.Text.Json.JsonSerializer.Serialize(lstRelationshipsCreate);
            if (count > 0)
            {
                //logger.LogInformation("User: '{userName}' created data with type: 'CatalogObject' content: '{json}'.", currentUser.CodeUser, objectJson);
                //logger.LogInformation("User: '{userName}' created data with type: 'CatalogRelationship' content: '{json}'.", currentUser.CodeUser, relationshipJson);
                //await cache.RemoveByPatternAsync(GetCatalogCacheKey("CatalogObject"));
                return Result.Success(command.ObjectFund.Object.Id.ToString());
            }
            //logger.LogInformation("User: '{userName}'  failed to create data with type: 'CatalogObject' content: '{json}'.", currentUser.CodeUser, objectJson);
            //logger.LogInformation("User: '{userName}' failed to create data with type: 'CatalogRelationship' content: '{json}'.", currentUser.CodeUser, relationshipJson);


            return Result.Failure<string>(new Error("400", "Thêm thất bại"));
        }
        catch (Exception e)
        {
            return Result.Failure<string>(new Error("500", e.Message));
        }
    }
}