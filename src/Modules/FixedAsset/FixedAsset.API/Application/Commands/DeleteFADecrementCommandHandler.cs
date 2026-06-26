namespace FixedAsset.API.Application.Commands;

using MediatR;

public class DeleteFADecrementCommandHandler(
    IFADecrementRepository faDecrementRepository,
    ILogger<DeleteFADecrementCommandHandler> logger)
        : IRequestHandler<DeleteFADecrementCommand, BatchVoucherResponse>
{
    private readonly IFADecrementRepository _faDecrementRepository = faDecrementRepository ?? throw new ArgumentNullException(nameof(faDecrementRepository));
    private readonly ILogger<DeleteFADecrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteFADecrementCommand message, CancellationToken cancellationToken)
    {
        List<MasterError> masterErrors = [];

        var existedConstrainIds = await _faDecrementRepository.CheckExistedConstrain([.. message.FADecrements.Select(x => x.RefId)], message.FADecrements.FirstOrDefault().RefType);

        foreach (var fADecrement in message.FADecrements.Where(x => existedConstrainIds.Contains(x.RefId)))
        {
            var dataError = new DataError()
            {
                RefId = fADecrement.RefId,
                PostedDate = fADecrement.PostedDate,
                RefDate = fADecrement.RefDate,
                RefNo = fADecrement.RefNo,
                RefType = fADecrement.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ ghi giảm này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
            };

            var masterError = new MasterError()
            {
                RefId = fADecrement.RefId,
                PostedDate = fADecrement.PostedDate,
                RefDate = fADecrement.RefDate,
                RefNo = fADecrement.RefNo,
                RefType = fADecrement.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ ghi giảm này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
                DataError = dataError
            };

            masterErrors.Add(masterError);
        }

        await _faDecrementRepository.DeleteAsync([.. message.FADecrements.Select(x => x.RefId)], message.FADecrements.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting FADecrement - FADecrement: {@FADecrement}", message.FADecrements);

        await _faDecrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.FADecrements.Count(),
            MasterErrors = masterErrors
        };

        return response;
    }
}

public class DeleteFADecrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteFADecrementCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteFADecrementCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting FADecrement.
    }
}