namespace FixedAsset.API.Application.Mapping;

public class FADepreciationDetailAllocationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FADepreciationDetailAllocation, FADepreciationDetailAllocationDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<FADepreciationDetailAllocation, FADepreciationDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
