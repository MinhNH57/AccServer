namespace FixedAsset.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using MassTransit;
using MediatR;
using Spectre.Console;

public class CreateFAAdjustmentCommandHandler(IMapper mapper,
    IFAAdjustmentRepository fAAdjustmentRepository,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateFAAdjustmentCommandHandler> logger)
        : IRequestHandler<CreateFAAdjustmentCommand, FAAdjustmentCreateResponse>
{
    private readonly IFAAdjustmentRepository _fAAdjustmentRepository = fAAdjustmentRepository ?? throw new ArgumentNullException(nameof(fAAdjustmentRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateFAAdjustmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<FAAdjustmentCreateResponse> Handle(CreateFAAdjustmentCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var fAAdjustment = new FAAdjustment(
            message.FAAdjustment.TenantId,
            message.FAAdjustment.BranchId,
            message.FAAdjustment.BranchName,
            message.FAAdjustment.RefType,
            message.FAAdjustment.DisplayOnBook,
            message.FAAdjustment.RefOrder,
            message.FAAdjustment.RefDate,
            message.FAAdjustment.PostedDate,
            message.FAAdjustment.DecisionDate,
            DateTime.Now,
            DateTime.Now,
            message.FAAdjustment.RefNo,
            message.FAAdjustment.DecisionNo,
            message.FAAdjustment.Reason,
            message.FAAdjustment.JournalMemo,
            userName,
            userName,
            message.FAAdjustment.State,
            editVersion,
            message.FAAdjustment.Members,
            message.FAAdjustment.TotalAmount,
            message.FAAdjustment.AttachmentIdList);

        foreach (var detail in message.FAAdjustmentDetails)
        {
            fAAdjustment.AddFAAdjustmentDetail(
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
                editVersion);
        }

        foreach (var post in message.FAAdjustmentDetailPosts)
        {
            fAAdjustment.AddFAAdjustmentDetailPost(
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
                editVersion);
        }

        _logger.LogInformation("Creating FAAdjustment - FAAdjustment: {@FAAdjustment}", fAAdjustment);

        var entity = _fAAdjustmentRepository.Add(fAAdjustment);

        await _fAAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new FAAdjustmentCreateResponse()
        {
            FAAdjustment = [_mapper.Map<FAAdjustmentSaveFullResponse>(entity)],
            FAAdjustmentDetail = [.. entity.FAAdjustmentDetails.Select(_mapper.Map<FAAdjustmentDetailSaveFullResponse>)],
            FAAdjustmentDetailPost = [.. entity.FAAdjustmentDetailPosts.Select(_mapper.Map<FAAdjustmentDetailSaveFullResponse>)]
        };
    }
}

public class CreateFAAdjustmentIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateFAAdjustmentCommand, FAAdjustmentCreateResponse>> logger) : IdentifiedCommandHandler<CreateFAAdjustmentCommand, FAAdjustmentCreateResponse>(mediator, requestManager, logger)
{
    protected override FAAdjustmentCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}