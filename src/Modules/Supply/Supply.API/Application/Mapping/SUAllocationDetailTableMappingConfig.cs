namespace Supply.API.Application.Mapping;

public class SUAllocationDetailTableMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAllocationDetailTable, SUAllocationDetailTableDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<SUAllocationDetailTable, SUAllocationDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
