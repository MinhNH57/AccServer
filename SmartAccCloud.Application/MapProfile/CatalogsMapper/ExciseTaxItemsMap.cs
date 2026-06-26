using SmartAccCloud.Application.Models.Catalogs.CatalogExciseTaxItems;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ExciseTaxItemsMap : Profile
{
    public ExciseTaxItemsMap()
    {
        CreateMap<CatalogExciseTaxItems, ExciseTaxItemsDto>().ReverseMap();
        CreateMap<CatalogExciseTaxItems, CbbExciseTaxItemsDto>().ReverseMap();
    }
}