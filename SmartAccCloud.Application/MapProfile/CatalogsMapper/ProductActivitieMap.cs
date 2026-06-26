using SmartAccCloud.Application.Models.Catalogs.ProductionActivitie;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ProductActivitieMap : Profile
{
    public ProductActivitieMap()
    {
        CreateMap<CatalogProductionActivitie, ProductionActivitieDto>().ReverseMap();
    }
}