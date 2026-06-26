namespace Supply.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using MassTransit;
using MediatR;
using Supply.Domain.AggregatesModel.SUIncrementAggregate;
using Supply.Infrastructure.Idempotency;

public class CreateSUIncrementCommandHandler(IMapper mapper,
    ISUIncrementRepository suIncrementRepository,
    IIdentityService identityService,
        IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateSUIncrementCommandHandler> logger)
        : IRequestHandler<CreateSUIncrementCommand, SUIncrementCreateResponse>
{
    private readonly ISUIncrementRepository _suIncrementRepository = suIncrementRepository ?? throw new ArgumentNullException(nameof(suIncrementRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateSUIncrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<SUIncrementCreateResponse> Handle(CreateSUIncrementCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var suIncrement = new SUIncrement(
            message.SUIncrement.TenantId,
            message.SUIncrement.BranchId,
            message.SUIncrement.SupplyCategoryId,
            message.SUIncrement.SUAuditRefId,
            message.SUIncrement.SupplyOtherBookId,
            message.SUIncrement.FADecrementRefId,
            message.SUIncrement.RefType,
            message.SUIncrement.AllocationTime,
            message.SUIncrement.RemainingAllocationTime,
            message.SUIncrement.DisplayOnBook,
            message.SUIncrement.RefOrder,
            message.SUIncrement.RefDate,
            DateTime.Now,
            DateTime.Now,
            message.SUIncrement.IsPostedManagement,
            message.SUIncrement.IsPostedFinance,
            message.SUIncrement.SuspendAllocate,
            message.SUIncrement.Quantity,
            message.SUIncrement.UnitPrice,
            message.SUIncrement.Amount,
            message.SUIncrement.AllocatedAmount,
            message.SUIncrement.RemainingAmount,
            message.SUIncrement.TermlyAllocationAmount,
            message.SUIncrement.SupplyCode,
            message.SUIncrement.SupplyName,
            message.SUIncrement.RefNo,
            message.SUIncrement.Unit,
            message.SUIncrement.AllocationAccount,
            userName,
            userName,
            message.SUIncrement.InPuRefDetailId,
            message.SUIncrement.ReasonIncrement,
            message.SUIncrement.SupplyGroup,
            message.SUIncrement.SupplyCategoryCode,
            message.SUIncrement.SupplyCategoryName,
            message.SUIncrement.State,
            editVersion,
            message.SUIncrement.BranchName,
            message.SUIncrement.ReasonInactive);

        foreach (var department in message.SUIncrementDetailDepartments)
        {
            suIncrement.AddSUIncrementDetailDepartment(
                null,
                department.OrganizationUnitId,
                department.SortOrder,
                department.AllocationTime,
                department.RemainingAllocationTime,
                department.Quantity,
                department.UnitPrice,
                department.Amount,
                department.AllocatedAmount,
                department.OrganizationUnitCode,
                department.OrganizationUnitName,
                department.OrganizationUnitType,
                department.State,
                editVersion);
        }

        foreach (var allocation in message.SUIncrementDetailAllocations)
        {
            suIncrement.AddSUIncrementDetailAllocation(
                null,
                allocation.ObjectId,
                allocation.ExpenseItemId,
                allocation.SortOrder,
                allocation.ObjectType,
                allocation.AllocationRate,
                allocation.CostAccount,
                allocation.ObjectCode,
                allocation.ObjectName,
                allocation.State,
                editVersion,
                allocation.ExpenseItemCode,
                allocation.ListItemId,
                allocation.ListItemCode);
        }

        foreach (var detail in message.SUIncrementDetails)
        {
            suIncrement.AddSUIncrementDetail(
                detail.SortOrder,
                detail.Description,
                detail.NumberNo,
                detail.State,
                editVersion);
        }

        foreach (var source in message.SUIncrementDetailSources)
        {
            suIncrement.AddSUIncrementDetailSource(
                null,
                source.RefId,
                source.RefDetailId,
                source.OrganizationUnitId,
                source.FixedAssetId,
                source.RefType,
                source.SortOrder,
                source.JournalMemo,
                source.CreditAccount,
                source.DebitAccount,
                source.RefNo,
                source.Amount,
                source.RefDate,
                source.State,
                editVersion,
                source.DetailPostOrder);
        }

        _logger.LogInformation("Creating SUIncrement - SUIncrement: {@SUIncrement}", suIncrement);

        var entity = _suIncrementRepository.Add(suIncrement);

        await _suIncrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
    new { entity.Id, entity.RefType },
    (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
    cancellationToken);

        return new SUIncrementCreateResponse()
        {
            SUIncrement = [_mapper.Map<SUIncrementSaveFullResponse>(entity)],
            SUIncrementDetailDepartment = [.. entity.SUIncrementDetailDepartments.Select(_mapper.Map<SUIncrementDetailSaveFullResponse>)],
            SUIncrementDetailAllocation = [.. entity.SUIncrementDetailAllocations.Select(_mapper.Map<SUIncrementDetailSaveFullResponse>)],
            SUIncrementDetail = [.. entity.SUIncrementDetails.Select(_mapper.Map<SUIncrementDetailSaveFullResponse>)],
            SUIncrementDetailSource = [.. entity.SUIncrementDetailSources.Select(_mapper.Map<SUIncrementDetailSaveFullResponse>)],
        };
    }
}

public class CreateSUIncrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateSUIncrementCommand, SUIncrementCreateResponse>> logger)
    : IdentifiedCommandHandler<CreateSUIncrementCommand, SUIncrementCreateResponse>(mediator, requestManager, logger)
{
    protected override SUIncrementCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}