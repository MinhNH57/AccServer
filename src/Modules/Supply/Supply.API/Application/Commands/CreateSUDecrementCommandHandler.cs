namespace Supply.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using MassTransit;
using MediatR;
using Supply.Domain.AggregatesModel.SUDecrementAggregate;
using Supply.Infrastructure.Idempotency;

public class CreateSUDecrementCommandHandler(IMapper mapper,
    ISUDecrementRepository suDecrementRepository,
    IIdentityService identityService,
        IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateSUDecrementCommandHandler> logger)
        : IRequestHandler<CreateSUDecrementCommand, SUDecrementCreateResponse>
{
    private readonly ISUDecrementRepository _suDecrementRepository = suDecrementRepository ?? throw new ArgumentNullException(nameof(suDecrementRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateSUDecrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<SUDecrementCreateResponse> Handle(CreateSUDecrementCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var suDecrement = new SUDecrement(
            message.SUDecrement.TenantId,
            message.SUDecrement.BranchId,
            message.SUDecrement.RefType,
            message.SUDecrement.DisplayOnBook,
            message.SUDecrement.RefOrder,
            message.SUDecrement.RefDate,
            DateTime.Now,
            DateTime.Now,
            message.SUDecrement.IsPostedManagement,
            message.SUDecrement.IsPostedFinance,
            message.SUDecrement.TotalAmount,
            message.SUDecrement.RefNo,
            message.SUDecrement.JournalMemo,
            userName,
            userName,
            message.SUDecrement.State,
            message.SUDecrement.BranchName,
            editVersion,
            message.SUDecrement.AttachmentIdList);

        foreach (var detail in message.SUDecrementDetails)
        {
            suDecrement.AddSUDecrementDetail(
                detail.SupplyId,
                detail.SupplyCode,
                detail.SupplyName,
                detail.OrganizationUnitId,
                detail.SUAllocationId,
                detail.SUAuditRefId,
                detail.SortOrder,
                detail.UseQuantity,
                detail.DecrementQuantity,
                detail.DecrementAmount,
                detail.RemainingDecrementAmount,
                detail.Reason,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.State,
                editVersion);
        }

        _logger.LogInformation("Creating SUDecrement - SUDecrement: {@SUDecrement}", suDecrement);

        var entity = _suDecrementRepository.Add(suDecrement);

        await _suDecrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new SUDecrementCreateResponse()
        {
            SUDecrement = [_mapper.Map<SUDecrementSaveFullResponse>(entity)],
            SUDecrementDetail = [.. entity.SUDecrementDetails.Select(_mapper.Map<SUDecrementDetailSaveFullResponse>)],
        };
    }
}

public class CreateSUDecrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateSUDecrementCommand, SUDecrementCreateResponse>> logger) : IdentifiedCommandHandler<CreateSUDecrementCommand, SUDecrementCreateResponse>(mediator, requestManager, logger)
{
    protected override SUDecrementCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}