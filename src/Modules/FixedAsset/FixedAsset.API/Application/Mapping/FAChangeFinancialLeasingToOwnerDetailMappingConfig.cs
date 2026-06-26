namespace FixedAsset.API.Application.Mapping;

public class FAChangeFinancialLeasingToOwnerDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FAChangeFinancialLeasingToOwnerDetail, FAChangeFinancialLeasingToOwnerDetailDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FAChangeFinancialLeasingToOwnerDetail, FAChangeFinancialLeasingToOwnerDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
