namespace Supply.API.Application.Commands;

using MediatR;
using Supply.Domain.AggregatesModel.SUDecrementAggregate;
using Supply.Infrastructure.Idempotency;

public class DeleteSUDecrementCommandHandler(
    ISUDecrementRepository suDecrementRepository,
    ILogger<DeleteSUDecrementCommandHandler> logger)
        : IRequestHandler<DeleteSUDecrementCommand, BatchVoucherResponse>
{
    private readonly ISUDecrementRepository _suDecrementRepository = suDecrementRepository ?? throw new ArgumentNullException(nameof(suDecrementRepository));
    private readonly ILogger<DeleteSUDecrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteSUDecrementCommand message, CancellationToken cancellationToken)
    {
        await _suDecrementRepository.DeleteAsync([.. message.SUDecrements.Select(x => x.RefId)], message.SUDecrements.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting SUDecrement - SUDecrement: {@SUDecrement}", message.SUDecrements);

        await _suDecrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.SUDecrements.Count(),
            MasterErrors = []
        };

        return response;
    }
}

public class DeleteSUDecrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteSUDecrementCommand, BatchVoucherResponse>> logger) 
    : IdentifiedCommandHandler<DeleteSUDecrementCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating SUDecrement.
    }
}