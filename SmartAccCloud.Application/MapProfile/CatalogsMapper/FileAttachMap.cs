using SmartAccCloud.Application.Models.Catalogs.FileWork;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper;

public class FileAttachMap : Profile
{
    public FileAttachMap()
    {
        CreateMap<FileAttach, FileAttachDto>().ReverseMap();
    }
}