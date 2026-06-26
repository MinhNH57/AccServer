using SmartAccCloud.Application.Models.Catalogs.QrCodeProduct;

namespace SmartAccCloud.Application.Services.Catalogs.QrCodeForProduct;
public interface IQrCodeForProductServices
{
    Task<bool> CreateQrCodeProduct(List<QrCodeProductDto> param);
    Task<bool> EditQrCodeProduct(List<QrCodeProductDto> param);
    Task<bool> DeleteQrCodeProduct(List<QrCodeProductDto> param);
}
