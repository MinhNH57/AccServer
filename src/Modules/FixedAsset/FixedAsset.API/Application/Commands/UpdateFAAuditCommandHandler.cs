namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;
using MediatR;

public class UpdateFAAuditCommandHandler(IMapper mapper,
    IFAAuditRepository fAAuditRepository,
    IIdentityService identityService,
    ILogger<UpdateFAAuditCommandHandler> logger)
        : IRequestHandler<UpdateFAAuditCommand, FAAuditUpdateResponse>
{
    private readonly IFAAuditRepository _fAAuditRepository = fAAuditRepository ?? throw new ArgumentNullException(nameof(fAAuditRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateFAAuditCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<FAAuditUpdateResponse> Handle(UpdateFAAuditCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new FAAuditUpdateResponse();

        var fAAudit = await _fAAuditRepository.GetAsync(message.FAAudit.RefId);

        var _ = fAAudit.Update(
            message.FAAudit.TenantId,
            message.FAAudit.BranchId,
            message.FAAudit.DisplayOnBook,
            message.FAAudit.RefDate,
            message.FAAudit.RefTime,
            message.FAAudit.InventoryDate,
            DateTime.Now,
            message.FAAudit.IsExecuted,
            message.FAAudit.RefNo,
            message.FAAudit.JournalMemo,
            message.FAAudit.Summary,
            userName,
            message.FAAudit.State,
            message.FAAudit.EditVersion,
            message.FAAudit.AuditMember,
            message.FAAudit.AttachmentIdList,
            message.FAAudit.BranchName);

        fAAudit.ClearDetailData();

        foreach (var detail in message.FAAuditDetails)
        {
            var entity = fAAudit.AddFAAuditDetail(
                detail.FixedAssetId,
                detail.OrganizationUnitId,
                detail.ExistInStock,
                detail.Quality,
                detail.Recommendation,
                detail.SortOrder,
                detail.OrgPrice,
                detail.DepreciationAmount,
                detail.AccumDepreciationAmount,
                detail.RemainingAmount,
                detail.Note,
                detail.OrganizationUnitCode,
                detail.OrganizationUnitName,
                detail.FixedAssetCode,
                detail.FixedAssetName,
                detail.State,
                detail.EditVersion);

            response.FAAuditDetail.Add(_mapper.Map<FAAuditDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating FAAudit - FAAudit: {@FAAudit}", fAAudit);

        await _fAAuditRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.FAAudit = [_mapper.Map<FAAuditSaveFullResponse>(fAAudit)];

        return response;
    }
}

public class UpdateFAAuditIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateFAAuditCommand, FAAuditUpdateResponse>> logger) : IdentifiedCommandHandler<UpdateFAAuditCommand, FAAuditUpdateResponse>(mediator, requestManager, logger)
{
    protected override FAAuditUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FAAudit.
    }
}