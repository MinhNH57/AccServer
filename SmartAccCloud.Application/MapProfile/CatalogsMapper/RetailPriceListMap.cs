using SmartAccCloud.Application.Models.Catalogs.RetailPriceList;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class RetailPriceListMap : Profile
{
    public RetailPriceListMap()
    {
        CreateMap<RetailPriceList, RetailPriceListDto>().ReverseMap();
    }
}