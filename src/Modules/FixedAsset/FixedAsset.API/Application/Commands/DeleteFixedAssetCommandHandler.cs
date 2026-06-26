namespace FixedAsset.API.Application.Commands;

using FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;
using MediatR;

public class DeleteFixedAssetCommandHandler(IFixedAssetRepository fixedAssetRepository,
    ILogger<DeleteFixedAssetCommandHandler> logger)
        : IRequestHandler<DeleteFixedAssetCommand, BatchVoucherResponse>
{
    private readonly IFixedAssetRepository _fixedAssetRepository = fixedAssetRepository ?? throw new ArgumentNullException(nameof(fixedAssetRepository));
    private readonly ILogger<DeleteFixedAssetCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteFixedAssetCommand message, CancellationToken cancellationToken)
    {
        List<MasterError> masterErrors = [];

        var existedConstrainIds = await _fixedAssetRepository.CheckExistedConstrain([.. message.FixedAssets.Select(x => x.FixedAssetId)], message.FixedAssets.FirstOrDefault().RefType);

        foreach (var fixedAsset in message.FixedAssets.Where(x => existedConstrainIds.Contains(x.FixedAssetId)))
        {
            var dataError = new DataError()
            {
                RefId = fixedAsset.FixedAssetId,
                PostedDate = fixedAsset.RefDate,
                RefDate = fixedAsset.RefDate,
                RefNo = fixedAsset.RefNo,
                RefType = fixedAsset.RefType,
                DisplayOnBook = 0,
                Message = "Tài sản cố định đã có phát sinh.",
            };

            var masterError = new MasterError()
            {
                RefId = fixedAsset.FixedAssetId,
                PostedDate = fixedAsset.RefDate,
                RefDate = fixedAsset.RefDate,
                RefNo = fixedAsset.RefNo,
                RefType = fixedAsset.RefType,
                DisplayOnBook = 0,
                Message = "Tài sản cố định đã có phát sinh.",
                DataError = dataError
            };

            masterErrors.Add(masterError);
        }

        await _fixedAssetRepository.DeleteAsync([.. message.FixedAssets.Where(x => !existedConstrainIds.Contains(x.FixedAssetId)).Select(x => x.FixedAssetId)], message.FixedAssets.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting FixedAsset - FixedAsset: {@FixedAsset}", message.FixedAssets);

        await _fixedAssetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.FixedAssets.Count(),
            MasterErrors = masterErrors
        };

        return response;
    }
}

public class DeleteFixedAssetIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteFixedAssetCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteFixedAssetCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null;
    }
}