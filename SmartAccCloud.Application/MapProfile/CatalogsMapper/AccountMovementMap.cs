using SmartAccCloud.Application.Models.Catalogs.AccountMovement;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class AccountMovementMap : Profile
{
    public AccountMovementMap()
    {
        CreateMap<CatalogAccountMovement, AccountMovementDto>().ReverseMap();
    }
}