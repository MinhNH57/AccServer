using SmartAccCloud.Application.Models.Catalogs.Project;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ProjectMap : Profile
{
    public ProjectMap()
    {
        CreateMap<CatalogProject, ProjectDto>().ReverseMap();
        CreateMap<CatalogProject, ProjectVm>().ReverseMap();
    }
}