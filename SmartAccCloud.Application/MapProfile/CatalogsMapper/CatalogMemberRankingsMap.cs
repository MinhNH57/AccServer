using SmartAccCloud.Application.Models.Catalogs.CatalogMemberRankings;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogMemberRankingsMap:Profile
{
    public CatalogMemberRankingsMap()
    {
        CreateMap<CatalogMemberRankings, CatalogMemberRankingsDto>().ReverseMap();
    }
}
