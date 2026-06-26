using SmartAccCloud.Application.Models.Catalogs.CatalogVoucherNumber;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class CatalogVoucherNbProfile : Profile
{
    public CatalogVoucherNbProfile()
    {
        CreateMap<CatalogVoucherNumber, CatalogVoucherNbVm>().ReverseMap();
        CreateMap<CatalogVoucherNumber, CatalogVoucherNbForReasonVm>().ReverseMap();
    }
}