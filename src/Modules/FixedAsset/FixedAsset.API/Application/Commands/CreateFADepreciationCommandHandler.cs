namespace FixedAsset.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using FixedAsset.Domain.AggregatesModel.FADepreciationAggregate;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using Spectre.Console;

public class CreateFADepreciationCommandHandler(IMapper mapper,
    IFADepreciationRepository fADepreciationRepository,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateFADepreciationCommandHandler> logger)
        : IRequestHandler<CreateFADepreciationCommand, FADepreciationCreateResponse>
{
    private readonly IFADepreciationRepository _fADepreciationRepository = fADepreciationRepository ?? throw new ArgumentNullException(nameof(fADepreciationRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateFADepreciationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<FADepreciationCreateResponse> Handle(CreateFADepreciationCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var fADepreciation = new FADepreciation(
            null,
            null,
            message.FADepreciation.RefType,
            message.FADepreciation.Month,
            message.FADepreciation.Year,
            message.FADepreciation.DisplayOnBook,
            0,
            message.FADepreciation.RefDate,
            message.FADepreciation.PostedDate,
            DateTime.Now,
            DateTime.Now,
            message.FADepreciation.TotalAmount,
            message.FADepreciation.RefNo,
            message.FADepreciation.JournalMemo,
            userName,
            userName,
            message.FADepreciation.State,
            editVersion,
            null,
            null);

        foreach (var detail in message.FADepreciationDetails)
        {
            fADepreciation.AddFADepreciationDetail(
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
                editVersion);
        }

        foreach (var allocation in message.FADepreciationDetailAllocations)
        {
            fADepreciation.AddFADepreciationDetailAllocation(
                null,
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
                editVersion,
                allocation.FixedAssetCode,
                allocation.FixedAssetName,
                allocation.ListItemCode,
                allocation.ListItemName,
                allocation.ExpenseItemCode,
                allocation.ExpenseItemName,
                allocation.DepreciationAccount,
                allocation.OrganizationUnitCode);
        }

        foreach (var post in message.FADepreciationDetailPosts)
        {
            fADepreciation.AddFADepreciationDetailPost(
                null,
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
                editVersion,
                post.AllocationObjectType);
        }

        _logger.LogInformation("Creating FADepreciation - FADepreciation: {@FADepreciation}", fADepreciation);

        var entity = _fADepreciationRepository.Add(fADepreciation);

        await _fADepreciationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new FADepreciationCreateResponse()
        {
            FADepreciation = [_mapper.Map<FADepreciationSaveFullResponse>(entity)],
            FADepreciationDetail = [.. entity.FADepreciationDetails.Select(_mapper.Map<FADepreciationDetailSaveFullResponse>)],
            FADepreciationDetailAllocation = [.. entity.FADepreciationDetails.Select(_mapper.Map<FADepreciationDetailSaveFullResponse>)],
            FADepreciationDetailPost = [.. entity.FADepreciationDetailPosts.Select(_mapper.Map<FADepreciationDetailSaveFullResponse>)]
        };
    }
}

public class CreateFADepreciationIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateFADepreciationCommand, FADepreciationCreateResponse>> logger) : IdentifiedCommandHandler<CreateFADepreciationCommand, FADepreciationCreateResponse>(mediator, requestManager, logger)
{
    protected override FADepreciationCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}