using SmartAccCloud.Application.Models.Catalogs.BankAccountForObj;

namespace SmartAccCloud.Application.Services.Catalogs.BankAccountForObject;
public interface IBankAccountForObjectServices
{
    Task<bool> CreateBankAccountForObject(List<BankAccountForObjectDto> param);
    Task<bool> EditBankAccountForObject(List<BankAccountForObjectDto> param);
    Task<bool> DeleteBankAccountForObject(List<BankAccountForObjectDto> param);
}
