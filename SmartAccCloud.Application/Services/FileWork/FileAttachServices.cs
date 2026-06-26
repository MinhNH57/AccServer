using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.FileAttachs;
using SmartAccCloud.Application.Models.QueryModels;
using SmartAccCloud.Application.Services.Catalogs.CatalogObject;

namespace SmartAccCloud.Application.Services.FileWork;

public class FileAttachServices(IApplicationDbContext context,
    IMultiTenantContextAccessor contextAccessor, ICurrentUser currentUser,
    ILogger<CatalogObjectServices> logger)
    : IFileAttachServices
{
    public async Task<Response.Result<string>> UploadFile(FileAttachModel fileAttachCreate)
    {
        int countFileSuccess = 0;
        foreach (var file in fileAttachCreate.Files)
        {
            long sizeFile = file.Length;
            if (sizeFile > (20 * 1024 * 1024))
            {
                return Response.Result<string>.Failure(new Error("400", "Maximum size can be 20mb"));
            }

            string fileName = file.FileName;
            var today = DateTime.Today;
            string subForderName = today.ToString("ddMMyy");
            string tenantIdForderNamer = contextAccessor.MultiTenantContext.TenantInfo!.Identifier!;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", tenantIdForderNamer, subForderName);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);

            var fileUrl = $"Uploads/{tenantIdForderNamer}/{subForderName}/{fileName}";
            var fileAttach = new FileAttach()
            {
                IdData = fileAttachCreate.IdContent,
                CodeUnit = fileAttachCreate.CodeUnit,
                FileNames = fileName,
                DataType = fileAttachCreate.DataType,
                KeyTable = fileAttachCreate.KeyTable,
                FilePath = fileUrl,
                TableName = fileAttachCreate.ColumnTable
            };
            context.FileAttach.Add(fileAttach);
            await context.SaveChangesAsync().ConfigureAwait(false);

            await using FileStream stream = new(Path.Combine(path, fileName), FileMode.OpenOrCreate);
            await file.CopyToAsync(stream).ConfigureAwait(false);
            countFileSuccess++;
        }

        return Response.Result<string>.Success(countFileSuccess.ToString());
    }

    public Task<Response.Result<bool>> DeleteAllFile(Guid idFile)
    {
        throw new NotImplementedException();
    }

    public async Task<Response.Result<bool>> DeleteAllFile(QueryFile query)
    {
        var lst = await context.FileAttach.AsNoTracking()
            .Where(c => c.KeyTable == query.KeyTable && c.TableName == query.ColumnTable)
            .ToListAsync()
            .ConfigureAwait(false);

        foreach (var item in lst)
        {
            var getFile = await context.FileAttach.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.Id)
                .ConfigureAwait(false);

            if (getFile == null)
            {
                continue;
            }

            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath =
                Path.Combine(currentDirectory, getFile.FilePath.Replace('/', Path.DirectorySeparatorChar));

            if (!File.Exists(filePath))
            {
                context.FileAttach.Remove(getFile);
                continue;
            }

            try
            {
                File.Delete(filePath);
                context.FileAttach.Remove(getFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa file: {filePath}. Lỗi: {ex.Message}");
                continue;
            }
        }

        await context.SaveChangesAsync().ConfigureAwait(false);
        return Response.Result<bool>.Success(true);
    }


    public async Task<Response.Result<List<FileAttach>>> GetFilePath(QueryFile query)
    {
        var lst = await context.FileAttach.Where(c => c.KeyTable == query.KeyTable && c.TableName == query.ColumnTable)
            .ToListAsync()
            .ConfigureAwait(false);

        return Response.Result<List<FileAttach>>.Success(lst);
    }



    public async Task<Result<string>> UploadFileFund(FileAttachModel fileAttachCreate)
    {
        int countFileSuccess = 0;
        foreach (var file in fileAttachCreate.Files)
        {
            long sizeFile = file.Length;
            if (sizeFile > (10 * 1024 * 1024))
            {
                return Result<string>.Failure(new Error("400", "Dung lượng tối đa có thể upload là 10mb."));
            }

            string fileName = file.FileName;
            var today = DateTime.Today;
            string subForderName = today.ToString("ddMMyy");
            string tenantIdForderNamer = contextAccessor.MultiTenantContext.TenantInfo!.Identifier!;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", tenantIdForderNamer, subForderName);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);

            var fileUrl = $"Uploads/{tenantIdForderNamer}/{subForderName}/{fileName}";
            var fileAttach = new SmartFileAttach()
            {
                NumberOfVouchers = fileAttachCreate.IdContent.ToString(),
                CodeUnit = currentUser.CodeUnit,
                FileNames = fileName,
                PathFile = fileUrl,
                Description = fileAttachCreate.ColumnTable,
                SizeFile = file.Length.ToString(),
                CreatedBy = currentUser.CodeUser,
                TypeFile = file.ContentType
            };
            context.SmartFileAttach.Add(fileAttach);

            await using FileStream stream = new(Path.Combine(path, fileName), FileMode.OpenOrCreate);
            await file.CopyToAsync(stream).ConfigureAwait(false);
            countFileSuccess++;
        }
        var count = await context.SaveChangesAsync().ConfigureAwait(false);
        if (count > 0)
        {
            logger.LogInformation("User: '{userName}' uploaded file with table: 'CatalogObject'.", currentUser.CodeUser);
            return Result<string>.Success(countFileSuccess.ToString());
        }
        return Result<string>.Failure(new Error("400", "Có lỗi trong quá trình đẩy file"));
    }

    public async Task<string> DownloadFileFund(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteFileFund(List<string> idFile)
    {
        var filesToDelete = await context.SmartFileAttach
            .AsNoTracking()
            .Where(x => idFile.Select(Guid.Parse).Contains(x.IdContents))
            .ToListAsync()
            .ConfigureAwait(false);


        if (filesToDelete == null || !filesToDelete.Any())
        {
            return Response.Result<bool>.Failure(new Error("400", "Không tìm thấy file để xóa trong cơ sở dữ liệu"));
        }

        foreach (var getFile in filesToDelete)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, getFile.PathFile.Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    return Response.Result<bool>.Failure(new Error("500", $"Lỗi khi xóa file: {ex.Message}"));
                }
            }

            // Xóa bản ghi trong database
            context.SmartFileAttach.Remove(getFile);
        }

        await context.SaveChangesAsync().ConfigureAwait(false);
        return Response.Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeleteAllFileFund(QueryFile query)
    {
        if (query.ColumnTable != "CatalogObject") return Result<bool>.Failure(new Error("400", "Bản ghi file không tồn tại trong cơ sở dữ liệu"));
        var getFiles = context.SmartFileAttach.AsNoTracking()
            .Where(x => x.NumberOfVouchers == (query.KeyTable)).ToList();

        if (!getFiles.Any())
            return Result<bool>.Failure(new Error("400", "Không tìm thấy file nào trong cơ sở dữ liệu"));

        string currentDirectory = Directory.GetCurrentDirectory();

        foreach (var file in getFiles)
        {
            string filePath = Path.Combine(currentDirectory, file.PathFile.Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    return Result<bool>.Failure(new Error("500", $"Lỗi khi xóa file {file.PathFile}: {ex.Message}"));
                }
            }
        }

        // Xóa tất cả bản ghi file trong database
        context.SmartFileAttach.RemoveRange(getFiles);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return Result<bool>.Success(true);
    }

    public async Task<Response.Result<bool>> DeleteFile(Guid idGuid)
    {
        var getFile = await context.FileAttach.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == idGuid)
            .ConfigureAwait(false);

        if (getFile == null)
        {
            return Response.Result<bool>.Failure(new Error("400", "Bản ghi file không tồn tại trong cơ sở dữ liệu"));
        }

        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, getFile.FilePath.Replace('/', Path.DirectorySeparatorChar));

        if (!File.Exists(filePath))
        {
            // Nếu file không tồn tại, loại bỏ bản ghi khỏi cơ sở dữ liệu
            context.FileAttach.Remove(getFile);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return Response.Result<bool>.Failure(new Error("400", "File đã bị xóa trước đó"));
        }

        try
        {
            // Chỉ xóa file, không xóa thư mục
            File.Delete(filePath);
            context.FileAttach.Remove(getFile);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return Response.Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ nếu có lỗi xảy ra trong quá trình xóa file
            return Response.Result<bool>.Failure(new Error("500", $"Lỗi khi xóa file: {ex.Message}"));
        }
    }
}