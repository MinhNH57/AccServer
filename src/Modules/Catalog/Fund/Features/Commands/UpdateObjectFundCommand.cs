using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Catalog.Fund.Entities;
using Catalog.Fund.Enums;
using Catalog.Fund.Infrastructure;
using Catalog.Fund.Models;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Features.Commands;


public record UpdateObjectFundCommand(ObjectDtoFundAction ObjectDto) : ICommand<Result<bool>>;


public class UpdateObjectFundCommandHandler(
    CatalogFundContext context,
    IMapper mapper,
    ICurrentUser currentUser) : ICommandHandler<UpdateObjectFundCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateObjectFundCommand command, CancellationToken cancellationToken)
    {
        var objectEdit = await context.CatalogObject
            .FirstOrDefaultAsync(x => x.CitizenIDNumber == command.ObjectDto.Object.CitizenIDNumber, cancellationToken)
            .ConfigureAwait(false);
        if (objectEdit == null)
        {
            return Result.Failure<bool>(new Error("400", "Sửa thất bại do không có đối tượng chứa căn cước này."));
        }

        try
        {
            objectEdit = mapper.Map(command.ObjectDto.Object, objectEdit);

            //Khảo sát
            if (command.ObjectDto.SurveyExpertise is not null)
            {
                var surveyEdit = await context.SurveyExpertise
                    .FirstOrDefaultAsync(x => x.IdSource == command.ObjectDto.SurveyExpertise.Id, cancellationToken)
                    .ConfigureAwait(false);

                if (surveyEdit is not null)
                {
                    surveyEdit = mapper.Map(command.ObjectDto.SurveyExpertise, surveyEdit);
                    surveyEdit.LastModified = DateTime.Now;
                    surveyEdit.LastModifiedBy = currentUser.CodeUser;
                    context.SurveyExpertise.Update(surveyEdit);
                }
            }

            if (command.ObjectDto.LstRelationShip is null)
            {
                await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return Result.Success(true);
            }

            var relationShipCreate = command.ObjectDto.LstRelationShip.Where(x => x.Type == StatusModel.Add).ToList();
            var relationShipUpdate = command.ObjectDto.LstRelationShip.Where(x => x.Type == StatusModel.Update).ToList();
            var relationShipRemove = command.ObjectDto.LstRelationShip.Where(x => x.Type == StatusModel.Delete).ToList();

            // Xoá danh sách quan hệ
            if (relationShipCreate.Any())
            {
                // Tạo mới danh sách quan hệ
                var lstRelationshipsCreate = mapper.Map<List<CatalogRelationship>>(relationShipCreate)
                    .Select(x =>
                    {
                        x.IdMember = objectEdit.Id;
                        x.MemberCode = objectEdit.Id.ToString();
                        x.CodeUnit = objectEdit.CodeUnit;
                        return x;
                    })
                    .ToList();
                await context.CatalogRelationship.AddRangeAsync(lstRelationshipsCreate, cancellationToken)
                    .ConfigureAwait(false);
            }

            // Cập nhật danh sách quan hệ
            if (relationShipUpdate.Any())
            {
                var relationshipIdsToUpdate = relationShipUpdate.Select(x => x.Id).ToList();
                var existingRelationships = await context.CatalogRelationship
                    .Where(x => relationshipIdsToUpdate.Contains(x.Id))
                    .ToDictionaryAsync(x => x.Id, cancellationToken)
                    .ConfigureAwait(false);

                var lstRelationshipsUpdate = relationShipUpdate
                    .Where(x => existingRelationships.ContainsKey(x.Id))
                    .Select(x => mapper.Map(x, existingRelationships[x.Id]))
                    .ToList();
                context.CatalogRelationship.UpdateRange(lstRelationshipsUpdate);
            }

            // Xóa danh sách quan hệ
            if (relationShipRemove.Any())
            {
                var relationshipIdsToRemove = relationShipRemove
                    .Select(x => x.Id)
                    .ToList();

                var lstRelationshipsRemove = await context.CatalogRelationship
                    .Where(x => relationshipIdsToRemove.Contains(x.Id))
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                context.CatalogRelationship.RemoveRange(lstRelationshipsRemove);
            }

            context.CatalogObject.Update(objectEdit);
            var count = await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (count > 0)
            {
                // logger.LogInformation("User: '{userName}' updated data with type: 'CatalogObject' content: '{json}'.", currentUser.CodeUser, objectJson);

                //await cache.RemoveByPatternAsync(GetCatalogCacheKey("CatalogObject"));
                return Result.Success(true);
            }

            //logger.LogInformation("User: '{userName}'  failed to update data with type: 'CatalogObject' content: '{json}'.", currentUser.CodeUser, objectJson);
            return Result.Failure<bool>(new Error("400", "Cập nhật thất bại"));
        }
        catch (Exception e)
        {
            return Result.Failure<bool>(new Error("500", e.Message));
        }
    }
}