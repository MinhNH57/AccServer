using SmartAccCloud.Application.Models.SmartData;

namespace SmartAccCloud.Application.MapProfile.SmartData;

public class SmartDataProfile : Profile
{
    public SmartDataProfile()
    {
        CreateMap<Domain.Entity.SmartData, SmartDataVm>();

        CreateMap<SmartContentsData, SmartContentDataVm>();

        CreateMap<SmartVatTaxList, SmartVatTaxVm>();
    }
}