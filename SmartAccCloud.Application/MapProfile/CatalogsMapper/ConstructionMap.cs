using SmartAccCloud.Application.Models.Catalogs.Construction;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class ConstructionMap :Profile
{
    public ConstructionMap()
    {
        CreateMap<CatalogConstruction, ConstructionDto>().ReverseMap();
    }
}
