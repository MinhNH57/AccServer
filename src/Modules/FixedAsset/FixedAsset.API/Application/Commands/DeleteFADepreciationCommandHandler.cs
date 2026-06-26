namespace FixedAsset.API.Application.Commands;

using MediatR;

public class DeleteFADepreciationCommandHandler(IFADepreciationRepository faDepreciationRepository,
    ILogger<DeleteFADepreciationCommandHandler> logger)
        : IRequestHandler<DeleteFADepreciationCommand, BatchVoucherResponse>
{
    private readonly IFADepreciationRepository _faDepreciationRepository = faDepreciationRepository ?? throw new ArgumentNullException(nameof(faDepreciationRepository));
    private readonly ILogger<DeleteFADepreciationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteFADepreciationCommand message, CancellationToken cancellationToken)
    {
        List<MasterError> masterErrors = [];

        var existedConstrainIds = await _faDepreciationRepository.CheckExistedConstrain([.. message.FADepreciations.Select(x => x.RefId)], message.FADepreciations.FirstOrDefault().RefType);

        foreach (var fADepreciation in message.FADepreciations.Where(x => existedConstrainIds.Contains(x.RefId)))
        {
            var dataError = new DataError()
            {
                RefId = fADepreciation.RefId,
                PostedDate = fADepreciation.PostedDate,
                RefDate = fADepreciation.RefDate,
                RefNo = fADepreciation.RefNo,
                RefType = fADepreciation.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ khấu hao này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
            };

            var masterError = new MasterError()
            {
                RefId = fADepreciation.RefId,
                PostedDate = fADepreciation.PostedDate,
                RefDate = fADepreciation.RefDate,
                RefNo = fADepreciation.RefNo,
                RefType = fADepreciation.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ khấu hao này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
                DataError = dataError
            };

            masterErrors.Add(masterError);
        }

        await _faDepreciationRepository.DeleteAsync([.. message.FADepreciations.Select(x => x.RefId)], message.FADepreciations.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting FADepreciation - FADepreciation: {@FADepreciation}", message.FADepreciations);

        await _faDepreciationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.FADepreciations.Count(),
            MasterErrors = masterErrors
        };

        return response;
    }
}

public class DeleteFADepreciationIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteFADepreciationCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteFADepreciationCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting FADepreciation.
    }
}