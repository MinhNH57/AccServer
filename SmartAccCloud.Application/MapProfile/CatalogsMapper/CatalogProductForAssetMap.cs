using SmartAccCloud.Application.Models.Catalogs.CatalogProductForAsset;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogProductForAssetMap :Profile
{
    public CatalogProductForAssetMap()
    {
        CreateMap<CatalogProductForAsset, CatalogProductForAssetDto>().ReverseMap();
    }
}
