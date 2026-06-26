namespace Supply.API.Application.Mapping;

public class SUIncrementDetailDepartmentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUIncrementDetailDepartment, SUIncrementDetailDepartmentDto>()
            .Map(dest => dest.SupplyDetailId, src => src.Id);

        config.NewConfig<SUIncrementDetailDepartment, SUIncrementDetailSaveFullResponse>()
            .Map(dest => dest.SupplyDetailId, src => src.Id);
    }
}
