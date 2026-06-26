namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;
using MediatR;

public class DeleteFAAdjustmentCommandHandler(IFAAdjustmentRepository faAdjustmentRepository,
    ILogger<DeleteFAAdjustmentCommandHandler> logger)
        : IRequestHandler<DeleteFAAdjustmentCommand, BatchVoucherResponse>
{
    private readonly IFAAdjustmentRepository _faAdjustmentRepository = faAdjustmentRepository ?? throw new ArgumentNullException(nameof(faAdjustmentRepository));
    private readonly ILogger<DeleteFAAdjustmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteFAAdjustmentCommand message, CancellationToken cancellationToken)
    {
        List<MasterError> masterErrors = [];

        var existedConstrainIds = await _faAdjustmentRepository.CheckExistedConstrain([.. message.FAAdjustments.Select(x => x.RefId)], message.FAAdjustments.FirstOrDefault().RefType);

        foreach (var fAAdjustment in message.FAAdjustments.Where(x => existedConstrainIds.Contains(x.RefId)))
        {
            var dataError = new DataError()
            {
                RefId = fAAdjustment.RefId,
                PostedDate = fAAdjustment.PostedDate,
                RefDate = fAAdjustment.RefDate,
                RefNo = fAAdjustment.RefNo,
                RefType = fAAdjustment.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ đánh giá lại này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
            };

            var masterError = new MasterError()
            {
                RefId = fAAdjustment.RefId,
                PostedDate = fAAdjustment.PostedDate,
                RefDate = fAAdjustment.RefDate,
                RefNo = fAAdjustment.RefNo,
                RefType = fAAdjustment.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ đánh giá lại này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
                DataError = dataError
            };

            masterErrors.Add(masterError);
        }

        await _faAdjustmentRepository.DeleteAsync([.. message.FAAdjustments.Select(x => x.RefId)], message.FAAdjustments.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting FAAdjustment - FAAdjustment: {@FAAdjustment}", message.FAAdjustments);

        await _faAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.FAAdjustments.Count(),
            MasterErrors = masterErrors
        };

        return response;
    }
}

public class DeleteFAAdjustmentIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteFAAdjustmentCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteFAAdjustmentCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FAAdjustment.
    }
}