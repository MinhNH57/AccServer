namespace Supply.API.Application.Mapping;

public class SUIncrementDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUIncrementDetail, SUIncrementDetailDto>()
            .Map(dest => dest.SupplyDetailId, src => src.Id);

        config.NewConfig<SUIncrementDetail, SUIncrementDetailSaveFullResponse>()
            .Map(dest => dest.SupplyDetailId, src => src.Id);
    }
}
