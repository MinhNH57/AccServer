using SmartAccCloud.Application.Models.Catalogs.CatalogAccountingShortcuts;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class CatalogAccountingShortcutsMap : Profile
{
    public CatalogAccountingShortcutsMap()
    {
        CreateMap<CatalogAccountingShortcuts, CatalogAccountingShortcutsDto>().ReverseMap();
    }
}