namespace FixedAsset.API.Application.Mapping;

public class FixedAssetDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FixedAssetDetail, FixedAssetDetailDto>()
            .Map(dest => dest.FixedAssetDetailId, src => src.Id);

        config.NewConfig<FixedAssetDetail, FixedAssetDetailSaveFullResponse>()
            .Map(dest => dest.FixedAssetDetailId, src => src.Id);
    }
}
