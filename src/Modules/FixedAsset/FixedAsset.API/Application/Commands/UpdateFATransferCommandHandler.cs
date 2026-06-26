namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;
using MediatR;

public class UpdateFATransferCommandHandler(IMapper mapper,
    IFATransferRepository fATransferRepository,
    IIdentityService identityService,
    ILogger<UpdateFATransferCommandHandler> logger)
        : IRequestHandler<UpdateFATransferCommand, FATransferUpdateResponse>
{
    private readonly IFATransferRepository _fATransferRepository = fATransferRepository ?? throw new ArgumentNullException(nameof(fATransferRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateFATransferCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<FATransferUpdateResponse> Handle(UpdateFATransferCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new FATransferUpdateResponse();

        var fATransfer = await _fATransferRepository.GetAsync(message.FATransfer.RefId);

        var _ = fATransfer.Update(
            null,
            message.FATransfer.BranchId,
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
            message.FATransfer.EditVersion,
            message.FATransfer.AttachmentIdList,
            message.FATransfer.BranchName,
            message.FATransfer.AutoRefNo,
            message.FATransfer.ForceUpdate);

        fATransfer.ClearDetailData();

        foreach (var detail in message.FATransferDetails)
        {
            var entity = fATransfer.AddFATransferDetail(
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
                detail.EditVersion);

            response.FATransferDetail.Add(_mapper.Map<FATransferDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating FATransfer - FATransfer: {@FATransfer}", fATransfer);

        await _fATransferRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.FATransfer = [_mapper.Map<FATransferSaveFullResponse>(fATransfer)];

        return response;
    }
}

public class UpdateFATransferIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateFATransferCommand, FATransferUpdateResponse>> logger) 
    : IdentifiedCommandHandler<UpdateFATransferCommand, FATransferUpdateResponse>(mediator, requestManager, logger)
{
    protected override FATransferUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FATransfer.
    }
}