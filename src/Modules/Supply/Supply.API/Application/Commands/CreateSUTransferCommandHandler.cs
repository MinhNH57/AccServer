namespace Supply.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using MassTransit;
using MediatR;
using Supply.Domain.AggregatesModel.SUTransferAggregate;
using Supply.Infrastructure.Idempotency;

public class CreateSUTransferCommandHandler(IMapper mapper,
    ISUTransferRepository suTransferRepository,
    IIdentityService identityService,
        IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateSUTransferCommandHandler> logger)
        : IRequestHandler<CreateSUTransferCommand, SUTransferCreateResponse>
{
    private readonly ISUTransferRepository _suTransferRepository = suTransferRepository ?? throw new ArgumentNullException(nameof(suTransferRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateSUTransferCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<SUTransferCreateResponse> Handle(CreateSUTransferCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var suTransfer = new SUTransfer(
            null,
            message.SUTransfer.BranchId,
            message.SUTransfer.RefType,
            message.SUTransfer.DisplayOnBook,
            null,
            message.SUTransfer.RefDate,
            DateTime.Now,
            DateTime.Now,
            message.SUTransfer.IsPostedManagement,
            message.SUTransfer.IsPostedFinance,
            null,
            message.SUTransfer.RefNo,
            message.SUTransfer.DeliveryName,
            message.SUTransfer.ReceiptName,
            message.SUTransfer.JournalMemo,
            userName,
            userName,
            message.SUTransfer.State,
            null,
            editVersion,
            null);

        foreach (var detail in message.SUTransferDetails)
        {
            suTransfer.AddSUTransferDetail(
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
        }

        _logger.LogInformation("Creating SUTransfer - SUTransfer: {@SUTransfer}", suTransfer);

        var entity = _suTransferRepository.Add(suTransfer);

        await _suTransferRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
    new { entity.Id, entity.RefType },
    (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
    cancellationToken);

        return new SUTransferCreateResponse()
        {
            SUTransfer = [_mapper.Map<SUTransferSaveFullResponse>(entity)],
            SUTransferDetail = [.. entity.SUTransferDetails.Select(_mapper.Map<SUTransferDetailSaveFullResponse>)],
        };
    }
}

public class CreateSUTransferIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateSUTransferCommand, SUTransferCreateResponse>> logger) : IdentifiedCommandHandler<CreateSUTransferCommand, SUTransferCreateResponse>(mediator, requestManager, logger)
{
    protected override SUTransferCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}