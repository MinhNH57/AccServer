namespace FixedAsset.API.Application.Commands;

using FixedAsset.Domain.AggregatesModel.FixedAssetAggregate;
using MediatR;

public class UpdateFixedAssetCommandHandler(IMapper mapper,
    IFixedAssetRepository fixedAssetRepository,
    IIdentityService identityService,
    ILogger<UpdateFixedAssetCommandHandler> logger)
        : IRequestHandler<UpdateFixedAssetCommand, FAIncrementUpdateResponse>
{
    private readonly IFixedAssetRepository _fixedAssetRepository = fixedAssetRepository ?? throw new ArgumentNullException(nameof(fixedAssetRepository));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UpdateFixedAssetCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<FAIncrementUpdateResponse> Handle(UpdateFixedAssetCommand message, CancellationToken cancellationToken)
    {
        var userName = _identityService.GetUserName();
        var response = new FAIncrementUpdateResponse();

        var fixedAsset = await _fixedAssetRepository.GetAsync(message.FixedAsset.FixedAssetId);

        var _ = fixedAsset.Update(
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
            message.FixedAsset.AttachmentIdList,
            userName,
            null,
            null,
            message.FixedAsset.EditVersion,
            message.FixedAsset.LocationWarehouseId,
            message.FixedAsset.LocationWarehouseCode,
            message.FixedAsset.LocationWarehouseName);

        fixedAsset.ClearDetailData();

        foreach (var allocation in message.FixedAssetDetailAllocations)
        {
            var entity = fixedAsset.AddFixedAssetDetailAllocation(
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
                allocation.EditVersion);

            response.FixedAssetDetailAllocation.Add(_mapper.Map<FixedAssetDetailSaveFullResponse>(entity));
        }

        foreach (var source in message.FixedAssetDetailSources)
        {
            var entity = fixedAsset.AddFixedAssetDetailSource(
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
                source.EditVersion);

            response.FixedAssetDetailSource.Add(_mapper.Map<FixedAssetDetailSaveFullResponse>(entity));
        }

        foreach (var detail in message.FixedAssetDetails)
        {
            var entity = fixedAsset.AddFixedAssetDetail(
                detail.SortOrder,
                detail.Quantity,
                detail.Description,
                detail.WarrantyTime,
                detail.Unit,
                detail.EditVersion);

            response.FixedAssetDetail.Add(_mapper.Map<FixedAssetDetailSaveFullResponse>(entity));
        }

        foreach (var accessory in message.FixedAssetDetailAccessories)
        {
            var entity = fixedAsset.AddFixedAssetDetailAccessory(
                accessory.SortOrder,
                accessory.Quantity,
                accessory.Amount,
                accessory.Description,
                accessory.Unit,
                accessory.EditVersion);

            response.FixedAssetDetailAccessory.Add(_mapper.Map<FixedAssetDetailSaveFullResponse>(entity));
        }

        _logger.LogInformation("Updating FixedAsset - FixedAsset: {@FixedAsset}", fixedAsset);

        await _fixedAssetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        response.FixedAsset = [_mapper.Map<FixedAssetSaveFullResponse>(fixedAsset)];

        return response;
    }
}

public class UpdateFixedAssetIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<UpdateFixedAssetCommand, FAIncrementUpdateResponse>> logger)
    : IdentifiedCommandHandler<UpdateFixedAssetCommand, FAIncrementUpdateResponse>(mediator, requestManager, logger)
{
    protected override FAIncrementUpdateResponse CreateResultForDuplicateRequest()
    {
        return null; // Ignore duplicate requests for updating FixedAsset.
    }
}