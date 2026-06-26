namespace FixedAsset.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using MassTransit;
using MediatR;
using Spectre.Console;

public class CreateFAChangeFinancialLeasingToOwnerCommandHandler(IMapper mapper,
    IFAChangeFinancialLeasingToOwnerRepository fAChangeFinancialLeasingToOwnerRepository,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateFAChangeFinancialLeasingToOwnerCommandHandler> logger)
        : IRequestHandler<CreateFAChangeFinancialLeasingToOwnerCommand, FAChangeFinancialLeasingToOwnerCreateResponse>
{
    private readonly IFAChangeFinancialLeasingToOwnerRepository _fAChangeFinancialLeasingToOwnerRepository = fAChangeFinancialLeasingToOwnerRepository ?? throw new ArgumentNullException(nameof(fAChangeFinancialLeasingToOwnerRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateFAChangeFinancialLeasingToOwnerCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<FAChangeFinancialLeasingToOwnerCreateResponse> Handle(CreateFAChangeFinancialLeasingToOwnerCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var fAChangeFinancialLeasingToOwner = new FAChangeFinancialLeasingToOwner(
            null,
            message.FAChangeFinancialLeasingToOwner.BranchId,
            message.FAChangeFinancialLeasingToOwner.FixedAssetId,
            message.FAChangeFinancialLeasingToOwner.FixedAssetName,
            message.FAChangeFinancialLeasingToOwner.DisplayOnBook,
            message.FAChangeFinancialLeasingToOwner.RefType,
            null,
            message.FAChangeFinancialLeasingToOwner.JournalMemo,
            message.FAChangeFinancialLeasingToOwner.PostedDate,
            message.FAChangeFinancialLeasingToOwner.RefDate,
            message.FAChangeFinancialLeasingToOwner.RefNo,
            message.FAChangeFinancialLeasingToOwner.FixedAssetCode,
            message.FAChangeFinancialLeasingToOwner.TotalAmount,
            DateTime.Now,
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
            userName,
            message.FAChangeFinancialLeasingToOwner.State,
            editVersion,
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
            null,
            null);

        foreach (var detail in message.FAChangeFinancialLeasingToOwnerDetails)
        {
            fAChangeFinancialLeasingToOwner.AddFAChangeFinancialLeasingToOwnerDetail(
                detail.SortOrder,
                detail.Description,
                detail.DebitAccount,
                detail.CreditAccount,
                detail.Amount,
                null,
                detail.ListItemCode,
                null,
                detail.State,
                editVersion);
        }

        _logger.LogInformation("Creating FAChangeFinancialLeasingToOwner - FAChangeFinancialLeasingToOwner: {@FAChangeFinancialLeasingToOwner}", fAChangeFinancialLeasingToOwner);

        var entity = _fAChangeFinancialLeasingToOwnerRepository.Add(fAChangeFinancialLeasingToOwner);

        await _fAChangeFinancialLeasingToOwnerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new FAChangeFinancialLeasingToOwnerCreateResponse()
        {
            FAChangeFinancialLeasingToOwner = [_mapper.Map<FAChangeFinancialLeasingToOwnerSaveFullResponse>(entity)],
            FAChangeFinancialLeasingToOwnerDetail = [.. entity.FAChangeFinancialLeasingToOwnerDetails.Select(_mapper.Map<FAChangeFinancialLeasingToOwnerDetailSaveFullResponse>)],
        };
    }
}

public class CreateFAChangeFinancialLeasingToOwnerIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateFAChangeFinancialLeasingToOwnerCommand, FAChangeFinancialLeasingToOwnerCreateResponse>> logger)
    : IdentifiedCommandHandler<CreateFAChangeFinancialLeasingToOwnerCommand, FAChangeFinancialLeasingToOwnerCreateResponse>(mediator, requestManager, logger)
{
    protected override FAChangeFinancialLeasingToOwnerCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}