namespace Supply.API.Application.Mapping;

public class SUAdjustmentDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAdjustmentDetail, SUAdjustmentDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<SUAdjustmentDetail, SUAdjustmentDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
