using SmartAccCloud.Application.Models.Catalogs.ProductTank;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ProductTankMap : Profile
{
    public ProductTankMap()
    {
        CreateMap<CatalogProductTank, ProductTankDto>().ReverseMap();
    }
}