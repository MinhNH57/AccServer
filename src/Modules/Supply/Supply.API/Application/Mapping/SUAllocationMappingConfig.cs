namespace Supply.API.Application.Mapping;

public class SUAllocationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAllocation, SUAllocationDto>()
            .Map(dest => dest.RefId, src => src.Id);

        config.NewConfig<SUAllocation, SUAllocationSaveFullResponse>()
            .Map(dest => dest.RefId, src => src.Id);
    }
}
