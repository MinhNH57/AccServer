namespace FixedAsset.API.Application.Mapping;

public class FAAuditMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FAAudit, FAAuditDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<FAAudit, FAAuditSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
