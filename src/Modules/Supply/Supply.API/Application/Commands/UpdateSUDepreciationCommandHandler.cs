namespace Supply.API.Application.Commands;

using MediatR;
using Supply.API.Application.Models;
using Supply.Domain.AggregatesModel.SUAllocationAggregate;
using Supply.Infrastructure.Idempotency;

public class UpdateSUAllocationCommandHandler(IMapper mapper,
    ISUAllocationRepository suAllocationRepository,
    IIdentityService identityService,
    ILogger<UpdateSUAllocationCommandHandler> logger)
        : IRequestHandler<UpdateSUAllocationCommand, SUAllocationUpdateResponse>
{
    private readonly ISUAllocationRepository _suAllocationRepository = suAllocationRepository ?? throw new ArgumentNullException(nameof(suAllocationRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateSUAllocationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<SUAllocationUpdateResponse> Handle(UpdateSUAllocationCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new SUAllocationUpdateResponse();

        var suAllocation = await _suAllocationRepository.GetAsync(message.SUAllocation.RefId);

        var _ = suAllocation.Update(
            message.SUAllocation.TenantId,
            message.SUAllocation.BranchId,
            message.SUAllocation.Month,
            message.SUAllocation.Year,
            message.SUAllocation.DisplayOnBook,
            message.SUAllocation.RefOrder,
            message.SUAllocation.RefDate,
            message.SUAllocation.PostedDate,
            DateTime.Now,
            message.SUAllocation.IsPostedManagement,
            message.SUAllocation.IsPostedFinance,
            message.SUAllocation.IsGetSupplyAllocated,
            message.SUAllocation.TotalAmount,
            message.SUAllocation.RefNo,
            message.SUAllocation.JournalMemo,
            userName,
            message.SUAllocation.State,
            message.SUAllocation.BranchName,
            message.SUAllocation.EditVersion,
            message.SUAllocation.AttachmentIdList);

        suAllocation.ClearDetailData();  

        foreach (var expense in message.SUAllocationDetailExpenses)
        {
            var entity = suAllocation.AddSUAllocationDetailExpense(
                expense.SupplyCode,
                expense.SupplyName,
                expense.SupplyCategoryCode,
                expense.SupplyCategoryName,
                expense.TenantId,
                expense.SupplyId,
                expense.SupplyCategoryId,
                expense.SortOrder,
                expense.TotalAllocationAmount,
                expense.AllocationAmount,
                expense.RemainingAmount,
                expense.State,
                expense.EditVersion);

            response.SUAllocationDetailExpense.Add(_mapper.Map<SUAllocationDetailSaveFullResponse>(entity));
        }

        foreach (var table in message.SUAllocationDetailTables)
        {
            var entity = suAllocation.AddSUAllocationDetailTable(
                table.TenantId,
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
                table.EditVersion,
                table.ExpenseItemCode,
                table.ExpenseItemName,
                table.AllocationObjectCode,
                table.AllocationObjectName,
                table.AllocationAccount,
                table.AllocationObjectType,
                table.ListItemId,
                table.ListItemCode,
                table.ListItemName);

            response.SUAllocationDetailTable.Add(_mapper.Map<SUAllocationDetailSaveFullResponse>(entity));
        }

        foreach (var post in message.SUAllocationDetailPosts)
        {
            var entity = suAllocation.AddSUAllocationDetailPost(
                post.TenantId,
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
                post.EditVersion,
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

            response.SUAllocationDetailPost.Add(_mapper.Map<SUAllocationDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating SUAllocation - SUAllocation: {@SUAllocation}", suAllocation);

        await _suAllocationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.SUAllocation = [_mapper.Map<SUAllocationSaveFullResponse>(suAllocation)];

        return response;
    }
}

public class UpdateSUAllocationIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateSUAllocationCommand, SUAllocationUpdateResponse>> logger) : IdentifiedCommandHandler<UpdateSUAllocationCommand, SUAllocationUpdateResponse>(mediator, requestManager, logger)
{
    protected override SUAllocationUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating SUAllocation.
    }
}