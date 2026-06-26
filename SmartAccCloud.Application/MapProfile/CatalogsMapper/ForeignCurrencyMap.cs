using SmartAccCloud.Application.Models.Catalogs.ForeignCurrency;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ForeignCurrencyMap : Profile
{
    public ForeignCurrencyMap()
    {
        CreateMap<CatalogForeignCurrency, ForeignCurrencyDto>().ReverseMap();
    }
}