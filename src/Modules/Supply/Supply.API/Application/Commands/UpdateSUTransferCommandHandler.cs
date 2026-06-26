namespace Supply.API.Application.Commands;

using MediatR;
using Supply.API.Application.Models;
using Supply.Domain.AggregatesModel.SUTransferAggregate;
using Supply.Infrastructure.Idempotency;

public class UpdateSUTransferCommandHandler(IMapper mapper,
    ISUTransferRepository suTransferRepository,
    IIdentityService identityService,
    ILogger<UpdateSUTransferCommandHandler> logger)
        : IRequestHandler<UpdateSUTransferCommand, SUTransferUpdateResponse>
{
    private readonly ISUTransferRepository _suTransferRepository = suTransferRepository ?? throw new ArgumentNullException(nameof(suTransferRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateSUTransferCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<SUTransferUpdateResponse> Handle(UpdateSUTransferCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new SUTransferUpdateResponse();

        var suTransfer = await _suTransferRepository.GetAsync(message.SUTransfer.RefId);

        var _ = suTransfer.Update(
            message.SUTransfer.TenantId,
            message.SUTransfer.BranchId,
            message.SUTransfer.DisplayOnBook,
            message.SUTransfer.RefOrder,
            message.SUTransfer.RefDate,
            DateTime.Now,
            message.SUTransfer.IsPostedManagement,
            message.SUTransfer.IsPostedFinance,
            message.SUTransfer.TotalQuantity,
            message.SUTransfer.RefNo,
            message.SUTransfer.DeliveryName,
            message.SUTransfer.ReceiptName,
            message.SUTransfer.JournalMemo,
            userName,
            message.SUTransfer.State,
            message.SUTransfer.BranchName,
            message.SUTransfer.EditVersion,
            message.SUTransfer.AttachmentIdList);

        suTransfer.ClearDetailData();

        foreach (var detail in message.SUTransferDetails)
        {
            var entity = suTransfer.AddSUTransferDetail(
                detail.SupplyId,
                detail.SupplyCode,
                detail.SupplyName,
                detail.FromOrganizationUnitId,
                detail.FromOrganizationUnitCode,
                detail.FromOrganizationUnitName,
                detail.ToOrganizationUnitId,
                detail.ToOrganizationUnitCode,
                detail.ToOrganizationUnitName,
                detail.ListItemId,
                detail.ListItemCode,
                detail.ListItemName,
                detail.ContractId,
                detail.ContractCode,
                detail.OrderId,
                detail.OrderCode,
                detail.ProjectWorkId,
                detail.ProjectWorkCode,
                detail.ProjectWorkName,
                detail.ExpenseItemId,
                detail.ExpenseItemCode,
                detail.ExpenseItemName,
                detail.JobId,
                detail.JobCode,
                detail.JobName,
                detail.SortOrder,
                detail.UseQuantity,
                detail.TransferQuantity,
                detail.CostAccount);

            response.SUTransferDetail.Add(_mapper.Map<SUTransferDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating SUTransfer - SUTransfer: {@SUTransfer}", suTransfer);

        await _suTransferRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.SUTransfer = [_mapper.Map<SUTransferSaveFullResponse>(suTransfer)];

        return response;
    }
}

public class UpdateSUTransferIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateSUTransferCommand, SUTransferUpdateResponse>> logger) : IdentifiedCommandHandler<UpdateSUTransferCommand, SUTransferUpdateResponse>(mediator, requestManager, logger)
{
    protected override SUTransferUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating SUTransfer.
    }
}