namespace FixedAsset.API.Application.Mapping;

public class FADecrementDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FADecrementDetail, FADecrementDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FADecrementDetail, FADecrementDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
