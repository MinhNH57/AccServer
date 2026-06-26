using SmartAccCloud.Application.Models.Catalogs.BankAccountForObj;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class BankAccountForObjectMap : Profile
{
    public BankAccountForObjectMap()
    {
        CreateMap<CatalogBankAccountForObject, BankAccountForObjectDto>().ReverseMap();
    }
}
