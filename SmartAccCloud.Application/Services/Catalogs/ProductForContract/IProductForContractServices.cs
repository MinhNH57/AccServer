using SmartAccCloud.Application.Models.Catalogs.CatalogProductForContract;

namespace SmartAccCloud.Application.Services.Catalogs.ProductForContract;

public interface IProductForContractServices
{
    Task<bool> CreateProductForContract(List<ProductForContractDto> param);
    Task<bool> EditProductForContract(List<ProductForContractDto> param);
    Task<bool> DeleteProductForContract(List<ProductForContractDto> param);
}