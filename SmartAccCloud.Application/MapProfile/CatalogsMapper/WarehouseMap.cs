using SmartAccCloud.Application.Models.Catalogs.Warehouse;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class WarehouseMap : Profile
{
    public WarehouseMap()
    {
        CreateMap<CategoryWarehose, WarehouseDto>().ReverseMap();
        CreateMap<CategoryWarehose, WarehouseVm>().ReverseMap();
    }
}