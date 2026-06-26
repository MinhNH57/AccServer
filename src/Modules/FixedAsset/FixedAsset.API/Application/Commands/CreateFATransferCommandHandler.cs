namespace FixedAsset.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using MassTransit;
using MediatR;
using Spectre.Console;

public class CreateFATransferCommandHandler(IMapper mapper,
    IFATransferRepository fATransferRepository,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateFATransferCommandHandler> logger)
        : IRequestHandler<CreateFATransferCommand, FATransferCreateResponse>
{
    private readonly IFATransferRepository _fATransferRepository = fATransferRepository ?? throw new ArgumentNullException(nameof(fATransferRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateFATransferCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<FATransferCreateResponse> Handle(CreateFATransferCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var fATransfer = new FATransfer(
            null,
            null,
            message.FATransfer.RefType,
            message.FATransfer.DisplayOnBook,
            message.FATransfer.RefOrder,
            message.FATransfer.RefDate,
            message.FATransfer.PostedDate,
            message.FATransfer.RefNo,
            message.FATransfer.HandOverName,
            message.FATransfer.RecipientName,
            message.FATransfer.JournalMemo,
            userName,
            message.FATransfer.State,
            editVersion,
            message.FATransfer.AttachmentIdList,
            null,
            false,
            false);

        foreach (var detail in message.FATransferDetails)
        {
            fATransfer.AddFATransferDetail(
                detail.FixedAssetId,
                detail.FixedAssetName,
                detail.FromOrganizationUnitId,
                detail.ToOrganizationUnitId,
                detail.ListItemId,
                detail.ContractId,
                detail.OrderId,
                detail.ProjectWorkId,
                detail.ExpenseItemId,
                detail.JobId,
                detail.SortOrder,
                detail.CostAccount,
                detail.ContractCode,
                detail.ExpenseItemCode,
                detail.JobCode,
                detail.ListItemCode,
                detail.OrderCode,
                detail.ProjectWorkCode,
                detail.ExpenseItemName,
                detail.JobName,
                detail.ListItemName,
                detail.ProjectWorkName,
                detail.FromOrganizationUnitCode,
                detail.ToOrganizationUnitCode,
                detail.FromOrganizationUnitName,
                detail.ToOrganizationUnitName,
                detail.FixedAssetCode,
                detail.State,
                editVersion);
        }

        _logger.LogInformation("Creating FATransfer - FATransfer: {@FATransfer}", fATransfer);

        var entity = _fATransferRepository.Add(fATransfer);

        await _fATransferRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new FATransferCreateResponse()
        {
            FATransfer = [_mapper.Map<FATransferSaveFullResponse>(entity)],
            FATransferDetail = [.. entity.FATransferDetails.Select(_mapper.Map<FATransferDetailSaveFullResponse>)],
        };
    }
}

public class CreateFATransferIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateFATransferCommand, FATransferCreateResponse>> logger) 
    : IdentifiedCommandHandler<CreateFATransferCommand, FATransferCreateResponse>(mediator, requestManager, logger)
{
    protected override FATransferCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}