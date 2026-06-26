namespace Supply.API.Application.Commands;
using MediatR;
using Supply.Domain.AggregatesModel.SUTransferAggregate;
using Supply.Infrastructure.Idempotency;

public class DeleteSUTransferCommandHandler(
    ISUTransferRepository suTransferRepository,
    ILogger<DeleteSUTransferCommandHandler> logger)
        : IRequestHandler<DeleteSUTransferCommand, BatchVoucherResponse>
{
    private readonly ISUTransferRepository _suTransferRepository = suTransferRepository ?? throw new ArgumentNullException(nameof(suTransferRepository));
    private readonly ILogger<DeleteSUTransferCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteSUTransferCommand message, CancellationToken cancellationToken)
    {

        await _suTransferRepository.DeleteAsync([.. message.SUTransfers.Select(x => x.RefId)], message.SUTransfers.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting SUTransfer - SUTransfer: {@SUTransfer}", message.SUTransfers);

        await _suTransferRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.SUTransfers.Count(),
            MasterErrors = []
        };

        return response;
    }
}

public class DeleteSUTransferIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteSUTransferCommand, BatchVoucherResponse>> logger)
    : IdentifiedCommandHandler<DeleteSUTransferCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting SUTransfer.
    }
}