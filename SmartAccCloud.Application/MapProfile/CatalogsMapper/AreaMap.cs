using SmartAccCloud.Application.Models.Catalogs.GroupArea;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class AreaMap : Profile
{
    public AreaMap()
    {
        CreateMap<CatalogArea, AreaDto>().ReverseMap();
    }
}