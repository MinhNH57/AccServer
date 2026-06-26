namespace Supply.API.Application.Mapping;

public class SUIncrementMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUIncrement, SUIncrementDto>()
            .Map(dest => dest.SupplyId, src => src.Id);

        config.NewConfig<SUIncrement, SUIncrementSaveFullResponse>()
            .Map(dest => dest.SupplyId, src => src.Id);

        config.NewConfig<SUIncrement, SUDecrementAvailableSupply>()
            .Map(dest => dest.SupplyId, src => src.Id);

        config.NewConfig<SUIncrement, SupplyTransfer>()
            .Map(dest => dest.SupplyId, src => src.Id);

        config.NewConfig<SUIncrement, SUAdjustmentSupplyAvailable>()
            .Map(dest => dest.SupplyId, src => src.Id);
    }
}
