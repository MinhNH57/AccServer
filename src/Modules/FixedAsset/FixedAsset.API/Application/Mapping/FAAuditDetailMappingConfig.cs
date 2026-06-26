namespace FixedAsset.API.Application.Mapping;

public class FAAuditDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FAAuditDetail, FAAuditDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FAAuditDetail, FAAuditDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
