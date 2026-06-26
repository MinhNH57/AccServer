using SmartAccCloud.Application.Models.Catalogs.FundingSource;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class FundingSourceMap : Profile
{
    public FundingSourceMap()
    {
        CreateMap<CatalogFundingSource, FundingSourceDto>().ReverseMap();
    }
}