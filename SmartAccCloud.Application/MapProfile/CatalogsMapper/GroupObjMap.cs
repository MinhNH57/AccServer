using SmartAccCloud.Application.Models.Catalogs.GroupObj;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class GroupObjMap : Profile
{
    public GroupObjMap()
    {
        CreateMap<CatalogGroupObj, GroupObjDto>().ReverseMap();
    }
}