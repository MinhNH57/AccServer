namespace Supply.API.Application.Commands;

using MediatR;
using Supply.Domain.AggregatesModel.SUAllocationAggregate;
using Supply.Infrastructure.Idempotency;

public class DeleteSUAllocationCommandHandler(
    ISUAllocationRepository suAllocationRepository,
    ILogger<DeleteSUAllocationCommandHandler> logger)
        : IRequestHandler<DeleteSUAllocationCommand, BatchVoucherResponse>
{
    private readonly ISUAllocationRepository _suAllocationRepository = suAllocationRepository ?? throw new ArgumentNullException(nameof(suAllocationRepository));
    private readonly ILogger<DeleteSUAllocationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<BatchVoucherResponse> Handle(DeleteSUAllocationCommand message, CancellationToken cancellationToken)
    {
        await _suAllocationRepository.DeleteAsync([.. message.SUAllocations.Select(x => x.RefId)], message.SUAllocations.FirstOrDefault().RefType);

        _logger.LogInformation("Deleting SUAllocation - SUAllocation: {@SUAllocation}", message.SUAllocations);

        await _suAllocationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var response = new BatchVoucherResponse()
        {
            TotalProcess = message.SUAllocations.Count(),
            MasterErrors = []
        };

        return response;
    }
}

public class DeleteSUAllocationIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<DeleteSUAllocationCommand, BatchVoucherResponse>> logger) 
    : IdentifiedCommandHandler<DeleteSUAllocationCommand, BatchVoucherResponse>(mediator, requestManager, logger)
{
    protected override BatchVoucherResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for deleting SUAllocation.
    }
}