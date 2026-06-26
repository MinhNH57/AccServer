using SmartAccCloud.Application.Models.Catalogs.GroupArea;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class GroupAreaMap : Profile
{
    public GroupAreaMap()
    {
        CreateMap<CatalogGroupArea, GroupAreaDto>().ReverseMap();
    }
}