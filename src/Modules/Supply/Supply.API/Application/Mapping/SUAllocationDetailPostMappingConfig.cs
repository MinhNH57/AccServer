namespace Supply.API.Application.Mapping;

public class SUAllocationDetailPostMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAllocationDetailPost, SUAllocationDetailPostDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<SUAllocationDetailPost, SUAllocationDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
