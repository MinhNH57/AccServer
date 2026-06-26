using SmartAccCloud.Application.Models.Catalogs.Products.Product;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ProductMap : Profile
{
    public ProductMap()
    {
        CreateMap<CatalogProduct, ProductDto>().ReverseMap();
        CreateMap<CatalogProduct, ProductVm>().ReverseMap();
    }
}