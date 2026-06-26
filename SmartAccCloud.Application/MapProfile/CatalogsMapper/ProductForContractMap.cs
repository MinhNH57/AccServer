using SmartAccCloud.Application.Models.Catalogs.CatalogProductForContract;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ProductForContractMap : Profile
{
    public ProductForContractMap()
    {
        CreateMap<CatalogProductForContract, ProductForContractDto>().ReverseMap();
    }
}