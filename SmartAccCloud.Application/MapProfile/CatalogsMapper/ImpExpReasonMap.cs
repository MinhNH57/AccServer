using SmartAccCloud.Application.Models.Catalogs.ImpExpReason;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ImpExpReasonMap : Profile
{
    public ImpExpReasonMap()
    {
        CreateMap<CatalogImpExpReason, ImpExpReasonDto>().ReverseMap();
        CreateMap<CatalogImpExpReason, ImpExpReasonCbb>();
    }
}