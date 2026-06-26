namespace Supply.API.Application.Commands;

using MediatR;
using Supply.API.Application.Models;
using Supply.Domain.AggregatesModel.SUAuditAggregate;
using System;

public class UpdateSUAuditCommandHandler(IMapper mapper,
    ISUAuditRepository suAuditRepository,
    IIdentityService identityService,
    ILogger<UpdateSUAuditCommandHandler> logger)
        : IRequestHandler<UpdateSUAuditCommand, SUAuditUpdateResponse>
{
    private readonly ISUAuditRepository _suAuditRepository = suAuditRepository ?? throw new ArgumentNullException(nameof(suAuditRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateSUAuditCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<SUAuditUpdateResponse> Handle(UpdateSUAuditCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new SUAuditUpdateResponse();

        var suAudit = await _suAuditRepository.GetAsync(message.SUAudit.RefId);

        var _ = suAudit.Update(
            message.SUAudit.TenantId,
            message.SUAudit.BranchId,
            message.SUAudit.DisplayOnBook,
            message.SUAudit.RefDate,
            message.SUAudit.RefTime,
            message.SUAudit.BalanceDate,
            DateTime.Now,
            message.SUAudit.IsExecuted,
            message.SUAudit.RefNo,
            message.SUAudit.JournalMemo,
            message.SUAudit.Summary,
            userName,
            message.SUAudit.State,
            message.SUAudit.EditVersion,
            message.SUAudit.AuditMember,
            message.SUAudit.BranchName,
            message.SUAudit.AttachmentIdList);

        suAudit.ClearDetailData();

        foreach (var detail in message.SUAuditDetails)
        {
            var entity = suAudit.AddSUAuditDetail(
                detail.SupplyId,
                detail.SupplyCode,
                detail.SupplyName,
                detail.OrganizationUnitId,
                detail.SortOrder,
                detail.Action,
                detail.QuantityOnBook,
                detail.QuantityInventory,
                detail.DiffQuantity,
                detail.GoodQuantity,
                detail.DamageQuantity,
                detail.ExecuteQuantity,
                detail.Note,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.State,
                detail.EditVersion,
                detail.Unit);

            response.SUAuditDetail.Add(_mapper.Map<SUAuditDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating SUAudit - SUAudit: {@SUAudit}", suAudit);

        await _suAuditRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.SUAudit = [_mapper.Map<SUAuditSaveFullResponse>(suAudit)];

        return response;
    }
}

public class UpdateSUAuditIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateSUAuditCommand, SUAuditUpdateResponse>> logger) 
    : IdentifiedCommandHandler<UpdateSUAuditCommand, SUAuditUpdateResponse>(mediator, requestManager, logger)
{
    protected override SUAuditUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating SUAudit.
    }
}