using SmartAccCloud.Application.Models.Catalogs.Contract;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class ContractMap :Profile
{
    public ContractMap()
    {
        CreateMap<CatalogContract, ContractDto>().ReverseMap();
        CreateMap<CatalogContract, ContractVm>().ReverseMap();
    }
}
