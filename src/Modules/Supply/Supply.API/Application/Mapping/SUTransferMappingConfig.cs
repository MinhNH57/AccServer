namespace Supply.API.Application.Mapping;

public class SUTransferMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUTransfer, SUTransferDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<SUTransfer, SUTransferSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
