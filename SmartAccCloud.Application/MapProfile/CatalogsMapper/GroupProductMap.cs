using SmartAccCloud.Application.Models.Catalogs.Products.GroupProduct;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class GroupProductMap : Profile
{
    public GroupProductMap()
    {
        CreateMap<CatalogGroupProduct, GroupProductDto>().ReverseMap();
    }
}