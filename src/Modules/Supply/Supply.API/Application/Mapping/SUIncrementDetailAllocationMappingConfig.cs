namespace Supply.API.Application.Mapping;

public class SUIncrementDetailAllocationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUIncrementDetailAllocation, SUIncrementDetailAllocationDto>()
            .Map(dest => dest.SupplyDetailId, src => src.Id);

        config.NewConfig<SUIncrementDetailAllocation, SUIncrementDetailSaveFullResponse>()
            .Map(dest => dest.SupplyDetailId, src => src.Id);
    }
}
