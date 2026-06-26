namespace Supply.API.Application.Commands;

using MediatR;
using Supply.API.Application.Models;
using Supply.Domain.AggregatesModel.SUAdjustmentAggregate;
using Supply.Infrastructure.Idempotency;

public class UpdateSUAdjustmentCommandHandler(IMapper mapper,
    ISUAdjustmentRepository suAdjustmentRepository,
    IIdentityService identityService,
    ILogger<UpdateSUAdjustmentCommandHandler> logger)
        : IRequestHandler<UpdateSUAdjustmentCommand, SUAdjustmentUpdateResponse>
{
    private readonly ISUAdjustmentRepository _suAdjustmentRepository = suAdjustmentRepository ?? throw new ArgumentNullException(nameof(suAdjustmentRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateSUAdjustmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<SUAdjustmentUpdateResponse> Handle(UpdateSUAdjustmentCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new SUAdjustmentUpdateResponse();

        var suAdjustment = await _suAdjustmentRepository.GetAsync(message.SUAdjustment.RefId);

        var _ = suAdjustment.Update(
            message.SUAdjustment.TenantId,
            message.SUAdjustment.BranchId,
            message.SUAdjustment.DisplayOnBook,
            message.SUAdjustment.RefOrder,
            message.SUAdjustment.JournalMemo,
            message.SUAdjustment.RefDate,
            DateTime.Now,
            message.SUAdjustment.IsPostedFinance,
            message.SUAdjustment.IsPostedManagement,
            message.SUAdjustment.RefNo,
            userName,
            message.SUAdjustment.State,
            message.SUAdjustment.EditVersion,
            message.SUAdjustment.TotalAmount,
            message.SUAdjustment.BranchName,
            message.SUAdjustment.AttachmentIdList);

        suAdjustment.ClearDetailData();

        foreach (var detail in message.SUAdjustmentDetails)
        {
            var entity = suAdjustment.AddSUAdjustmentDetail(
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
                detail.EditVersion);

            response.SUAdjustmentDetail.Add(_mapper.Map<SUAdjustmentDetailSaveFullResponse>(entity));
        }

        foreach (var voucher in message.SUAdjustmentDetailVouchers)
        {
            var entity = suAdjustment.AddSUAdjustmentDetailVoucher(
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
                voucher.EditVersion,
                voucher.DetailPostOrder);

            response.SUAdjustmentDetailVoucher.Add(_mapper.Map<SUAdjustmentDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating SUAdjustment - SUAdjustment: {@SUAdjustment}", suAdjustment);

        await _suAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.SUAdjustment = [_mapper.Map<SUAdjustmentSaveFullResponse>(suAdjustment)];

        return response;
    }
}

public class UpdateSUAdjustmentIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateSUAdjustmentCommand, SUAdjustmentUpdateResponse>> logger) : IdentifiedCommandHandler<UpdateSUAdjustmentCommand, SUAdjustmentUpdateResponse>(mediator, requestManager, logger)
{
    protected override SUAdjustmentUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating SUAdjustment.
    }
}