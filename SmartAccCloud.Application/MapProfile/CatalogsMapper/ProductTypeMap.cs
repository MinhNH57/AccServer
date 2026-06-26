using SmartAccCloud.Application.Models.Catalogs.Products.CatalogProductType;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ProductTypeMap : Profile
{
    public ProductTypeMap()
    {
        CreateMap<CatalogProductType, ProductTypeDto>().ReverseMap();
    }
}