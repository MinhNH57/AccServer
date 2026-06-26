using SmartAccCloud.Application.Models.Catalogs.QrCodeProduct;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class QrCodeProductMap : Profile
{
    public QrCodeProductMap()
    {
        CreateMap<CatalogQrCodeProduct, QrCodeProductDto>().ReverseMap();
    }
}