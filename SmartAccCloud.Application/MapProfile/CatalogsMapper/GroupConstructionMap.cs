using SmartAccCloud.Application.Models.Catalogs.GroupConstruction;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class GroupConstructionMap : Profile
{
    public GroupConstructionMap()
    {
        CreateMap<CatalogGroupConstruction, GroupConstructionDto>().ReverseMap();
    }
}