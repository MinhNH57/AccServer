namespace FixedAsset.API.Application.Mapping;

public class FATransferDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FATransferDetail, FATransferDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FATransferDetail, FATransferDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
