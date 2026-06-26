namespace FixedAsset.API.Application.Mapping;

public class FADepreciationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FADepreciation, FADepreciationDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<FADepreciation, FADepreciationSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
