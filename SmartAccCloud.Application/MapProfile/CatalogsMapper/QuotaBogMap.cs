using SmartAccCloud.Application.Models.Catalogs.QuotaBog;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class QuotaBogMap : Profile
{
    public QuotaBogMap()
    {
        CreateMap<CatalogQuotaBog, QuotaBogDto>().ReverseMap();
    }
}