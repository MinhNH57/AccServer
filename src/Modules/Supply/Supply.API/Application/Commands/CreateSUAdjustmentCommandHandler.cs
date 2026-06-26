namespace Supply.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using MassTransit;
using MediatR;
using Supply.Domain.AggregatesModel.SUAdjustmentAggregate;
using Supply.Infrastructure.Idempotency;

public class CreateSUAdjustmentCommandHandler(IMapper mapper,
    ISUAdjustmentRepository suAdjustmentRepository,
    IIdentityService identityService,
        IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateSUAdjustmentCommandHandler> logger)
        : IRequestHandler<CreateSUAdjustmentCommand, SUAdjustmentCreateResponse>
{
    private readonly ISUAdjustmentRepository _suAdjustmentRepository = suAdjustmentRepository ?? throw new ArgumentNullException(nameof(suAdjustmentRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateSUAdjustmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<SUAdjustmentCreateResponse> Handle(CreateSUAdjustmentCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var suAdjustment = new SUAdjustment(
            message.SUAdjustment.TenantId,
            message.SUAdjustment.BranchId,
            message.SUAdjustment.RefType,
            message.SUAdjustment.DisplayOnBook,
            message.SUAdjustment.RefOrder,
            message.SUAdjustment.JournalMemo,
            message.SUAdjustment.RefDate,
            DateTime.Now,
            DateTime.Now,
            message.SUAdjustment.IsPostedFinance,
            message.SUAdjustment.IsPostedManagement,
            message.SUAdjustment.RefNo,
            userName,
            userName,
            message.SUAdjustment.State,
            editVersion,
            message.SUAdjustment.TotalAmount,
            message.SUAdjustment.BranchName,
            message.SUAdjustment.AttachmentIdList);

        foreach (var detail in message.SUAdjustmentDetails)
        {
            suAdjustment.AddSUAdjustmentDetail(
                detail.SupplyId,
                detail.SupplyCode,
                detail.SupplyName,
                detail.Quantity,
                detail.AllocationAccount,
                detail.CurrentRemainingAmount,
                detail.NewRemainingAmount,
                detail.DiffRemainingAmount,
                detail.CurrentRemainingAllocationTime,
                detail.NewRemainingAllocationTime,
                detail.DiffAllocationTime,
                detail.TermlyAllocationAmount,
                detail.Note,
                detail.SortOrder,
                detail.State,
                editVersion);
        }

        foreach (var voucher in message.SUAdjustmentDetailVouchers)
        {
            suAdjustment.AddSUAdjustmentDetailVoucher(
                voucher.TenantId,
                voucher.VoucherRefId,
                voucher.VoucherRefDetailId,
                voucher.CreditAccount,
                voucher.DebitAccount,
                voucher.VoucherRefType,
                voucher.SortOrder,
                voucher.RefNo,
                voucher.Amount,
                voucher.RefDate,
                voucher.RefTypeName,
                voucher.Description,
                voucher.State,
                editVersion,
                voucher.DetailPostOrder);
        }

        _logger.LogInformation("Creating SUAdjustment - SUAdjustment: {@SUAdjustment}", suAdjustment);

        var entity = _suAdjustmentRepository.Add(suAdjustment);

        await _suAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new SUAdjustmentCreateResponse()
        {
            SUAdjustment = [_mapper.Map<SUAdjustmentSaveFullResponse>(entity)],
            SUAdjustmentDetail = [.. entity.SUAdjustmentDetails.Select(_mapper.Map<SUAdjustmentDetailSaveFullResponse>)],
            SUAdjustmentDetailVoucher = [.. entity.SUAdjustmentDetailVouchers.Select(_mapper.Map<SUAdjustmentDetailSaveFullResponse>)]
        };
    }
}

public class CreateSUAdjustmentIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateSUAdjustmentCommand, SUAdjustmentCreateResponse>> logger) : IdentifiedCommandHandler<CreateSUAdjustmentCommand, SUAdjustmentCreateResponse>(mediator, requestManager, logger)
{
    protected override SUAdjustmentCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}