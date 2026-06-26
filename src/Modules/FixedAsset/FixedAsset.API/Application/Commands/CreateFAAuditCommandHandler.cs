namespace FixedAsset.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Spectre.Console;
using static MassTransit.Transports.ReceiveEndpoint;

public class CreateFAAuditCommandHandler(IMapper mapper,
    IFAAuditRepository fAAuditRepository,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateFAAuditCommandHandler> logger)
        : IRequestHandler<CreateFAAuditCommand, FAAuditCreateResponse>
{
    private readonly IFAAuditRepository _fAAuditRepository = fAAuditRepository ?? throw new ArgumentNullException(nameof(fAAuditRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateFAAuditCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<FAAuditCreateResponse> Handle(CreateFAAuditCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var fAAudit = new FAAudit(
            null,
            message.FAAudit.BranchId,
            message.FAAudit.RefType,
            message.FAAudit.DisplayOnBook,
            message.FAAudit.RefDate,
            message.FAAudit.RefTime,
            message.FAAudit.InventoryDate,
            DateTime.Now,
            DateTime.Now,
            message.FAAudit.IsExecuted,
            message.FAAudit.RefNo,
            message.FAAudit.JournalMemo,
            message.FAAudit.Summary,
            userName,
            userName,
            message.FAAudit.State,
            editVersion,
            message.FAAudit.AuditMember,
            null,
            null);

        foreach (var detail in message.FAAuditDetails)
        {
            fAAudit.AddFAAuditDetail(
                detail.FixedAssetId,
                detail.OrganizationUnitId,
                detail.ExistInStock,
                detail.Quality,
                detail.Recommendation,
                detail.SortOrder,
                detail.OrgPrice,
                detail.DepreciationAmount,
                detail.AccumDepreciationAmount,
                detail.RemainingAmount,
                detail.Note,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.FixedAssetCode,
                detail.FixedAssetName,
                detail.State,
                editVersion);
        }

        _logger.LogInformation("Creating FAAudit - FAAudit: {@FAAudit}", fAAudit);

        var entity = _fAAuditRepository.Add(fAAudit);

        await _fAAuditRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new FAAuditCreateResponse()
        {
            FAAudit = [_mapper.Map<FAAuditSaveFullResponse>(entity)],
            FAAuditDetail = [.. entity.FAAuditDetails.Select(_mapper.Map<FAAuditDetailSaveFullResponse>)],
        };
    }
}

public class CreateFAAuditIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateFAAuditCommand, FAAuditCreateResponse>> logger) : IdentifiedCommandHandler<CreateFAAuditCommand, FAAuditCreateResponse>(mediator, requestManager, logger)
{
    protected override FAAuditCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}