namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;
using FixedAsset.Domain.AggregatesModel.FADecrementAggregate;
using MediatR;

public class UpdateFADecrementCommandHandler(IMapper mapper,
    IFADecrementRepository fADecrementRepository,
    IIdentityService identityService,
    ILogger<UpdateFADecrementCommandHandler> logger)
        : IRequestHandler<UpdateFADecrementCommand, FADecrementUpdateResponse>
{
    private readonly IFADecrementRepository _fADecrementRepository = fADecrementRepository ?? throw new ArgumentNullException(nameof(fADecrementRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateFADecrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<FADecrementUpdateResponse> Handle(UpdateFADecrementCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new FADecrementUpdateResponse();

        var fADecrement = await _fADecrementRepository.GetAsync(message.FADecrement.RefId);

        var _ = fADecrement.Update(
            message.FADecrement.BranchId,
            message.FADecrement.DisplayOnBook,
            message.FADecrement.RefOrder,
            message.FADecrement.RefDate,
            message.FADecrement.PostedDate,
            message.FADecrement.TotalAmount,
            message.FADecrement.RefNo,
            message.FADecrement.JournalMemo,
            message.FADecrement.BranchName,
            message.FADecrement.AttachmentIdList,
            userName,
            message.FADecrement.AutoRefNo,
            message.FADecrement.ForceUpdate,
            message.FADecrement.EditVersion,
            message.FADecrement.State);

        fADecrement.ClearDetailData();

        foreach (var detail in message.FADecrementDetails)
        {
            var entity = fADecrement.AddFADecrementDetail(
                detail.FixedAssetId,
                detail.FixedAssetCode,
                detail.FixedAssetName,
                detail.OrganizationUnitId,
                detail.SortOrder,
                detail.OrgPrice,
                detail.DepreciationAmount,
                detail.AccumDepreciationAmount,
                detail.RemainingAmount,
                detail.DepreciationAmountInMonth,
                detail.OrgPriceAccount,
                detail.DepreciationAccount,
                detail.RemainingAccount,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.EditVersion,
                detail.State);

            response.FADecrementDetail.Add(_mapper.Map<FADecrementDetailSaveFullResponse>(entity));
        }

        foreach (var post in message.FADecrementDetailPosts)
        {
            var entity = fADecrement.AddFADecrementDetailPost(
                post.ExpenseItemId,
                post.ExpenseItemCode,
                post.OrganizationUnitId,
                post.OrganizationUnitCode,
                post.JobId,
                post.JobCode,
                post.ProjectWorkId,
                post.ProjectWorkCode,
                post.OrderId,
                post.OrderCode,
                post.ContractId,
                post.ContractCode,
                post.ListItemId,
                post.ListItemCode,
                post.OrganizationUnitName,
                post.JobName,
                post.ProjectWorkName,
                post.ExpenseItemName,
                post.ListItemName,
                post.AccountObjectId,
                post.AccountObjectCode,
                post.AccountObjectName,
                post.SortOrder,
                post.UnReasonableCost,
                post.Amount,
                post.Description,
                post.DebitAccount,
                post.CreditAccount,
                post.EditVersion,
                post.State);

            response.FADecrementDetailPost.Add(_mapper.Map<FADecrementDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating FADecrement - FADecrement: {@FADecrement}", fADecrement);

        await _fADecrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.FADecrement = [_mapper.Map<FADecrementSaveFullResponse>(fADecrement)];

        return response;
    }
}

public class UpdateFADecrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateFADecrementCommand, FADecrementUpdateResponse>> logger) 
    : IdentifiedCommandHandler<UpdateFADecrementCommand, FADecrementUpdateResponse>(mediator, requestManager, logger)
{
    protected override FADecrementUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FADecrement.
    }
}