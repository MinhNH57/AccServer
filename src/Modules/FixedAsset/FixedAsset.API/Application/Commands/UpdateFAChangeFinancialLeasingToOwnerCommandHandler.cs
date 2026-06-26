namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;
using MediatR;

public class UpdateFAChangeFinancialLeasingToOwnerCommandHandler(IMapper mapper,
    IFAChangeFinancialLeasingToOwnerRepository fAChangeFinancialLeasingToOwnerRepository,
    IIdentityService identityService,
    ILogger<UpdateFAChangeFinancialLeasingToOwnerCommandHandler> logger)
        : IRequestHandler<UpdateFAChangeFinancialLeasingToOwnerCommand, FAChangeFinancialLeasingToOwnerUpdateResponse>
{
    private readonly IFAChangeFinancialLeasingToOwnerRepository _fAChangeFinancialLeasingToOwnerRepository = fAChangeFinancialLeasingToOwnerRepository ?? throw new ArgumentNullException(nameof(fAChangeFinancialLeasingToOwnerRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateFAChangeFinancialLeasingToOwnerCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<FAChangeFinancialLeasingToOwnerUpdateResponse> Handle(UpdateFAChangeFinancialLeasingToOwnerCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new FAChangeFinancialLeasingToOwnerUpdateResponse();

        var fAChangeFinancialLeasingToOwner = await _fAChangeFinancialLeasingToOwnerRepository.GetAsync(message.FAChangeFinancialLeasingToOwner.RefId);

        var _ = fAChangeFinancialLeasingToOwner.Update(
            message.FAChangeFinancialLeasingToOwner.TenantId,
            message.FAChangeFinancialLeasingToOwner.BranchId,
            message.FAChangeFinancialLeasingToOwner.FixedAssetId,
            message.FAChangeFinancialLeasingToOwner.FixedAssetName,
            message.FAChangeFinancialLeasingToOwner.DisplayOnBook,
            message.FAChangeFinancialLeasingToOwner.RefOrder,
            message.FAChangeFinancialLeasingToOwner.JournalMemo,
            message.FAChangeFinancialLeasingToOwner.PostedDate,
            message.FAChangeFinancialLeasingToOwner.RefDate,
            message.FAChangeFinancialLeasingToOwner.RefNo,
            message.FAChangeFinancialLeasingToOwner.FixedAssetCode,
            message.FAChangeFinancialLeasingToOwner.TotalAmount,
            DateTime.Now,
            message.FAChangeFinancialLeasingToOwner.OldOrgPriceAccount,
            message.FAChangeFinancialLeasingToOwner.NewOrgPriceAccount,
            message.FAChangeFinancialLeasingToOwner.OldDepreciationAccount,
            message.FAChangeFinancialLeasingToOwner.NewDepreciationAccount,
            message.FAChangeFinancialLeasingToOwner.OldOrgPrice,
            message.FAChangeFinancialLeasingToOwner.NewOrgPrice,
            message.FAChangeFinancialLeasingToOwner.OldDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.NewDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.OldAccumDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.NewAccumDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.OldRemainingAmount,
            message.FAChangeFinancialLeasingToOwner.NewRemainingAmount,
            message.FAChangeFinancialLeasingToOwner.OldLifeTime,
            message.FAChangeFinancialLeasingToOwner.NewLifeTime,
            message.FAChangeFinancialLeasingToOwner.OldLifeTimeRemaining,
            message.FAChangeFinancialLeasingToOwner.NewLifeTimeRemaining,
            message.FAChangeFinancialLeasingToOwner.OldDepreciationRateMonth,
            message.FAChangeFinancialLeasingToOwner.NewDepreciationRateMonth,
            message.FAChangeFinancialLeasingToOwner.OldMonthlyDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.NewMonthlyDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.OldDepreciationRateYear,
            message.FAChangeFinancialLeasingToOwner.NewDepreciationRateYear,
            message.FAChangeFinancialLeasingToOwner.OldYearlyDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.NewYearlyDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.OldIsLimitDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.NewIsLimitDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.OldDepreciationAmountByIncomeTax,
            message.FAChangeFinancialLeasingToOwner.NewDepreciationAmountByIncomeTax,
            message.FAChangeFinancialLeasingToOwner.OldRemainingAmountByIncomeTax,
            message.FAChangeFinancialLeasingToOwner.NewRemainingAmountByIncomeTax,
            message.FAChangeFinancialLeasingToOwner.OldMonthlyDepreciationAmountByIncomeTax,
            message.FAChangeFinancialLeasingToOwner.NewMonthlyDepreciationAmountByIncomeTax,
            userName,
            message.FAChangeFinancialLeasingToOwner.State,
            message.FAChangeFinancialLeasingToOwner.EditVersion,
            message.FAChangeFinancialLeasingToOwner.DifferentOrgPrice,
            message.FAChangeFinancialLeasingToOwner.DifferentDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.DifferentAccumDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.DifferentRemainingAmount,
            message.FAChangeFinancialLeasingToOwner.DifferentLifeTime,
            message.FAChangeFinancialLeasingToOwner.DifferentLifeTimeRemaining,
            message.FAChangeFinancialLeasingToOwner.DifferentDepreciationRateMonth,
            message.FAChangeFinancialLeasingToOwner.DifferentMonthlyDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.DifferentDepreciationRateYear,
            message.FAChangeFinancialLeasingToOwner.DifferentYearlyDepreciationAmount,
            message.FAChangeFinancialLeasingToOwner.DifferentDepreciationAmountByIncomeTax,
            message.FAChangeFinancialLeasingToOwner.DifferentRemainingAmountByIncomeTax,
            message.FAChangeFinancialLeasingToOwner.DifferentMonthlyDepreciationAmountByIncomeTax,
            message.FAChangeFinancialLeasingToOwner.AttachmentIdList,
            message.FAChangeFinancialLeasingToOwner.BranchName);

        fAChangeFinancialLeasingToOwner.ClearDetailData();

        foreach (var detail in message.FAChangeFinancialLeasingToOwnerDetails)
        {
            var entity = fAChangeFinancialLeasingToOwner.AddFAChangeFinancialLeasingToOwnerDetail(
                detail.SortOrder,
                detail.Description,
                detail.DebitAccount,
                detail.CreditAccount,
                detail.Amount,
                null,
                detail.ListItemCode,
                null,
                detail.State,
                detail.EditVersion);

            response.FAChangeFinancialLeasingToOwnerDetail.Add(_mapper.Map<FAChangeFinancialLeasingToOwnerDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating FAChangeFinancialLeasingToOwner - FAChangeFinancialLeasingToOwner: {@FAChangeFinancialLeasingToOwner}", fAChangeFinancialLeasingToOwner);

        await _fAChangeFinancialLeasingToOwnerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.FAChangeFinancialLeasingToOwner = [_mapper.Map<FAChangeFinancialLeasingToOwnerSaveFullResponse>(fAChangeFinancialLeasingToOwner)];

        return response;
    }
}

public class UpdateFAChangeFinancialLeasingToOwnerIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateFAChangeFinancialLeasingToOwnerCommand, FAChangeFinancialLeasingToOwnerUpdateResponse>> logger) 
    : IdentifiedCommandHandler<UpdateFAChangeFinancialLeasingToOwnerCommand, FAChangeFinancialLeasingToOwnerUpdateResponse>(mediator, requestManager, logger)
{
    protected override FAChangeFinancialLeasingToOwnerUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FAChangeFinancialLeasingToOwner.
    }
}