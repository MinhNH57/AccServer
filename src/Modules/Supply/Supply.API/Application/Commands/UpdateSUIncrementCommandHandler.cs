namespace Supply.API.Application.Commands;

using MediatR;
using Supply.API.Application.Models;
using Supply.Domain.AggregatesModel.SUIncrementAggregate;
using Supply.Infrastructure.Idempotency;

public class UpdateSUIncrementCommandHandler(IMapper mapper,
    ISUIncrementRepository suIncrementRepository,
    IIdentityService identityService,
    ILogger<UpdateSUIncrementCommandHandler> logger)
        : IRequestHandler<UpdateSUIncrementCommand, SUIncrementUpdateResponse>
{
    private readonly ISUIncrementRepository _suIncrementRepository = suIncrementRepository ?? throw new ArgumentNullException(nameof(suIncrementRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateSUIncrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<SUIncrementUpdateResponse> Handle(UpdateSUIncrementCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new SUIncrementUpdateResponse();

        var suIncrement = await _suIncrementRepository.GetAsync(message.SUIncrement.SupplyId);

        var _ = suIncrement.Update(
            null,
            message.SUIncrement.BranchId,
            message.SUIncrement.SupplyCategoryId,
            message.SUIncrement.SUAuditRefId,
            message.SUIncrement.SupplyOtherBookId,
            message.SUIncrement.FADecrementRefId,
            message.SUIncrement.AllocationTime,
            message.SUIncrement.RemainingAllocationTime,
            message.SUIncrement.DisplayOnBook,
            message.SUIncrement.RefOrder,
            message.SUIncrement.RefDate,
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
            message.SUIncrement.InPuRefDetailId,
            message.SUIncrement.ReasonIncrement,
            message.SUIncrement.SupplyGroup,
            message.SUIncrement.SupplyCategoryCode,
            message.SUIncrement.SupplyCategoryName,
            message.SUIncrement.State,
            message.SUIncrement.EditVersion,
            message.SUIncrement.BranchName,
            message.SUIncrement.ReasonInactive);

        suIncrement.ClearDetailData();

        foreach (var department in message.SUIncrementDetailDepartments)
        {
            var entity = suIncrement.AddSUIncrementDetailDepartment(
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
                department.EditVersion);

            response.SUIncrementDetailDepartment.Add(_mapper.Map<SUIncrementDetailSaveFullResponse>(entity));
        }

        foreach (var allocation in message.SUIncrementDetailAllocations)
        {
            var entity = suIncrement.AddSUIncrementDetailAllocation(
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
                allocation.EditVersion,
                allocation.ExpenseItemCode,
                allocation.ListItemId,
                allocation.ListItemCode);

            response.SUIncrementDetailAllocation.Add(_mapper.Map<SUIncrementDetailSaveFullResponse>(entity));
        }

        foreach (var detail in message.SUIncrementDetails)
        {
            var entity = suIncrement.AddSUIncrementDetail(
                detail.SortOrder,
                detail.Description,
                detail.NumberNo,
                detail.State,
                detail.EditVersion);

            response.SUIncrementDetail.Add(_mapper.Map<SUIncrementDetailSaveFullResponse>(entity));
        }

        foreach (var source in message.SUIncrementDetailSources)
        {
            var entity = suIncrement.AddSUIncrementDetailSource(
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
                source.EditVersion,
                source.DetailPostOrder);

            response.SUIncrementDetailSource.Add(_mapper.Map<SUIncrementDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating SUIncrement - SUIncrement: {@SUIncrement}", suIncrement);

        await _suIncrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.SUIncrement = [_mapper.Map<SUIncrementSaveFullResponse>(suIncrement)];

        return response;
    }
}

public class UpdateSUIncrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateSUIncrementCommand, SUIncrementUpdateResponse>> logger)
    : IdentifiedCommandHandler<UpdateSUIncrementCommand, SUIncrementUpdateResponse>(mediator, requestManager, logger)
{
    protected override SUIncrementUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating SUIncrement.
    }
}