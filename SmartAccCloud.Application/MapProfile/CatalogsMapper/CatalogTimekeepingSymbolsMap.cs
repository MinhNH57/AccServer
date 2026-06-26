using SmartAccCloud.Application.Models.Catalogs.CatalogTimekeepingSymbols;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogTimekeepingSymbolsMap:Profile
{
    public CatalogTimekeepingSymbolsMap()
    {
        CreateMap<CatalogTimekeepingSymbols, CatalogTimekeepingSymbolsDto>().ReverseMap();
    }
}
