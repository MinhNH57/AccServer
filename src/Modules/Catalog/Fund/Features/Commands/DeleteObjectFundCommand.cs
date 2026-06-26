using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Catalog.Fund.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Features.Commands;

public record DeleteObjectFundCommand(string Cccd) : ICommand<Result<bool>>;

public class DeleteObjectFundCommandHandler(
    CatalogFundContext context,
    ICurrentUser currentUser,
    IPublishEndpoint publishEndpoint) : ICommandHandler<DeleteObjectFundCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteObjectFundCommand command, CancellationToken cancellationToken)
    {
        var obj = await context.CatalogObject
            .FirstOrDefaultAsync(x => x.CitizenIDNumber == command.Cccd.Trim(), cancellationToken);

        if (obj is null)
            return Result.Failure<bool>(new("400", "Chưa có đối tượng có mã căn cước này"));


        context.CatalogObject.Remove(obj);

        var relationShip = await context.CatalogRelationship
            .Where(x => x.IdMember == obj.Id)
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        context.CatalogRelationship.RemoveRange(relationShip);

        // Xử lý xóa file bằng RabbitMq
        var eventMessage = new DeleteFileEvent(obj.Id.ToString(), "CatalogObject");
        await publishEndpoint.Publish(eventMessage, (PublishContext publishContext) =>
        {
            publishContext.Headers.Set("X-Tenant-Id", currentUser.TenantId);
        }, cancellationToken);

        //await fileAttachServices.DeleteAllFileFund(new()
        //{ ColumnTable = "CatalogObject", KeyTable = obj.Id.ToString() });

        var surveyExpertise = await context.SurveyExpertise.FirstOrDefaultAsync(x => x.IdSource == obj.Id, cancellationToken);
        if (surveyExpertise is not null)
        {
            context.SurveyExpertise.Remove(surveyExpertise);
        }

        int count = await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (count <= 0)
            //  logger.LogInformation("User: '{userName}' failed to remove data with type: 'CatalogObject' with cccd: '{cccd}' .", currentUser.CodeUser, cccd);
            return Result.Failure<bool>(new Error("400", "Xoá thất bại"));
        //logger.LogInformation("User: '{userName}' removed data with type: 'CatalogObject' with cccd: '{cccd}' .", currentUser.CodeUser, cccd);

        //TODO: Xóa cache
        //await cache.RemoveByPatternAsync(GetCatalogCacheKey("CatalogObject"));
        return Result.Success(true);
    }
}