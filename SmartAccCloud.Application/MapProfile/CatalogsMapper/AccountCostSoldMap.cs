using SmartAccCloud.Application.Models.Catalogs.AccountCostSold;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class AccountCostSoldMap : Profile
{
    public AccountCostSoldMap()
    {
        CreateMap<CatalogAccountCostSold, AccountCostSoldDto>().ReverseMap();
        CreateMap<CatalogAccountCostSold, AccountCostSoldView>().ReverseMap();
    }
}