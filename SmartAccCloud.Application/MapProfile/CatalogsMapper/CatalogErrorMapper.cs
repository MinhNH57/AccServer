using SmartAccCloud.Application.Models.Catalogs.CatalogError;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogErrorMapper: Profile
{
    public CatalogErrorMapper()
    {
        CreateMap<CatalogError, CatalogErrorDto>().ReverseMap();
    }
}
