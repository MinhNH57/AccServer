namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;
using MediatR;

public class UpdateFAAdjustmentCommandHandler(IMapper mapper,
    IFAAdjustmentRepository fAAdjustmentRepository,
    IIdentityService identityService,
    ILogger<UpdateFAAdjustmentCommandHandler> logger)
        : IRequestHandler<UpdateFAAdjustmentCommand, FAAdjustmentUpdateResponse>
{
    private readonly IFAAdjustmentRepository _fAAdjustmentRepository = fAAdjustmentRepository ?? throw new ArgumentNullException(nameof(fAAdjustmentRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateFAAdjustmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<FAAdjustmentUpdateResponse> Handle(UpdateFAAdjustmentCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new FAAdjustmentUpdateResponse();

        var fAAdjustment = await _fAAdjustmentRepository.GetAsync(message.FAAdjustment.RefId);

        var _ = fAAdjustment.Update(
            message.FAAdjustment.TenantId,
            message.FAAdjustment.BranchId,
            message.FAAdjustment.BranchName,
            message.FAAdjustment.DisplayOnBook,
            message.FAAdjustment.RefOrder,
            message.FAAdjustment.RefDate,
            message.FAAdjustment.PostedDate,
            message.FAAdjustment.DecisionDate,
            DateTime.Now,
            message.FAAdjustment.RefNo,
            message.FAAdjustment.DecisionNo,
            message.FAAdjustment.Reason,
            message.FAAdjustment.JournalMemo,
            userName,
            message.FAAdjustment.State,
            message.FAAdjustment.EditVersion,
            message.FAAdjustment.Members,
            message.FAAdjustment.TotalAmount,
            message.FAAdjustment.AttachmentIdList);

        fAAdjustment.ClearDetailData();

        foreach (var detail in message.FAAdjustmentDetails)
        {
            var entity = fAAdjustment.AddFAAdjustmentDetail(
                detail.FixedAssetId,
                detail.FixedAssetCode,
                detail.FixedAssetName,
                detail.OrganizationUnitId,
                detail.SortOrder,
                detail.CurrentRemainingAmount,
                detail.NewRemainingAmount,
                detail.DiffRemainingAmount,
                detail.CurrentLifeTime,
                detail.NewLifeTime,
                detail.CurrentAccumDepreciationAmount,
                detail.NewMonthlyDepreciationAmountByIncomeTax,
                detail.DiffLifeTime,
                detail.DiffMonthlyDepreciationAmount,
                detail.NewAccumDepreciationAmount,
                detail.DiffAccumDepreciationAmount,
                detail.CurrentDepreciationAmount,
                detail.NewDepreciationAmount,
                detail.DiffDepreciationAmount,
                detail.NewMonthlyDepreciationAmount,
                detail.CostAccount,
                detail.AdjustmentAccount,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.State,
                detail.EditVersion);

            response.FAAdjustmentDetail.Add(_mapper.Map<FAAdjustmentDetailSaveFullResponse>(entity));
        }

        foreach (var post in message.FAAdjustmentDetailPosts)
        {
            var entity = fAAdjustment.AddFAAdjustmentDetailPost(
                post.TenantId,
                post.AccountObjectId,
                post.AccountObjectCode,
                post.AccountObjectName,
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
                post.SortOrder,
                post.UnReasonableCost,
                post.Amount,
                post.Description,
                post.DebitAccount,
                post.CreditAccount,
                post.State,
                post.EditVersion);

            response.FAAdjustmentDetailPost.Add(_mapper.Map<FAAdjustmentDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating FAAdjustment - FAAdjustment: {@FAAdjustment}", fAAdjustment);

        await _fAAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.FAAdjustment = [_mapper.Map<FAAdjustmentSaveFullResponse>(fAAdjustment)];

        return response;
    }
}

public class UpdateFAAdjustmentIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateFAAdjustmentCommand, FAAdjustmentUpdateResponse>> logger) : IdentifiedCommandHandler<UpdateFAAdjustmentCommand, FAAdjustmentUpdateResponse>(mediator, requestManager, logger)
{
    protected override FAAdjustmentUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FAAdjustment.
    }
}