using SmartAccCloud.Application.Models.Catalogs.CatalogAsset;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class CatalogAssetMap : Profile
{
    public CatalogAssetMap()
    {
        CreateMap<CatalogAsset, CatalogAssetDto>().ReverseMap();
        CreateMap<CatalogAsset, CatalogAssetVm>().ReverseMap();
    }
}