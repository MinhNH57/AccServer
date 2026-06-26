using SmartAccCloud.Application.Models.Catalogs.CatalogLocationWarehouse;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogLocationWarehouseMap :Profile
{
    public CatalogLocationWarehouseMap()
    {
        CreateMap<CatalogLocationWarehouse, CatalogLocationWarehouseDto>().ReverseMap();
    }
}
