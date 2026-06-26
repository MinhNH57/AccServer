using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using FileHandle.Data;
using FileHandle.Data.Entites;
using FileHandle.Models;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace FileHandle.Services;

public class FileAttachServices(FileHandleDbContext context,
    IMultiTenantContextAccessor contextAccessor,
    IMultiTenantStore<TenantInfoCustomize> tenantStore,
    IMultiTenantContextSetter multiTenantContextSetter,
    ICurrentUser currentUser)
    : IFileAttachServices
{

    public async Task<Result<string>> UploadFileFund(FileAttachModel fileAttachCreate)
    {
        int countFileSuccess = 0;
        foreach (var file in fileAttachCreate.Files)
        {
            long sizeFile = file.Length;
            if (sizeFile > (10 * 1024 * 1024))
            {
                return Result.Failure<string>(new Error("400", "Dung lượng tối đa có thể upload là 10mb."));
            }

            string fileName = file.FileName;
            var today = DateTime.Today;
            string subForderName = today.ToString("ddMMyy");
            string tenantIdForderNamer = contextAccessor.MultiTenantContext.TenantInfo!.Identifier!;

            string path;
            string fileUrl;
            if (!string.IsNullOrEmpty(fileAttachCreate.ColumnTable))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", tenantIdForderNamer, fileAttachCreate.ColumnTable, subForderName);
                fileUrl = $"Uploads/{tenantIdForderNamer}/{fileAttachCreate.ColumnTable}/{subForderName}/{fileName}";
            }
            else
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", tenantIdForderNamer, subForderName);
                fileUrl = $"Uploads/{tenantIdForderNamer}/{subForderName}/{fileName}";
            }
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);

            var fileAttach = new SmartFileAttach()
            {
                IdContents = fileAttachCreate.IdContent,
                NumberOfVouchers = fileAttachCreate.IdContent.ToString(),
                // CodeUnit = currentUser.CodeUnit,
                FileNames = fileName,
                PathFile = fileUrl,
                Description = fileAttachCreate.Description,
                SizeFile = sizeFile.ToString(),
                //CreatedBy = currentUser.CodeUser,
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
            // logger.LogInformation("User: '{userName}' uploaded file with table: 'CatalogObject'.", currentUser.CodeUser);
            return Result.Success(countFileSuccess.ToString());
        }
        return Result.Failure<string>(new Error("400", "Có lỗi trong quá trình đẩy file"));
    }


    public async Task<Result<string>> UploadMutipleFileV2(List<MultipleFileUploadModel> request, CancellationToken token)
    {
        int countFileSuccess = 0;
        List<SmartFileAttach> lsCreate = new();
        foreach (var item in request)
        {
            long sizeFile = item.Files.Length;
            if (sizeFile > (5 * 1024 * 1024))
            {
                //return Result.Failure<string>(new Error("400", "Dung lượng tối đa có thể upload là 5mb."));
                continue;
            }

            string fileName = item.Files.FileName;
            var today = DateTime.Today;
            string tenantIdForderNamer = currentUser.TenantId!;

            string path;
            string fileUrl;
            path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", tenantIdForderNamer, item.DataType ?? "", item.NumberOfVouchers ?? "");
            fileUrl = $"Uploads/{tenantIdForderNamer}/{item.DataType}/{item.NumberOfVouchers}/{fileName}";
            //if (!string.IsNullOrEmpty(item.DataType))
            //{
            //}
            //else
            //{
            //    path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", tenantIdForderNamer, subForderName);
            //    fileUrl = $"Uploads/{tenantIdForderNamer}/{subForderName}/{fileName}";
            //}

            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);

            var fileAttach = new SmartFileAttach()
            {
                IdContents = item.IdContent,
                // CodeUnit = currentUser.CodeUnit,
                NumberOfVouchers = item.NumberOfVouchers,
                FileNames = fileName,
                PathFile = fileUrl,
                Description = item.Description,
                SizeFile = item.Files.Length.ToString(),
                CreatedBy = currentUser.NameUser,
                CodeUser = currentUser.CodeUser,
                TypeFile = item.Files.ContentType
            };
            lsCreate.Add(fileAttach);
            await using FileStream stream = new(Path.Combine(path, fileName), FileMode.OpenOrCreate);

            await item.Files.CopyToAsync(stream, token).ConfigureAwait(false);
            countFileSuccess++;
        }

        await context.SmartFileAttach.AddRangeAsync(lsCreate, token);

        var count = await context.SaveChangesAsync(token);
        if (count > 0)
        {
            return Result.Success(countFileSuccess.ToString());
        }
        return Result.Failure<string>(new Error("400", "Có lỗi trong quá trình đẩy file"));
    }


    public async Task<string> DownloadFileFund(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteFileFund(List<string> idFile)
    {
        var filesToDelete = await context.SmartFileAttach
            .AsNoTracking()
            .Where(x => idFile.Select(Guid.Parse).Contains(x.IdContents) || idFile.Select(Guid.Parse).Contains(x.Id))
            .ToListAsync()
            .ConfigureAwait(false);


        if (filesToDelete == null || !filesToDelete.Any())
        {
            return Result.Failure<bool>(new Error("400", "Không tìm thấy file để xóa trong cơ sở dữ liệu"));
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
                    return Result.Failure<bool>(new Error("500", $"Lỗi khi xóa file: {ex.Message}"));
                }
            }

            // Xóa bản ghi trong database
            context.SmartFileAttach.Remove(getFile);
        }

        await context.SaveChangesAsync().ConfigureAwait(false);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeleteAllFileFund(QueryFile query, string? tenantId = null)
    {
        if (!string.IsNullOrEmpty(tenantId))
        {
            var tenantInfo = await tenantStore.TryGetByIdentifierAsync(tenantId);
            multiTenantContextSetter.MultiTenantContext = new MultiTenantContext<TenantInfoCustomize>
            { TenantInfo = tenantInfo };
            //context.Database.SetConnectionString(tenantInfo.ConnectionString());
        }

        var getFiles = context.SmartFileAttach.AsNoTracking()
            .Where(x => x.NumberOfVouchers == (query.IdContent.ToString())).ToList();

        if (!getFiles.Any())
            return Result.Failure<bool>(new Error("400", "Không tìm thấy file nào trong cơ sở dữ liệu"));

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
                    return Result.Failure<bool>(new Error("500", $"Lỗi khi xóa file {file.PathFile}: {ex.Message}"));
                }
            }
        }

        // Xóa tất cả bản ghi file trong database
        context.SmartFileAttach.RemoveRange(getFiles);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return Result<bool>.Success(true);
    }

    public async Task<Result<List<SmartFileAttach>>> GetFilePath(QueryFile query)
    {
        var lst = await context.SmartFileAttach.Where(c => c.NumberOfVouchers == query.IdContent.ToString() && c.Description == query.ColumnTable)
            .ToListAsync()
            .ConfigureAwait(false);

        return Result<List<SmartFileAttach>>.Success(lst);
    }

    public async Task<Result<string>> HRMUploadFile(FileAttachModel fileAttachCreate)
    {
        string fileUrl = string.Empty;

        foreach (var file in fileAttachCreate.Files)
        {
            long sizeFile = file.Length;
            if (sizeFile > (10 * 1024 * 1024))
            {
                return Result.Failure<string>(new Error("400", "Dung lượng tối đa có thể upload là 10mb."));
            }

            string fileName = file.FileName;
            var today = DateTime.Today;
            string subForderName = today.ToString("ddMMyy");
            string tenantIdForderNamer = contextAccessor.MultiTenantContext.TenantInfo!.Identifier!;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", tenantIdForderNamer, subForderName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            fileUrl = $"Uploads/{tenantIdForderNamer}/{subForderName}/{fileName}";

            var fileAttach = new SmartFileAttach()
            {
                NumberOfVouchers = fileAttachCreate.NumberVoucher,
                FileNames = fileName,
                PathFile = fileUrl,
                Description = fileAttachCreate.ColumnTable,
                SizeFile = file.Length.ToString(),
                TypeFile = file.ContentType,
                CodeUnit = fileAttachCreate.CodeUnit,
                IdContents = fileAttachCreate.IdContent
            };
            context.SmartFileAttach.Add(fileAttach);

            await using FileStream stream = new(Path.Combine(path, fileName), FileMode.OpenOrCreate);
            await file.CopyToAsync(stream).ConfigureAwait(false);
        }

        var count = await context.SaveChangesAsync().ConfigureAwait(false);
        if (count > 0)
        {
            return Result.Success(fileUrl);
        }
        return Result.Failure<string>(new Error("400", "Có lỗi trong quá trình đẩy file"));
    }

    public async Task<Result> UploadFiles(FileAttachRequest request, CancellationToken token)
    {
        int countFileSuccess = 0;
        var lstFiles = await context.SmartFileAttach
            .AsNoTracking()
            .Where(c => c.IdContents == request.IdContent)
            .ToListAsync(token);

        if (request.SmartFileAttachs != null)
        {
            var smartFileAttachs = JsonSerializer.Deserialize<List<SmartFileAttach>>(request.SmartFileAttachs);

            //var lstRemoveFile = lstFiles
            //    .Where(y => smartFileAttachs!.All(x => x.Id != y.Id))
            //    .ToList();

            //foreach (var file in lstRemoveFile)
            //{
            //    string currentDirectory = Directory.GetCurrentDirectory();
            //    string filePath = Path.Combine(currentDirectory, file.PathFile.Replace('/', Path.DirectorySeparatorChar));

            //    if (File.Exists(filePath))
            //    {
            //        try
            //        {
            //            File.Delete(filePath);
            //        }
            //        catch (Exception ex)
            //        {
            //            return Result.Failure<bool>(new Error("500", $"Lỗi khi xóa file: {ex.Message}"));
            //        }
            //    }
            //    context.SmartFileAttach.Remove(file);
            //}
        }

        if (request.Files.Count > 0)
        {
            foreach (var file in request.Files)
            {
                long sizeFile = file.Length;
                if (sizeFile > (10 * 1024 * 1024))
                {
                    return Result.Failure<string>(new Error("400", "Dung lượng tối đa có thể upload là 10mb."));
                }
                var nameFile = Path.GetFileName(file.FileName);
                var extensionFile = Path.GetExtension(file.FileName);

                string fileWithExtension = $"{Guid.NewGuid()}{extensionFile}";
                string path = string.Empty;
                string pathDirection = string.Empty;
                switch (request.Type.ToLower())
                {
                    case "voucher":
                        pathDirection = "Vouchers";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Vouchers", currentUser.CodeUnit.ToString());
                        break;
                    case "course":
                        pathDirection = "Courses";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Courses", currentUser.CodeUnit.ToString());
                        break;
                    case "candidate":
                        pathDirection = "Candidate";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Candidate", currentUser.CodeUnit.ToString());
                        break;
                    case "exam":
                        pathDirection = "Exam";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Exam", currentUser.CodeUnit.ToString());
                        break;
                    case "card_work":
                        pathDirection = "Card_Work";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Card_Work", currentUser.CodeUnit.ToString());
                        break;
                    case "error":
                        pathDirection = "Error";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Error", currentUser.CodeUnit.ToString());
                        break;
                    case "xinnghi":
                        pathDirection = "XINNGHI";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "XINNGHI", currentUser.CodeUnit.ToString());
                        break;
                }

                bool exists = Directory.Exists(path);
                if (!exists)
                    Directory.CreateDirectory(path);
                string fileUrl;

                fileUrl = $"Uploads/{pathDirection}/{currentUser.CodeUnit.ToString()}/{fileWithExtension}";

                var fileAttach = new SmartFileAttach()
                {
                    //Id = Guid.NewGuid(),
                    IdContents = request.IdContent,
                    CodeUnit = currentUser.CodeUnit,
                    FileNames = nameFile,
                    PathFile = fileUrl,
                    NumberOfVouchers = request.NumberOfVouchers ?? string.Empty,
                    Description = request.Type.ToLower(),
                    SizeFile = file.Length.ToString(),
                    CreatedBy = currentUser.CodeUser,
                    CreatedDate = DateTime.Now,
                    TypeFile = file.ContentType,
                };
                context.SmartFileAttach.Add(fileAttach);

                await using FileStream stream = new(Path.Combine(path, fileWithExtension), FileMode.OpenOrCreate);
                await file.CopyToAsync(stream, token).ConfigureAwait(false);

            }
        }
        countFileSuccess = await context.SaveChangesAsync(token);
        if (countFileSuccess > 0)
        {
            string numberOfVoucher = string.IsNullOrWhiteSpace(request.NumberOfVouchers)
                ? ""
                : $"-{request.NumberOfVouchers}";

            Log.Information($"[File]:User: {currentUser.CodeUser} Updated data in the table SmartFileAttach: {request.IdContent}{numberOfVoucher}");

            return Result.Success("Add file successfully.!!");
        }
        return Result.Success();
    }



    public async Task<Result<SmartFileAttach>> GetFileAttchByCode(string code, CancellationToken token)
    {
        var fileGet = await context.SmartFileAttach.FirstOrDefaultAsync(c => c.NumberOfVouchers == code, cancellationToken: token);
        if (fileGet is not null)
        {
            return Result.Success(fileGet);
        }
        return Result.Failure<SmartFileAttach>(new Error("400", "Không tìm thấy file"), null);
    }

    public async Task<Result<SmartFileAttach>> GetSmartFileAttachById(Guid IdContents, CancellationToken token)
    {
        var fileGet = await context.SmartFileAttach.FirstOrDefaultAsync(c => c.IdContents == IdContents, cancellationToken: token);
        if (fileGet is not null)
        {
            return Result.Success(fileGet);
        }
        return Result.Failure<SmartFileAttach>(new Error("400", "Không tìm thấy file"), null);
    }

    public async Task<Result<List<SmartFileAttach>>> GetFileAttchByUser(string codeUser, CancellationToken token)
    {
        try
        {
            var fileget = await context.SmartFileAttach.Where(x => x.CodeUser == codeUser).ToListAsync();
            if (fileget is not null)
            {
                return Result<List<SmartFileAttach>>.Success(fileget);
            }
            return Result.Failure<List<SmartFileAttach>>(new Error("400", "Không có File của người dùng"), null);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<SmartFileAttach>>(new Error("400", ex.Message), null);
        }
    }

    public async Task<Result<List<SmartFileAttach>>> GetListFile(string IdContents, CancellationToken token)
    {
        try
        {
            var fileget = await context.SmartFileAttach.Where(x => x.IdContents.ToString() == IdContents).ToListAsync();
            if (fileget is not null)
            {
                return Result<List<SmartFileAttach>>.Success(fileget);
            }
            return Result.Failure<List<SmartFileAttach>>(new Error("400", "Không có File"), null);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<SmartFileAttach>>(new Error("400", ex.Message), null);
        }
    }
}