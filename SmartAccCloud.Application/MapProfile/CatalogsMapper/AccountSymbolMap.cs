using SmartAccCloud.Application.Models.Catalogs.AccountSymbol;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper
{
    public class AccountSymbolMap : Profile
    {
        public AccountSymbolMap()
        {
            CreateMap<CatalogAccountSymbol, AccountSymbolDto>().ReverseMap();
            CreateMap<CatalogAccountSymbol, AccountSymbolVm>().ReverseMap();
        }
    }
}