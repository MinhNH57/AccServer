using SmartAccCloud.Application.Models.Catalogs.AccountType;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class AccountTypeMap : Profile
{
    public AccountTypeMap()
    {
        CreateMap<CatalogAccountType, AccountTypeDto>().ReverseMap();
    }
}