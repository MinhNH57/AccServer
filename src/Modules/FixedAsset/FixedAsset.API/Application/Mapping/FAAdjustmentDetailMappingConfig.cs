namespace FixedAsset.API.Application.Mapping;

public class FAAdjustmentDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FAAdjustmentDetail, FAAdjustmentDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FAAdjustmentDetail, FAAdjustmentDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
