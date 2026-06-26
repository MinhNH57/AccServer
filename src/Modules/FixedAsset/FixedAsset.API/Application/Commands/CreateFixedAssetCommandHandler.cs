namespace FixedAsset.API.Application.Commands;

using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;
using MassTransit;
using MediatR;

public class CreateFixedAssetCommandHandler(IMapper mapper,
    IFixedAssetRepository fixedAssetRepository,
    IIdentityService identityService,
    IPublishEndpoint publishEndpoint,
    ICurrentUser currentUser,
    ILogger<CreateFixedAssetCommandHandler> logger)
        : IRequestHandler<CreateFixedAssetCommand, FAIncrementCreateResponse>
{
    private readonly IFixedAssetRepository _fixedAssetRepository = fixedAssetRepository ?? throw new ArgumentNullException(nameof(fixedAssetRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateFixedAssetCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    private readonly ICurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

    public async Task<FAIncrementCreateResponse> Handle(CreateFixedAssetCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var editVersion = new Random().Next(100000000, int.MaxValue);

        var fixedAsset = new FixedAsset(
            message.FixedAsset.FixedAssetCategoryId,
            message.FixedAsset.OrganizationUnitId,
            null,
            message.FixedAsset.AccountObjectId,
            message.FixedAsset.AccountObjectCode,
            message.FixedAsset.ProductionYear,
            message.FixedAsset.Quality,
            message.FixedAsset.LifeTimeUnit,
            0,
            0,
            0,
            message.FixedAsset.RefDate,
            message.FixedAsset.DeliveryRecordDate,
            message.FixedAsset.DepreciationDate,
            message.FixedAsset.IsNotDepreciation,
            message.FixedAsset.IsLimitDepreciationAmount,
            null,
            null,
            null,
            message.FixedAsset.IsFixedAssetOfStateBudget,
            null,
            message.FixedAsset.OrgPrice,
            message.FixedAsset.DepreciationAmount,
            message.FixedAsset.LifeTime,
            message.FixedAsset.LifeTimeRemaining,
            message.FixedAsset.DepreciationRateMonth,
            null,
            message.FixedAsset.MonthlyDepreciationAmountByIncomeTax,
            message.FixedAsset.MonthlyDepreciationAmount,
            message.FixedAsset.YearlyDepreciationAmount,
            message.FixedAsset.AccumDepreciationAmount,
            message.FixedAsset.RemainingAmount,
            message.FixedAsset.DepreciationAmountByIncomeTax,
            message.FixedAsset.RefNo,
            message.FixedAsset.FixedAssetCode,
            message.FixedAsset.FixedAssetName,
            message.FixedAsset.Manufacturer,
            message.FixedAsset.OrgPriceAccount,
            message.FixedAsset.MadeIn,
            message.FixedAsset.DepreciationAccount,
            message.FixedAsset.Source,
            message.FixedAsset.CapacityMachine,
            message.FixedAsset.SerialNumber,
            message.FixedAsset.VendorName,
            message.FixedAsset.GuaranteeDuration,
            message.FixedAsset.GuaranteeCondition,
            message.FixedAsset.DeliveryRecordNo,
            message.FixedAsset.OrganizationUnitCode,
            message.FixedAsset.OrganizationUnitName,
            message.FixedAsset.FixedAssetCategoryName,
            message.FixedAsset.LifeTimeInMonth,
            message.FixedAsset.LifeTimeRemainingInMonth,
            message.FixedAsset.DepreciationRateYear,
            message.FixedAsset.FixedAssetState,
            message.FixedAsset.FixedAssetCategoryCode,
            null,
            message.FixedAsset.ReasonNotDepreciation,
            null,
            null,
            null,
            message.FixedAsset.RefType,
            message.FixedAsset.AttachmentIdList,
            userName,
            null,
            null,
            editVersion,
            message.FixedAsset.LocationWarehouseId,
            message.FixedAsset.LocationWarehouseCode,
            message.FixedAsset.LocationWarehouseName);

        foreach (var allocation in message.FixedAssetDetailAllocations)
        {
            fixedAsset.AddFixedAssetDetailAllocation(
                allocation.ObjectId,
                allocation.ExpenseItemId,
                allocation.ListItemId,
                allocation.SortOrder,
                allocation.ObjectType,
                allocation.AllocationRate,
                allocation.CostAccount,
                allocation.ObjectCode,
                allocation.ObjectName,
                allocation.ExpenseItemCode,
                allocation.ListItemCode,
                editVersion);
        }

        foreach (var source in message.FixedAssetDetailSources)
        {
            fixedAsset.AddFixedAssetDetailSource(
                source.RefId,
                source.RefDetailId,
                source.SortOrder,
                source.RefType,
                source.JournalMemo,
                source.CreditAccount,
                source.DebitAccount,
                source.RefNo,
                source.Amount,
                source.RefDate,
                source.PostedDate,
                source.DetailPostOrder,
                editVersion);
        }

        foreach (var detail in message.FixedAssetDetails)
        {
            fixedAsset.AddFixedAssetDetail(
                detail.SortOrder,
                detail.Quantity,
                detail.Description,
                detail.WarrantyTime,
                detail.Unit,
                editVersion);
        }

        foreach (var accessory in message.FixedAssetDetailAccessories)
        {
            fixedAsset.AddFixedAssetDetailAccessory(
                accessory.SortOrder,
                accessory.Quantity,
                accessory.Amount,
                accessory.Description,
                accessory.Unit,
                editVersion);
        }

        _logger.LogInformation("Creating FixedAsset - FixedAsset: {@FixedAsset}", fixedAsset);

        var entity = _fixedAssetRepository.Add(fixedAsset);

        await _fixedAssetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _publishEndpoint.Publish<ICreatedSuccessfully>(
            new { entity.Id, entity.RefType },
            (publishContext) => publishContext.Headers.Set(TenantConstant.TenantIdHeader, _currentUser.TenantId),
            cancellationToken);

        return new FAIncrementCreateResponse()
        {
            FixedAsset = [_mapper.Map<FixedAssetSaveFullResponse>(entity)],
            FixedAssetDetailAllocation = [.. entity.FixedAssetDetailAllocations.Select(_mapper.Map<FixedAssetDetailSaveFullResponse>)],
            FixedAssetDetailSource = [.. entity.FixedAssetDetailSources.Select(_mapper.Map<FixedAssetDetailSaveFullResponse>)],
            FixedAssetDetail = [.. entity.FixedAssetDetails.Select(_mapper.Map<FixedAssetDetailSaveFullResponse>)],
            FixedAssetDetailAccessory = [.. entity.FixedAssetDetailAccessories.Select(_mapper.Map<FixedAssetDetailSaveFullResponse>)]
        };
    }
}


public class CreateFixedAssetIdentifiedCommandHandler(
    IMediator mediator, IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateFixedAssetCommand, FAIncrementCreateResponse>> logger) 
    : IdentifiedCommandHandler<CreateFixedAssetCommand, FAIncrementCreateResponse>(mediator, requestManager, logger)
{
    protected override FAIncrementCreateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for creating fixed asset.
    }
}