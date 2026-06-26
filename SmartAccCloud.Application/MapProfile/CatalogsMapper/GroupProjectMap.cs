using SmartAccCloud.Application.Models.Catalogs.GroupProject;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class GroupProjectMap : Profile
{
    public GroupProjectMap()
    {
        CreateMap<CatalogGroupProject, GroupProjectDto>().ReverseMap();
    }
}