namespace Supply.API.Application.Mapping;

public class SUAdjustmentDetailVoucherMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SUAdjustmentDetailVoucher, SUAdjustmentDetailVoucherDto>()
            .Map(dest => dest.RefDetailId, src => src.Id);

        config.NewConfig<SUAdjustmentDetailVoucher, SUAdjustmentDetailSaveFullResponse>()
            .Map(dest => dest.RefDetailId, src => src.Id);
    }
}
