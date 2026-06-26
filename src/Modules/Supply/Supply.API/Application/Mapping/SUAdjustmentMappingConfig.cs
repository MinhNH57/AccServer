namespace Supply.API.Application.Mapping;

public class SUAdjustmentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAdjustment, SUAdjustmentDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<SUAdjustment, SUAdjustmentSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
