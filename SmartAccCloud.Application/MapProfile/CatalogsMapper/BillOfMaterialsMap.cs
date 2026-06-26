using SmartAccCloud.Application.Models.Catalogs.BillOfMaterials;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class BillOfMaterialsMap : Profile
{
    public BillOfMaterialsMap()
    {
        CreateMap<BillOfMaterials, BillOfMaterialsDto>().ReverseMap();
    }
}