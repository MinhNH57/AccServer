using SmartAccCloud.Application.Models.Catalogs.CatalogAssetGroup;
using SmartAccCloud.Share.Dtos.Catalogs.CatalogAssetGroup;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class CatalogAssetGroupMap : Profile
{
    public CatalogAssetGroupMap()
    {
        CreateMap<CatalogAssetGroup, CatalogAssetGroupDto>().ReverseMap();
        CreateMap<CatalogAssetGroup, CatalogAssetGroupCbbDto>().ReverseMap();
    }
}