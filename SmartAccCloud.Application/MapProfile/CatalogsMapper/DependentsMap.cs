using SmartAccCloud.Application.Models.Catalogs.CatalogDependents;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class DependentsMap :Profile
{
    public DependentsMap()
    {
        CreateMap<CatalogDependents, DependentsDto>().ReverseMap();
    }
}
