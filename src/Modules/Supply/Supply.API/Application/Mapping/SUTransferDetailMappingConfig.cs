namespace Supply.API.Application.Mapping;

public class SUTransferDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUTransferDetail, SUTransferDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<SUTransferDetail, SUTransferDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
