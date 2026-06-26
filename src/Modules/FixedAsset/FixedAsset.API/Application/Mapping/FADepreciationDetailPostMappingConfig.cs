namespace FixedAsset.API.Application.Mapping;

public class FADepreciationDetailPostMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FADepreciationDetailPost, FADepreciationDetailPostDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FADepreciationDetailPost, FADepreciationDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
