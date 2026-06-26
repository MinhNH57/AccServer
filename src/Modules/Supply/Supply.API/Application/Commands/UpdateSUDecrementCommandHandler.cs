namespace Supply.API.Application.Commands;

using MediatR;
using Supply.API.Application.Models;
using Supply.Domain.AggregatesModel.SUDecrementAggregate;
using Supply.Infrastructure.Idempotency;

public class UpdateSUDecrementCommandHandler(IMapper mapper,
    ISUDecrementRepository suDecrementRepository,
    IIdentityService identityService,
    ILogger<UpdateSUDecrementCommandHandler> logger)
        : IRequestHandler<UpdateSUDecrementCommand, SUDecrementUpdateResponse>
{
    private readonly ISUDecrementRepository _suDecrementRepository = suDecrementRepository ?? throw new ArgumentNullException(nameof(suDecrementRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateSUDecrementCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<SUDecrementUpdateResponse> Handle(UpdateSUDecrementCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new SUDecrementUpdateResponse();

        var suDecrement = await _suDecrementRepository.GetAsync(message.SUDecrement.RefId);

        var _ = suDecrement.Update(
            message.SUDecrement.TenantId,
            message.SUDecrement.BranchId,
            message.SUDecrement.DisplayOnBook,
            message.SUDecrement.RefOrder,
            message.SUDecrement.RefDate,
            DateTime.Now,
            message.SUDecrement.IsPostedManagement,
            message.SUDecrement.IsPostedFinance,
            message.SUDecrement.TotalAmount,
            message.SUDecrement.RefNo,
            message.SUDecrement.JournalMemo,
            userName,
            message.SUDecrement.State,
            message.SUDecrement.BranchName,
            message.SUDecrement.EditVersion,
            message.SUDecrement.AttachmentIdList);

        suDecrement.ClearDetailData();

        foreach (var detail in message.SUDecrementDetails)
        {
            var entity = suDecrement.AddSUDecrementDetail(
                detail.SupplyId,
                detail.SupplyCode,
                detail.SupplyName,
                detail.OrganizationUnitId,
                detail.SUAllocationId,
                detail.SUAuditRefId,
                detail.SortOrder,
                detail.UseQuantity,
                detail.DecrementQuantity,
                detail.DecrementAmount,
                detail.RemainingDecrementAmount,
                detail.Reason,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.State,
                detail.EditVersion);

            response.SUDecrementDetail.Add(_mapper.Map<SUDecrementDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating SUDecrement - SUDecrement: {@SUDecrement}", suDecrement);

        await _suDecrementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.SUDecrement = [_mapper.Map<SUDecrementSaveFullResponse>(suDecrement)];

        return response;
    }
}

public class UpdateSUDecrementIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateSUDecrementCommand, SUDecrementUpdateResponse>> logger) : IdentifiedCommandHandler<UpdateSUDecrementCommand, SUDecrementUpdateResponse>(mediator, requestManager, logger)
{
    protected override SUDecrementUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating SUDecrement.
    }
}