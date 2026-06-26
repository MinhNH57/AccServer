namespace Supply.API.Application.Mapping;

public class SUDecrementDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUDecrementDetail, SUDecrementDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<SUDecrementDetail, SUDecrementDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
