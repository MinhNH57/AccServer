using SmartAccCloud.Application.Models.Catalogs.ContractGroup;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class ContractGroupMap : Profile
{
    public ContractGroupMap()
    {
        CreateMap<CatalogGroupContract, ContractGroupDto>().ReverseMap();
    }
}

