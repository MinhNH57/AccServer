using SmartAccCloud.Application.Models.Catalogs.Room;
using SmartAccCloud.Share.Dtos.Catalogs.Room;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class RoomMap : Profile
{
    public RoomMap()
    {
        CreateMap<CatalogRoom, RoomDto>().ReverseMap();
        CreateMap<CatalogRoom, CatalogRoomCbbDto>().ReverseMap();

    }
}