namespace FixedAsset.API.Application.Mapping;

public class FAAdjustmentDetailPostMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FAAdjustmentDetailPost, FAAdjustmentDetailPostDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FAAdjustmentDetailPost, FAAdjustmentDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
