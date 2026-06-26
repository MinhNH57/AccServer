namespace Supply.API.Application.Commands;

using MediatR;
using Supply.Domain.AggregatesModel.SUAuditAggregate;
using Supply.Infrastructure.Idempotency;

public class DeleteSUAuditCommandHandler(ISUAuditRepository suAuditRepository,
    ILogger<DeleteSUAuditCommandHandler> logger)
        : IRequestHandler<DeleteSUAuditCommand, BatchVoucherResponse>
{
    private readonly ISUAuditRepository _suAuditRepository = suAuditRepository ?? throw new ArgumentNullException(nameof(suAuditRepository));
    private readonly ILogger<DeleteSUAuditCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteSUAuditCommand message, CancellationToken cancellationToken)
    {
        await _suAuditRepository.DeleteAsync([.. message.SUAudits.Select(x => x.RefId)], message.SUAudits.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting SUAudit - SUAudit: {@SUAudit}", message.SUAudits);

        await _suAuditRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.SUAudits.Count(),
            MasterErrors = []
        };

        return response;
    }
}

public class DeleteSUAuditIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteSUAuditCommand, BatchVoucherResponse>> logger) 
    : IdentifiedCommandHandler<DeleteSUAuditCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting SUAudit.
    }
}