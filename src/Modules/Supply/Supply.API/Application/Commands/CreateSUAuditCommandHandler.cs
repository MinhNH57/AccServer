namespace Supply.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using MassTransit;
using MediatR;

public class CreateSUAuditCommandHandler(IMapper mapper,
    ISUAuditRepository suAuditRepository,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateSUAuditCommandHandler> logger)
        : IRequestHandler<CreateSUAuditCommand, SUAuditCreateResponse>
{
    private readonly ISUAuditRepository _suAuditRepository = suAuditRepository ?? throw new ArgumentNullException(nameof(suAuditRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateSUAuditCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<SUAuditCreateResponse> Handle(CreateSUAuditCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var suAudit = new SUAudit(
            null,
            message.SUAudit.BranchId,
            message.SUAudit.RefType,
            null,
            message.SUAudit.RefDate,
            message.SUAudit.RefTime,
            message.SUAudit.BalanceDate,
            DateTime.Now,
            DateTime.Now,
            message.SUAudit.IsExecuted,
            message.SUAudit.RefNo,
            message.SUAudit.JournalMemo,
            message.SUAudit.Summary,
            userName,
            userName,
            message.SUAudit.State,
            editVersion,
            message.SUAudit.AuditMember,
            null,
            null);

        foreach (var detail in message.SUAuditDetails)
        {
            suAudit.AddSUAuditDetail(
                detail.SupplyId,
                detail.SupplyCode,
                detail.SupplyName,
                detail.OrganizationUnitId,
                detail.SortOrder,
                detail.Action,
                detail.QuantityOnBook,
                detail.QuantityInventory,
                detail.DiffQuantity,
                detail.GoodQuantity,
                detail.DamageQuantity,
                detail.ExecuteQuantity,
                detail.Note,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.State,
                editVersion,
                detail.Unit);
        }

        _logger.LogInformation("Creating SUAudit - SUAudit: {@SUAudit}", suAudit);

        var entity = _suAuditRepository.Add(suAudit);

        await _suAuditRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new SUAuditCreateResponse()
        {
            SUAudit = [_mapper.Map<SUAuditSaveFullResponse>(entity)],
            SUAuditDetail = [.. entity.SUAuditDetails.Select(_mapper.Map<SUAuditDetailSaveFullResponse>)],
        };
    }
}

public class CreateSUAuditIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateSUAuditCommand, SUAuditCreateResponse>> logger) : IdentifiedCommandHandler<CreateSUAuditCommand, SUAuditCreateResponse>(mediator, requestManager, logger)
{
    protected override SUAuditCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}