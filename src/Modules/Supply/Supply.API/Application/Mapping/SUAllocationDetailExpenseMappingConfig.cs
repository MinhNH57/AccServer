namespace Supply.API.Application.Mapping;

public class SUAllocationDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAllocationDetailExpense, SUAllocationDetailExpenseDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<SUAllocationDetailExpense, SUAllocationDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
