namespace FixedAsset.API.Application.Commands;

using MediatR;

public class DeleteFAChangeFinancialLeasingToOwnerCommandHandler(
    IFAChangeFinancialLeasingToOwnerRepository fAChangeFinancialLeasingToOwnerRepository,
    ILogger<DeleteFAChangeFinancialLeasingToOwnerCommandHandler> logger)
        : IRequestHandler<DeleteFAChangeFinancialLeasingToOwnerCommand, BatchVoucherResponse>
{
    private readonly IFAChangeFinancialLeasingToOwnerRepository _fAChangeFinancialLeasingToOwnerRepository = fAChangeFinancialLeasingToOwnerRepository ?? throw new ArgumentNullException(nameof(fAChangeFinancialLeasingToOwnerRepository));
    private readonly ILogger<DeleteFAChangeFinancialLeasingToOwnerCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteFAChangeFinancialLeasingToOwnerCommand message, CancellationToken cancellationToken)
    {
        List<MasterError> masterErrors = [];

        var existedConstrainIds = await _fAChangeFinancialLeasingToOwnerRepository.CheckExistedConstrain([.. message.FAChangeFinancialLeasingToOwners.Select(x => x.RefId)], message.FAChangeFinancialLeasingToOwners.FirstOrDefault().RefType);

        foreach (var fAChangeFinancialLeasingToOwner in message.FAChangeFinancialLeasingToOwners.Where(x => existedConstrainIds.Contains(x.RefId)))
        {
            var dataError = new DataError()
            {
                RefId = fAChangeFinancialLeasingToOwner.RefId,
                PostedDate = fAChangeFinancialLeasingToOwner.PostedDate,
                RefDate = fAChangeFinancialLeasingToOwner.RefDate,
                RefNo = fAChangeFinancialLeasingToOwner.RefNo,
                RefType = fAChangeFinancialLeasingToOwner.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
            };

            var masterError = new MasterError()
            {
                RefId = fAChangeFinancialLeasingToOwner.RefId,
                PostedDate = fAChangeFinancialLeasingToOwner.PostedDate,
                RefDate = fAChangeFinancialLeasingToOwner.RefDate,
                RefNo = fAChangeFinancialLeasingToOwner.RefNo,
                RefType = fAChangeFinancialLeasingToOwner.RefType,
                DisplayOnBook = 0,
                Message = "TSCĐ đã có phát sinh đánh giá lại, tính khấu hao, ghi giảm, điều chuyển hoặc chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu sau ngày hạch toán của chứng từ chuyển TSCĐ thuê tài chính thành TSCĐ sở hữu này. Để xóa chứng từ này thì phải xóa các chứng từ liên quan.",
                DataError = dataError
            };

            masterErrors.Add(masterError);
        }

        await _fAChangeFinancialLeasingToOwnerRepository.DeleteAsync([.. message.FAChangeFinancialLeasingToOwners.Select(x => x.RefId)], message.FAChangeFinancialLeasingToOwners.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting FAChangeFinancialLeasingToOwner - FAChangeFinancialLeasingToOwner: {@FAChangeFinancialLeasingToOwner}", message.FAChangeFinancialLeasingToOwners);

        await _fAChangeFinancialLeasingToOwnerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.FAChangeFinancialLeasingToOwners.Count(),
            MasterErrors = masterErrors
        };

        return response;
    }
}

public class DeleteFAChangeFinancialLeasingToOwnerIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteFAChangeFinancialLeasingToOwnerCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteFAChangeFinancialLeasingToOwnerCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting FAChangeFinancialLeasingToOwner.
    }
}