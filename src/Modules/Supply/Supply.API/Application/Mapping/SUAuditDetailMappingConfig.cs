namespace Supply.API.Application.Mapping;

public class SUAuditDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAuditDetail, SUAuditDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<SUAuditDetail, SUAuditDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
