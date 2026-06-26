using SmartAccCloud.Application.Models.Catalogs.Branch;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;
public class BranchMap :Profile
{
    public BranchMap()
    {
        CreateMap<CatalogBranch, BranchDto>().ReverseMap();
    }
}
