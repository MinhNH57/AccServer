using SmartAccCloud.Application.Models.Catalogs.Object;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class ObjectMap : Profile
{
    public ObjectMap()
    {
        CreateMap<CatalogObject, ObjectDto>().ReverseMap();
        CreateMap<CatalogObject, ObjectVm>().ReverseMap();
        CreateMap<CatalogObject, ObjectDtoFund>().ReverseMap();
        CreateMap<CatalogObject, ObjectViewMobile>().ReverseMap();
        CreateMap<Pagination.PagedResult<CatalogObject>, Pagination.PagedResult<ObjectDto>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<Pagination.PagedResult<CatalogObject>, Pagination.PagedResult<ObjectVm>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<CatalogObject, ObjectForCbb>();
        CreateMap<CatalogObject, ObjectSalaryVm>();
        CreateMap<CatalogObject, ObjectStaffVm>();
    }
}