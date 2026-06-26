namespace FixedAsset.API.Application.Mapping;

public class FixedAssetDetailAllocationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FixedAssetDetailAllocation, FixedAssetDetailAllocationDto>()
            .Map(dest => dest.FixedAssetDetailId, src => src.Id);

        config.NewConfig<FixedAssetDetailAllocation, FixedAssetDetailSaveFullResponse>()
            .Map(dest => dest.FixedAssetDetailId, src => src.Id);
    }
}
