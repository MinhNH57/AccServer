namespace Supply.API.Application.Mapping;

public class SUDecrementMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUDecrement, SUDecrementDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<SUDecrement, SUDecrementSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
