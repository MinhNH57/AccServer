using System.Text.Json;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using FileHandle.Data;
using FileHandle.Data.Entites;
using FileHandle.Models;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FileHandle.Services.HRM;

public class FileHandleHrmService : IFileHandleHRMService
{
    private readonly HRMFileHandleDbContext _context;
    private readonly IMultiTenantContextAccessor _contextAccessor;
    private readonly IMultiTenantStore<TenantInfoCustomize> _tenantStore;
    private readonly IMultiTenantContextSetter _multiTenantContextSetter;
    private readonly ICurrentUser _currentUser;

    // Constructor chuẩn để DI nhận
    public FileHandleHrmService(
        HRMFileHandleDbContext context,
        IMultiTenantContextAccessor contextAccessor,
        IMultiTenantStore<TenantInfoCustomize> tenantStore,
        IMultiTenantContextSetter multiTenantContextSetter,
        ICurrentUser currentUser)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _tenantStore = tenantStore;
        _multiTenantContextSetter = multiTenantContextSetter;
        _currentUser = currentUser;
    }

    public async Task<Result<List<HRM_SmartFileAttach>>> GetListFile(string IdContents, CancellationToken token)
    {
        try
        {
            var fileget = await _context.SmartFileAttach
                .Where(x => x.IdContents.ToString() == IdContents)
                .ToListAsync(token);

            if (fileget is not null && fileget.Any())
            {
                return Result<List<HRM_SmartFileAttach>>.Success(fileget);
            }

            return Result.Failure<List<HRM_SmartFileAttach>>(new Error("400", "Không có File"), null);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<HRM_SmartFileAttach>>(new Error("400", ex.Message), null);
        }
    }

    public async Task<Result<HRM_SmartFileAttach>> GetSmartFileAttachById(Guid Id, CancellationToken token)
    {
        var fileGet = await _context.SmartFileAttach.FirstOrDefaultAsync(c => c.Id == Id, cancellationToken: token);
        if (fileGet is not null)
        {
            return Result.Success(fileGet);
        }
        return Result.Failure<HRM_SmartFileAttach>(new Error("400", "Không tìm thấy file"), null);
    }

    public async Task<Result> HRMUploadListFiles(FileAttachRequest request, CancellationToken token)
    {
        int countFileSuccess = 0;
        var lstFiles = await _context.SmartFileAttach
            .AsNoTracking()
            .Where(c => c.IdContents == request.IdContent)
            .ToListAsync(token);

        if (request.SmartFileAttachs != null)
        {
            var smartFileAttachs = JsonSerializer.Deserialize<List<HRM_SmartFileAttach>>(request.SmartFileAttachs);

            var lstRemoveFile = lstFiles
                .Where(y => smartFileAttachs!.All(x => x.Id != y.Id))
                .ToList();

            foreach (var file in lstRemoveFile)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string filePath = Path.Combine(currentDirectory, file.PathFile.Replace('/', Path.DirectorySeparatorChar));

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
                _context.SmartFileAttach.Remove(file);
            }
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


                //string fileWithExtension = $"{nameFile}{extensionFile}";
                string fileWithExtension = $"{Guid.NewGuid()}{extensionFile}";
                string path = string.Empty;
                string pathDirection = string.Empty;
                switch (request.Type.ToLower())
                {
                    case "voucher":
                        pathDirection = "Vouchers";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Vouchers", _currentUser.CodeUnit.ToString());
                        break;
                    case "course":
                        pathDirection = "Courses";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Courses", _currentUser.CodeUnit.ToString());
                        break;
                    case "candidate":
                        pathDirection = "Candidate";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Candidate", _currentUser.CodeUnit.ToString());
                        break;
                    case "exam":
                        pathDirection = "Exam";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Exam", _currentUser.CodeUnit.ToString());
                        break;
                    case "card_work":
                        pathDirection = "Card_Work";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Card_Work", _currentUser.CodeUnit.ToString());
                        break;
                    case "error":
                        pathDirection = "Error";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Error", _currentUser.CodeUnit.ToString());
                        break;
                    case "workdaily":
                        pathDirection = "Workdaily";
                        path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Workdaily", _currentUser.CodeUnit.ToString());
                        break;
                }

                bool exists = Directory.Exists(path);
                if (!exists)
                    Directory.CreateDirectory(path);
                var fileUrl = $"Uploads/{pathDirection}/{_currentUser.CodeUnit.ToString()}/{fileWithExtension}";


                var fileAttach = new HRM_SmartFileAttach
                {
                    Id = Guid.NewGuid(),
                    IdContents = request.IdContent,
                    CodeUnit = _currentUser.CodeUnit,
                    FileNames = nameFile,
                    PathFile = fileUrl,
                    NumberOfVouchers = request.NumberOfVouchers ?? string.Empty,
                    Description = request.Type.ToLower(),
                    SizeFile = file.Length.ToString(),
                    CreatedBy = _currentUser.CodeUser,
                    CodeUser = _currentUser.CodeUser,
                    CreatedDate = DateTime.Now,
                    TypeFile = file.ContentType,
                };
                _context.SmartFileAttach.Add(fileAttach);

                await using FileStream stream = new(Path.Combine(path, fileWithExtension), FileMode.OpenOrCreate);
                await file.CopyToAsync(stream, token).ConfigureAwait(false);

            }
        }
        countFileSuccess = await _context.SaveChangesAsync(token);
        if (countFileSuccess > 0)
        {
            string numberOfVoucher = string.IsNullOrWhiteSpace(request.NumberOfVouchers)
                ? ""
                : $"-{request.NumberOfVouchers}";

            Log.Information($"[File]:User: {_currentUser.CodeUser} Updated data in the table HRM_SmartFileAttach: {request.IdContent}{numberOfVoucher}");

            return Result.Success("Add file successfully.!!");
        }
        return Result.Success();
    }
}
