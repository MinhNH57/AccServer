namespace SmartAccCloud.Application.Models.Catalogs.FileWork;

public class FileAttchInfoVm
{
    public List<FileAttachDto> FilePdfs { get; set; } = new();
    public List<FileAttachDto> FileImgs { get; set; } = new();
}