namespace Supply.API.Application.Mapping;

public class SUIncrementDetailSourceMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUIncrementDetailSource, SUIncrementDetailSourceDto>()
            .Map(dest => dest.SupplyDetailId, src => src.Id);

        config.NewConfig<SUIncrementDetailSource, SUIncrementDetailSaveFullResponse>()
            .Map(dest => dest.SupplyDetailId, src => src.Id);
    }
}
