namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;
using MediatR;

public class UpdateFADepreciationCommandHandler(IMapper mapper,
    IFADepreciationRepository fADepreciationRepository,
    IIdentityService identityService,
    ILogger<UpdateFADepreciationCommandHandler> logger)
        : IRequestHandler<UpdateFADepreciationCommand, FADepreciationUpdateResponse>
{
    private readonly IFADepreciationRepository _fADepreciationRepository = fADepreciationRepository ?? throw new ArgumentNullException(nameof(fADepreciationRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateFADepreciationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<FADepreciationUpdateResponse> Handle(UpdateFADepreciationCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new FADepreciationUpdateResponse();

        var fADepreciation = await _fADepreciationRepository.GetAsync(message.FADepreciation.RefId);

        var _ = fADepreciation.Update(
            message.FADepreciation.TenantId,
            message.FADepreciation.BranchId,
            message.FADepreciation.Month,
            message.FADepreciation.Year,
            message.FADepreciation.DisplayOnBook,
            message.FADepreciation.RefOrder,
            message.FADepreciation.RefDate,
            message.FADepreciation.PostedDate,
            DateTime.Now,
            message.FADepreciation.TotalAmount,
            message.FADepreciation.RefNo,
            message.FADepreciation.JournalMemo,
            userName,
            message.FADepreciation.State,
            message.FADepreciation.EditVersion,
            message.FADepreciation.AttachmentIdList,
            message.FADepreciation.BranchName);

        fADepreciation.ClearDetailData();

        foreach (var detail in message.FADepreciationDetails)
        {
            var entity = fADepreciation.AddFADepreciationDetail(
                detail.FixedAssetId,
                detail.OrganizationUnitId,
                detail.SortOrder,
                detail.MonthlyDepreciationAmount,
                detail.AmountReasonableCost,
                detail.AmountUnReasonableCost,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.FixedAssetCode,
                detail.FixedAssetName,
                detail.FixedAssetCategoryName,
                detail.FixedAssetCategoryId,
                detail.FixedAssetCategoryCode,
                detail.State,
                detail.EditVersion);

            response.FADepreciationDetail.Add(_mapper.Map<FADepreciationDetailSaveFullResponse>(entity));
        }

        foreach (var allocation in message.FADepreciationDetailAllocations)
        {
            var entity = fADepreciation.AddFADepreciationDetailAllocation(
                allocation.TenantId,
                allocation.FixedAssetId,
                allocation.OrganizationUnitId,
                allocation.AllocationObjectId,
                allocation.ExpenseItemId,
                allocation.ListItemId,
                allocation.SortOrder,
                allocation.MonthlyDepreciationAmount,
                allocation.AllocationRate,
                allocation.AllocationAmount,
                allocation.CostAccount,
                allocation.OrganizationUnitName,
                allocation.AllocationObjectCode,
                allocation.AllocationObjectName,
                allocation.AllocationObjectType,
                allocation.State,
                allocation.EditVersion,
                allocation.FixedAssetCode,
                allocation.FixedAssetName,
                allocation.ListItemCode,
                allocation.ListItemName,
                allocation.ExpenseItemCode,
                allocation.ExpenseItemName,
                allocation.DepreciationAccount,
                allocation.OrganizationUnitCode);

            response.FADepreciationDetailAllocation.Add(_mapper.Map<FADepreciationDetailSaveFullResponse>(entity));
        }

        foreach (var post in message.FADepreciationDetailPosts)
        {
            var entity = fADepreciation.AddFADepreciationDetailPost(
                post.TenantId,
                null,
                post.DebitAccountObjectId,
                post.CreditAccountObjectId,
                post.ExpenseItemId,
                post.OrganizationUnitId,
                post.JobId,
                post.ProjectWorkId,
                post.OrderId,
                post.ContractId,
                post.ListItemId,
                post.SortOrder,
                post.UnReasonableCost,
                post.Amount,
                post.Description,
                post.DebitAccount,
                post.CreditAccount,
                post.OrganizationUnitName,
                post.ListItemCode,
                post.ExpenseItemCode,
                post.OrganizationUnitCode,
                post.JobCode,
                post.JobName,
                post.ExpenseItemName,
                post.ListItemName,
                post.ProjectWorkCode,
                post.CreditAccountObjectCode,
                post.DebitAccountObjectCode,
                post.CreditAccountObjectName,
                post.DebitAccountObjectName,
                post.OrderCode,
                post.ContractCode,
                post.ContractSubject,
                post.ProjectWorkName,
                post.AccountName,
                post.ContractSignDate,
                post.State,
                post.EditVersion,
                post.AllocationObjectType);

            response.FADepreciationDetailPost.Add(_mapper.Map<FADepreciationDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating FADepreciation - FADepreciation: {@FADepreciation}", fADepreciation);

        await _fADepreciationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.FADepreciation = [_mapper.Map<FADepreciationSaveFullResponse>(fADepreciation)];

        return response;
    }
}

public class UpdateFADepreciationIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateFADepreciationCommand, FADepreciationUpdateResponse>> logger)
    : IdentifiedCommandHandler<UpdateFADepreciationCommand, FADepreciationUpdateResponse>(mediator, requestManager, logger)
{
    protected override FADepreciationUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FADepreciation.
    }
}