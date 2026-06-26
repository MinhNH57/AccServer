namespace FixedAsset.API.Application.Mapping;

public class FATransferMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FATransfer, FATransferDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<FATransfer, FATransferSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
