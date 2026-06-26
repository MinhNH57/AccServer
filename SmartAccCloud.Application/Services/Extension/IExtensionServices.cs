using Microsoft.AspNetCore.Http;
using SmartAccCloud.Application.Models.ExcelModel;
using SmartAccCloud.Application.Models.QueryModels;
using SmartAccCloud.Application.StoreViewModels;
using SmartAccCloud.Domain.Entity.Catalogs;

namespace SmartAccCloud.Application.Services.Extension;

public interface IExtensionServices
{
    Task<Result<SmartCode>> GetCodeCatalogByGroupCode(QueryCodeCatalogByGroup query);
    Task<Result<VietQrTaxCode.ReponseVietQr>> GetTaxCodeInfo(string taxCode);
    Task<bool> GroupCodeCategory(GroupCodeQuery query);
    Task<SmartCode> GetNoCoupon(GetNoCouponRequest request, CancellationToken token = default);
    Task<Result<List<SheetInfo>>> UploadFileExcel(IFormFile file);
    Task<Result<List<ObjectExcelDto>>> UploadFileExcelObj(IFormFile file);
}