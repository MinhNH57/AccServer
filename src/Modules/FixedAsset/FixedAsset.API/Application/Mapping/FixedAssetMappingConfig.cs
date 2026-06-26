namespace FixedAsset.API.Application.Mapping;

public class FixedAssetMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset, FixedAssetDto>()
            .Map(dest => dest.FixedAssetId, src => src.Id);

        config.NewConfig<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset, FixedAssetSaveFullResponse>()
            .Map(dest => dest.FixedAssetId, src => src.Id);

        config.NewConfig<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset, FADecrementAvailableFixedAsset>()
            .Map(dest => dest.FixedAssetId, src => src.Id);

        config.NewConfig<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset, FATransferAvailableFixedAsset>()
            .Map(dest => dest.FixedAssetId, src => src.Id);

        config.NewConfig<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset, FAAdjustmentAvailableFixedAsset>()
            .Map(dest => dest.FixedAssetId, src => src.Id);

        config.NewConfig<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset, FAChangeFinancialLeasingToOwnerAvailableFixedAsset>()
            .Map(dest => dest.FixedAssetId, src => src.Id)
            .Map(dest => dest.OldIsLimitDepreciationAmount, src => src.IsLimitDepreciationAmount)
            .Map(dest => dest.OldOrgPrice, src => src.OrgPrice)
            .Map(dest => dest.OldDepreciationAmount, src => src.DepreciationAmount)
            .Map(dest => dest.OldAccumDepreciationAmount, src => src.AccumDepreciationAmount)
            .Map(dest => dest.OldMonthlyDepreciationAmountByIncomeTax, src => src.MonthlyDepreciationAmountByIncomeTax)
            .Map(dest => dest.OldYearlyDepreciationAmount, src => src.YearlyDepreciationAmount)
            .Map(dest => dest.OldDepreciationAmountByIncomeTax, src => src.DepreciationAmountByIncomeTax)
            .Map(dest => dest.OldRemainingAmountByIncomeTax, src => src.RemainingAmountByIncomeTax)
            .Map(dest => dest.OldDepreciationRateMonth, src => src.DepreciationRateMonth)
            .Map(dest => dest.OldMonthlyDepreciationAmount, src => src.MonthlyDepreciationAmount)
            .Map(dest => dest.OldDepreciationRateYear, src => src.DepreciationRateYear)
            .Map(dest => dest.OldRemainingAmount, src => src.RemainingAmount)
            .Map(dest => dest.OldLifeTime, src => src.LifeTime)
            .Map(dest => dest.OldLifeTimeRemaining, src => src.LifeTimeRemaining)
            .Map(dest => dest.OldOrgPriceAccount, src => src.OrgPriceAccount)
            .Map(dest => dest.OldDepreciationAccount, src => src.DepreciationAccount);

        config.NewConfig<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset, FADecrementCustomFixedAsset>()
                .Map(dest => dest.FixedAssetId, src => src.Id);

        config.NewConfig<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset, FAAdjustmentCustomFixedAsset>()
            .Map(dest => dest.FixedAssetId, src => src.Id);
    }
}