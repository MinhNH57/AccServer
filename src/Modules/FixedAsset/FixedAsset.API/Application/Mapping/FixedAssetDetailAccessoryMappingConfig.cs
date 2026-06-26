namespace FixedAsset.API.Application.Mapping;

public class FixedAssetDetailAccessoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FixedAssetDetailAccessory, FixedAssetDetailAccessoryDto>()
            .Map(dest => dest.FixedAssetDetailId, src => src.Id);

        config.NewConfig<FixedAssetDetailAccessory, FixedAssetDetailSaveFullResponse>()
            .Map(dest => dest.FixedAssetDetailId, src => src.Id);
    }
}
