namespace FixedAsset.API.Application.Mapping;

public class FADecrementDetailPostMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FADecrementDetailPost, FADecrementDetailPostDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FADecrementDetailPost, FADecrementDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
