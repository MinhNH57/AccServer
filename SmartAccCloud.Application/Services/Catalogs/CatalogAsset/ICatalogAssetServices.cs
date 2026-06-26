using SmartAccCloud.Application.Models.Catalogs.CatalogAsset;

namespace SmartAccCloud.Application.Services.Catalogs.CatalogAsset;
public interface ICatalogAssetServices
{
    Task<Result<bool>> CreateAsset(CatalogAssetDto param);
    Task<Result<bool>> EditAsset(CatalogAssetDto param);
}
