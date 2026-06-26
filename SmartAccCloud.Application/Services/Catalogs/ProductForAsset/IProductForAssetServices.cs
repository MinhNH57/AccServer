using SmartAccCloud.Application.Models.Catalogs.CatalogProductForAsset;

namespace SmartAccCloud.Application.Services.Catalogs.ProductForAsset;
public interface IProductForAssetServices
{
    Task<bool> CreateProductForAsset(List<CatalogProductForAssetDto> param);
    Task<bool> EditProductForAsset(List<CatalogProductForAssetDto> param);
    Task<bool> DeleteProductForAsset(List<CatalogProductForAssetDto> param);
}
