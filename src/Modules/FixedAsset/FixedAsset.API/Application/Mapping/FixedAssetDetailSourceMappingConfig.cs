namespace FixedAsset.API.Application.Mapping;

public class FixedAssetDetailSourceMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FixedAssetDetailSource, FixedAssetDetailSourceDto>()
            .Map(dest => dest.FixedAssetDetailId, src => src.Id);

        config.NewConfig<FixedAssetDetailSource, FixedAssetDetailSaveFullResponse>()
            .Map(dest => dest.FixedAssetDetailId, src => src.Id);
    }
}
