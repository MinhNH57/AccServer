namespace FixedAsset.API.Application.Mapping;

public class FAAdjustmentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FAAdjustment, FAAdjustmentDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<FAAdjustment, FAAdjustmentSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
