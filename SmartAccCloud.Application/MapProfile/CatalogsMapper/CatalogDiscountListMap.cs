using SmartAccCloud.Application.Models.Catalogs.CatalogDiscountList;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogDiscountListMap : Profile
{
    public CatalogDiscountListMap()
    {
        CreateMap<CatalogDiscountList, CatalogDiscountListDto>().ReverseMap();
    }
}
