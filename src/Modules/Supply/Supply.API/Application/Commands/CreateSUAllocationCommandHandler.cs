using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using MassTransit;

namespace Supply.API.Application.Commands;

public class CreateSUAllocationCommandHandler(IMapper mapper,
    ISUAllocationRepository suAllocationRepository,
    IIdentityService identityService,
        IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateSUAllocationCommandHandler> logger)
        : IRequestHandler<CreateSUAllocationCommand, SUAllocationCreateResponse>
{
    private readonly ISUAllocationRepository _suAllocationRepository = suAllocationRepository ?? throw new ArgumentNullException(nameof(suAllocationRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateSUAllocationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<SUAllocationCreateResponse> Handle(CreateSUAllocationCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var suAllocation = new SUAllocation(
            null,
            null,
            message.SUAllocation.RefType,
            message.SUAllocation.Month,
            message.SUAllocation.Year,
            message.SUAllocation.DisplayOnBook,
            null,
            message.SUAllocation.RefDate,
            message.SUAllocation.PostedDate,
            DateTime.Now,
            DateTime.Now,
            message.SUAllocation.IsPostedManagement,
            message.SUAllocation.IsPostedFinance,
            message.SUAllocation.IsGetSupplyAllocated,
            message.SUAllocation.TotalAmount,
            message.SUAllocation.RefNo,
            message.SUAllocation.JournalMemo,
            userName,
            userName,
            message.SUAllocation.State,
            null,
            editVersion,
            null);

        foreach (var expense in message.SUAllocationDetailExpenses)
        {
            suAllocation.AddSUAllocationDetailExpense(
                expense.SupplyCode,
                expense.SupplyName,
                expense.SupplyCategoryCode,
                expense.SupplyCategoryName,
                null,
                expense.SupplyId,
                expense.SupplyCategoryId,
                expense.SortOrder,
                expense.TotalAllocationAmount,
                expense.AllocationAmount,
                expense.RemainingAmount,
                expense.State,
                editVersion);
        }

        foreach (var table in message.SUAllocationDetailTables)
        {
            suAllocation.AddSUAllocationDetailTable(
                null,
                table.SupplyId,
                table.SupplyCode,
                table.SupplyName,
                table.AllocationObjectId,
                table.AllocationRate,
                table.AllocationAmount,
                table.SortOrder,
                table.CostAccount,
                table.ExpenseItemId,
                table.TotalAllocationAmount,
                table.State,
                editVersion,
                table.ExpenseItemCode,
                table.ExpenseItemName,
                table.AllocationObjectCode,
                table.AllocationObjectName,
                table.AllocationAccount,
                table.AllocationObjectType,
                table.ListItemId,
                table.ListItemCode,
                table.ListItemName);
        }

        foreach (var post in message.SUAllocationDetailPosts)
        {
            suAllocation.AddSUAllocationDetailPost(
                null,
                post.Description,
                post.DebitAccount,
                post.CreditAccount,
                post.Amount,
                post.ListItemCode,
                post.DebitAccountObjectId,
                post.DebitAccountObjectName,
                post.CreditAccountObjectId,
                post.CreditAccountObjectName,
                post.DebitAccountObjectCode,
                post.CreditAccountObjectCode,
                post.OrganizationUnitId,
                post.JobId,
                post.ProjectWorkId,
                post.ProjectWorkCode,
                post.ProjectWorkName,
                post.OrderId,
                post.OrderCode,
                post.ContractId,
                post.ContractCode,
                post.ListItemId,
                post.ExpenseItemId,
                post.SortOrder,
                post.UnReasonableCost,
                post.State,
                editVersion,
                post.ExpenseItemCode,
                post.OrganizationUnitName,
                post.JobName,
                post.ExpenseItemName,
                post.ListItemName,
                post.OrganizationUnitCode,
                post.JobCode,
                post.ContractSubject,
                post.AllocationObjectId,
                post.AllocationObjectCode,
                post.AllocationObjectName,
                post.AllocationObjectType);
        }

        _logger.LogInformation("Creating SUAllocation - SUAllocation: {@SUAllocation}", suAllocation);

        var entity = _suAllocationRepository.Add(suAllocation);

        await _suAllocationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new SUAllocationCreateResponse()
        {
            SUAllocation = [_mapper.Map<SUAllocationSaveFullResponse>(entity)],
            SUAllocationDetailExpense = [.. entity.SUAllocationDetailExpenses.Select(_mapper.Map<SUAllocationDetailSaveFullResponse>)],
            SUAllocationDetailTable = [.. entity.SUAllocationDetailTables.Select(_mapper.Map<SUAllocationDetailSaveFullResponse>)],
            SUAllocationDetailPost = [.. entity.SUAllocationDetailPosts.Select(_mapper.Map<SUAllocationDetailSaveFullResponse>)]
        };
    }
}

public class CreateSUAllocationIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateSUAllocationCommand, SUAllocationCreateResponse>> logger) : IdentifiedCommandHandler<CreateSUAllocationCommand, SUAllocationCreateResponse>(mediator, requestManager, logger)
{
    protected override SUAllocationCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}