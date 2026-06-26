namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;
using MediatR;

public class DeleteFAAuditCommandHandler(IFAAuditRepository faAuditRepository,
    ILogger<DeleteFAAuditCommandHandler> logger)
        : IRequestHandler<DeleteFAAuditCommand, BatchVoucherResponse>
{
    private readonly IFAAuditRepository _faAuditRepository = faAuditRepository ?? throw new ArgumentNullException(nameof(faAuditRepository));
    private readonly ILogger<DeleteFAAuditCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteFAAuditCommand message, CancellationToken cancellationToken)
    {
        List<MasterError> masterErrors = [];

        var existedConstrainIds = await _faAuditRepository.CheckExistedConstrain([.. message.FAAudits.Select(x => x.RefId)], message.FAAudits.FirstOrDefault().RefType);

        foreach (var fAAudit in message.FAAudits.Where(x => existedConstrainIds.Contains(x.RefId)))
        {
            var dataError = new DataError()
            {
                RefId = fAAudit.RefId,
                PostedDate = fAAudit.RefDate,
                RefDate = fAAudit.RefDate,
                RefNo = fAAudit.RefNo,
                RefType = fAAudit.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ kiểm kê này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
            };

            var masterError = new MasterError()
            {
                RefId = fAAudit.RefId,
                PostedDate = fAAudit.RefDate,
                RefDate = fAAudit.RefDate,
                RefNo = fAAudit.RefNo,
                RefType = fAAudit.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ kiểm kê này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
                DataError = dataError
            };

            masterErrors.Add(masterError);
        }

        await _faAuditRepository.DeleteAsync([.. message.FAAudits.Select(x => x.RefId)], message.FAAudits.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting FAAudit - FAAudit: {@FAAudit}", message.FAAudits);

        await _faAuditRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.FAAudits.Count(),
            MasterErrors = masterErrors
        };

        return response;
    }
}

public class DeleteFAAuditIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteFAAuditCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteFAAuditCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FAAudit.
    }
}