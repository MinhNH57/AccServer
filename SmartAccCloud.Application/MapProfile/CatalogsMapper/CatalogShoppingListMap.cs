using SmartAccCloud.Application.Models.Catalogs.CatalogShoppingList;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogShoppingListMap : Profile
{
    public CatalogShoppingListMap()
    {
        CreateMap<CatalogShoppingList, CatalogShoppingListDto>().ReverseMap();
    }
}
