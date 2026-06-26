namespace FixedAsset.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using FixedAsset.Domain.AggregatesModel.FADecrementAggregate;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using Spectre.Console;

public class CreateFADecrementCommandHandler(IMapper mapper,
    IFADecrementRepository fADecrementRepository,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateFADecrementCommandHandler> logger)
        : IRequestHandler<CreateFADecrementCommand, FADecrementCreateResponse>
{
    private readonly IFADecrementRepository _fADecrementRepository = fADecrementRepository ?? throw new ArgumentNullException(nameof(fADecrementRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateFADecrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<FADecrementCreateResponse> Handle(CreateFADecrementCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var fADecrement = new FADecrement(
            null,
            message.FADecrement.RefType,
            message.FADecrement.DisplayOnBook,
            message.FADecrement.RefOrder,
            message.FADecrement.RefDate,
            message.FADecrement.PostedDate,
            message.FADecrement.TotalAmount,
            message.FADecrement.RefNo,
            message.FADecrement.JournalMemo,
            null,
            message.FADecrement.AttachmentIdList,
            userName,
            false,
            false,
            editVersion,
            message.FADecrement.State);

        foreach (var detail in message.FADecrementDetails)
        {
            fADecrement.AddFADecrementDetail(
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
                editVersion,
                detail.State);
        }

        foreach (var post in message.FADecrementDetailPosts)
        {
            fADecrement.AddFADecrementDetailPost(
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
                editVersion,
                post.State);
        }

        _logger.LogInformation("Creating FADecrement - FADecrement: {@FADecrement}", fADecrement);

        var entity = _fADecrementRepository.Add(fADecrement);

        await _fADecrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new FADecrementCreateResponse()
        {
            FADecrement = [_mapper.Map<FADecrementSaveFullResponse>(entity)],
            FADecrementDetail = [.. entity.FADecrementDetails.Select(_mapper.Map<FADecrementDetailSaveFullResponse>)],
            FADecrementDetailPost = [.. entity.FADecrementDetailPosts.Select(_mapper.Map<FADecrementDetailSaveFullResponse>)]
        };
    }
}

public class CreateFADecrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateFADecrementCommand, FADecrementCreateResponse>> logger) : IdentifiedCommandHandler<CreateFADecrementCommand, FADecrementCreateResponse>(mediator, requestManager, logger)
{
    protected override FADecrementCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}