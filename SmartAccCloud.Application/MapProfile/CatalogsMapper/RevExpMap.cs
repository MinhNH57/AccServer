using SmartAccCloud.Application.Models.Catalogs.RevExp;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class RevExpMap : Profile
{
    public RevExpMap()
    {
        CreateMap<CatalogRevExp, RevExpDto>().ReverseMap();
    }
}