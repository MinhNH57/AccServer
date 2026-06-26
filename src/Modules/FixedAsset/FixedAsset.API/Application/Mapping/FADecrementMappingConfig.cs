namespace FixedAsset.API.Application.Mapping;

public class FADecrementMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FADecrement, FADecrementDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<FADecrement, FADecrementSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
