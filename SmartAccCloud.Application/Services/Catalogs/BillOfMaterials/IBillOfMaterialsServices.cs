using SmartAccCloud.Application.Models.Catalogs.BillOfMaterials;

namespace SmartAccCloud.Application.Services.Catalogs.BillOfMaterials;
public interface IBillOfMaterialsServices
{
    Task<bool> CreateBillOfMaterials(List<BillOfMaterialsDto> param);
    Task<bool> EditBillOfMaterials(List<BillOfMaterialsDto> param);
    Task<bool> DeleteBillOfMaterials(List<BillOfMaterialsDto> param);
}
