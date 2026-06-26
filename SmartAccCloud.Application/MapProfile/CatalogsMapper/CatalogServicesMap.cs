using SmartAccCloud.Application.Models.Catalogs.CatalogServices;


namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogServicesMap : Profile
{
    public CatalogServicesMap()
    {
        CreateMap<CatalogServices, CatalogServicesDto>().ReverseMap();
        CreateMap<CatalogServices, CbbCatalogServicesDto>().ReverseMap();
    }
}
