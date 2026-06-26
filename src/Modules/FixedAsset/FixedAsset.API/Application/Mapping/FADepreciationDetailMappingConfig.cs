namespace FixedAsset.API.Application.Mapping;

public class FADepreciationDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FADepreciationDetail, FADepreciationDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FADepreciationDetail, FADepreciationDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
