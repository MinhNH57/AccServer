namespace Supply.API.Application.Mapping;

public class SUAuditMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAudit, SUAuditDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<SUAudit, SUAuditSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
