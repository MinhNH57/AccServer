using SmartAccCloud.Application.Models.Catalogs.CatalogPersonalIncomeTaxRates;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class CatalogPersonalIncomeTaxRatesMap:Profile
{
    public CatalogPersonalIncomeTaxRatesMap()
    {
        CreateMap<CatalogPersonalIncomeTaxRates, CatalogPersonalIncomeTaxRatesDto>().ReverseMap();
    }
}
