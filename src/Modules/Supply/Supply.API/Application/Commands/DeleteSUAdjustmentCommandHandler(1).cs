namespace Supply.API.Application.Commands;

using MediatR;
using Supply.Domain.AggregatesModel.SUAdjustmentAggregate;
using Supply.Infrastructure.Idempotency;

public class DeleteSUAdjustmentCommandHandler(ISUAdjustmentRepository suAdjustmentRepository,
    ILogger<DeleteSUAdjustmentCommandHandler> logger)
        : IRequestHandler<DeleteSUAdjustmentCommand, BatchVoucherResponse>
{
    private readonly ISUAdjustmentRepository _suAdjustmentRepository = suAdjustmentRepository ?? throw new ArgumentNullException(nameof(suAdjustmentRepository));
    private readonly ILogger<DeleteSUAdjustmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteSUAdjustmentCommand message, CancellationToken cancellationToken)
    {
        await _suAdjustmentRepository.DeleteAsync([.. message.SUAdjustments.Select(x => x.RefId)], message.SUAdjustments.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting SUAdjustment - SUAdjustment: {@SUAdjustment}", message.SUAdjustments);

        await _suAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.SUAdjustments.Count(),
            MasterErrors = []
        };

        return response;
    }
}

public class DeleteSUAdjustmentIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteSUAdjustmentCommand, BatchVoucherResponse>> logger) 
    : IdentifiedCommandHandler<DeleteSUAdjustmentCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting SUAdjustment.
    }
}