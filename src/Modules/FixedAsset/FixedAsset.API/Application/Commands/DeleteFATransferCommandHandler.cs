namespace FixedAsset.API.Application.Commands;

using MediatR;

public class DeleteFATransferCommandHandler(
    IFATransferRepository faTransferRepository,
    ILogger<DeleteFATransferCommandHandler> logger)
        : IRequestHandler<DeleteFATransferCommand, BatchVoucherResponse>
{
    private readonly IFATransferRepository _faTransferRepository = faTransferRepository ?? throw new ArgumentNullException(nameof(faTransferRepository));
    private readonly ILogger<DeleteFATransferCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteFATransferCommand message, CancellationToken cancellationToken)
    {
        List<MasterError> masterErrors = [];

        var existedConstrainIds = await _faTransferRepository.CheckExistedConstrain([.. message.FATransfers.Select(x => x.RefId)], message.FATransfers.FirstOrDefault().RefType);

        foreach (var fATransfer in message.FATransfers.Where(x => existedConstrainIds.Contains(x.RefId)))
        {
            var dataError = new DataError()
            {
                RefId = fATransfer.RefId,
                PostedDate = fATransfer.PostedDate,
                RefDate = fATransfer.RefDate,
                RefNo = fATransfer.RefNo,
                RefType = fATransfer.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ điều chuyển này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
            };

            var masterError = new MasterError()
            {
                RefId = fATransfer.RefId,
                PostedDate = fATransfer.PostedDate,
                RefDate = fATransfer.RefDate,
                RefNo = fATransfer.RefNo,
                RefType = fATransfer.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ điều chuyển này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
                DataError = dataError
            };

            masterErrors.Add(masterError);
        }

        await _faTransferRepository.DeleteAsync([.. message.FATransfers.Select(x => x.RefId)], message.FATransfers.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting FATransfer - FATransfer: {@FATransfer}", message.FATransfers);

        await _faTransferRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.FATransfers.Count(),
            MasterErrors = masterErrors
        };

        return response;
    }
}

public class DeleteFATransferIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteFATransferCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteFATransferCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting FATransfer.
    }
}