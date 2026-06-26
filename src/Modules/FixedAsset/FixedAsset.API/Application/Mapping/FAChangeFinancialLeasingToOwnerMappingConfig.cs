namespace FixedAsset.API.Application.Mapping;

public class FAChangeFinancialLeasingToOwnerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FAChangeFinancialLeasingToOwner, FAChangeFinancialLeasingToOwnerDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<FAChangeFinancialLeasingToOwner, FAChangeFinancialLeasingToOwnerSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
