namespace Supply.API.Application.Commands;

using MediatR;
using Supply.Domain.AggregatesModel.SUIncrementAggregate;
using Supply.Infrastructure.Idempotency;

public class DeleteSUIncrementCommandHandler(ISUIncrementRepository suIncrementRepository,
    ILogger<DeleteSUIncrementCommandHandler> logger)
        : IRequestHandler<DeleteSUIncrementCommand, BatchVoucherResponse>
{
    private readonly ISUIncrementRepository _suIncrementRepository = suIncrementRepository ?? throw new ArgumentNullException(nameof(suIncrementRepository));
    private readonly ILogger<DeleteSUIncrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteSUIncrementCommand message, CancellationToken cancellationToken)
    {
        List<MasterError> masterErrors = [];

        var existedConstrainIds = await _suIncrementRepository.CheckExistedConstrain([.. message.SUIncrements.Select(x => x.SupplyId)], message.SUIncrements.FirstOrDefault().RefType);

        foreach (var sUIncrement in message.SUIncrements.Where(x => existedConstrainIds.Contains(x.SupplyId)))
        {
            var dataError = new DataError()
            {
                RefId = sUIncrement.SupplyId,
                PostedDate = (DateTime)sUIncrement.RefDate,
                RefDate = (DateTime)sUIncrement.RefDate,
                RefNo = sUIncrement.RefNo,
                RefType = sUIncrement.RefType,
                DisplayOnBook = 0,
                Message = "Công cụ dụng cụ đã có phát sinh.",
            };

            var masterError = new MasterError()
            {
                RefId = sUIncrement.SupplyId,
                PostedDate = (DateTime)sUIncrement.RefDate,
                RefDate = (DateTime)sUIncrement.RefDate,
                RefNo = sUIncrement.RefNo,
                RefType = sUIncrement.RefType,
                DisplayOnBook = 0,
                Message = "Công cụ dụng cụ đã có phát sinh.",
                DataError = dataError
            };

            masterErrors.Add(masterError);
        }

        await _suIncrementRepository.DeleteAsync([.. message.SUIncrements.Where(x => !existedConstrainIds.Contains(x.SupplyId)).Select(x => x.SupplyId)], message.SUIncrements.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting FixedAsset - FixedAsset: {@FixedAsset}", message.SUIncrements);

        await _suIncrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.SUIncrements.Count(),
            MasterErrors = masterErrors
        };

        return response;
    }
}

public class DeleteSUIncrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteSUIncrementCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteSUIncrementCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting SUIncrement.
    }
}