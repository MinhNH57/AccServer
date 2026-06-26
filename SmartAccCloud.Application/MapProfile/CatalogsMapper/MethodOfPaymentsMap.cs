using SmartAccCloud.Application.Models.Catalogs.MethodOfPayments;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class MethodOfPaymentsMap : Profile
{
    public MethodOfPaymentsMap()
    {
        CreateMap<CatalogMethodOfPayments, MethodOfPaymentsDto>().ReverseMap();
    }
}