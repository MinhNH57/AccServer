using Amazon.Runtime.Internal.Transform;
using BoldReports.Processing.ObjectModels;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using FileHandle.Models;
using MassTransit;
using MiniExcelLibs;
using System.Threading;

namespace FileHandle.Services;

public interface IHandleExcelFileService
{
    Task<Result> UploadFileExcelDynamic(IFormFile file, CancellationToken token);
    Task<Result<List<SheetInfo>>> UploadFileExcel(IFormFile file);
    Task<Result<List<ObjectExcelDto>>> UploadFileExcelObj(IFormFile file);
    Task<Result> ImportDataLeave(IFormFile fileAttach, CancellationToken token);
    Task<Result> ImportDataRecruimentPosition(IFormFile fileAttach, CancellationToken token);
    Task<Result> ImportDataRollCallLocation(IFormFile fileAttach, CancellationToken token); 
    Task<Result> ImportManufacturingStage(IFormFile fileAttach, CancellationToken token);
}

public class HandleExcelFileService(IRequestClient<List<string>> client, ICurrentUser currentUser) : IHandleExcelFileService
{
    public async Task<Result> ImportDataLeave(IFormFile fileAttach, CancellationToken token)
    {
        const long maxFileSize = 20 * 1024 * 1024;

        if (fileAttach.Length == 0)
            return Result.Failure(new Error("400", "File lỗi vui lòng thử lại"));

        if (fileAttach.Length > maxFileSize)
            return Result.Failure(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / 1024 / 1024}MB"));

        try
        {
            var headerMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "TÊN" , "NameObj" },
            { "EMAIL", "Email" },
            { "SỐ ĐIỆN THOẠI", "Numberphone" },
            { "GIỚI TÍNH", "Gender" },
            { "ĐỊA CHỈ", "AddressObj" },
            { "NGUỒN TUYỂN DỤNG", "NameRecruitmentSourcre" },
            { "CHIẾN DỊCH TUYỂN DỤNG", "NameRecruimentCampaign" }
        };

            await using var stream = fileAttach.OpenReadStream();

            var rows = stream.Query(useHeaderRow: true)
                             .Cast<IDictionary<string, object>>()
                             .ToList();

            var list = new List<CandidateListDto>();

            foreach (var row in rows)
            {
                var item = new CandidateListDto();

                foreach (var kv in row)
                {
                    if (headerMap.TryGetValue(kv.Key.Trim(), out var propertyName))
                    {
                        var prop = typeof(CandidateListDto).GetProperty(propertyName);
                        if (prop != null && kv.Value != null)
                        {
                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = Convert.ChangeType(kv.Value, targetType);
                            prop.SetValue(item, safeValue);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(item.NameObj))
                    list.Add(item);
            }

            return Result.Success(list);
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("400", $"Lỗi xử lý file: {ex.Message}"),
                new List<CandidateListDto>());
        }
    }

    public async Task<Result> ImportDataRecruimentPosition(IFormFile fileAttach, CancellationToken token)
    {
        const long maxFileSize = 20 * 1024 * 1024;

        if (fileAttach.Length == 0)
            return Result.Failure(new Error("400", "File lỗi vui lòng thử lại"));

        if (fileAttach.Length > maxFileSize)
            return Result.Failure(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / 1024 / 1024}MB"));

        try
        {
            var headerMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "VỊ TRÍ TUYỂN DỤNG" , "RecruitmentPositionName" },
            { "CHỨC DANH", "NamePosition" },
            { "NƠI LÀM VIỆC", "NameWorkSpace" },
            { "MÔ TẢ", "Dercription" },
            { "YÊU CẦU CÔNG VIỆC", "NameRecruitmentSourcre" },
            { "QUYỀN LỢI ĐƯỢC HƯỞNG", "JobRequirements" }
        };

            await using var stream = fileAttach.OpenReadStream();

            var rows = stream.Query(useHeaderRow: true)
                             .Cast<IDictionary<string, object>>()
                             .ToList();

            var list = new List<RecruimentPosition>();

            foreach (var row in rows)
            {
                var item = new RecruimentPosition();

                foreach (var kv in row)
                {
                    if (headerMap.TryGetValue(kv.Key.Trim(), out var propertyName))
                    {
                        var prop = typeof(RecruimentPosition).GetProperty(propertyName);
                        if (prop != null && kv.Value != null)
                        {
                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = Convert.ChangeType(kv.Value, targetType);
                            prop.SetValue(item, safeValue);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(item.RecruitmentPositionName))
                    list.Add(item);
            }

            return Result.Success(list);
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("400", $"Lỗi xử lý file: {ex.Message}"),
                new List<RecruimentPosition>());
        }
    }

    public async Task<Result> ImportDataRollCallLocation(IFormFile fileAttach, CancellationToken token)
    {
        const long maxFileSize = 20 * 1024 * 1024;

        if (fileAttach.Length == 0)
            return Result.Failure(new Error("400", "File lỗi vui lòng thử lại"));

        if (fileAttach.Length > maxFileSize)
            return Result.Failure(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / 1024 / 1024}MB"));

        try
        {
            var headerMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Tên vị trí" , "LocationName" },
            //{ "Địa chỉ", "NamePosition" },
            { "Kinh độ", "Longitude" },
            { "Vĩ độ", "Latitude" },
            { "Bán kính cho phép", "DistanceAllow" },
        };

            await using var stream = fileAttach.OpenReadStream();

            var rows = stream.Query(useHeaderRow: true)
                             .Cast<IDictionary<string, object>>()
                             .ToList();

            var list = new List<RollCallLocationView>();

            foreach (var row in rows)
            {
                var item = new RollCallLocationView();

                foreach (var kv in row)
                {
                    if (headerMap.TryGetValue(kv.Key.Trim(), out var propertyName))
                    {
                        var prop = typeof(RollCallLocationView).GetProperty(propertyName);
                        if (prop != null && kv.Value != null)
                        {
                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = Convert.ChangeType(kv.Value, targetType);
                            prop.SetValue(item, safeValue);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(item.LocationName))
                    list.Add(item);
            }

            return Result.Success(list);
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("400", $"Lỗi xử lý file: {ex.Message}"),
                new List<RecruimentPosition>());
        }
    }
    public async Task<Result> ImportManufacturingStage(IFormFile fileAttach, CancellationToken token)
    {
        const long maxFileSize = 20 * 1024 * 1024;

        if (fileAttach.Length == 0)
            return Result.Failure(new Error("400", "File lỗi vui lòng thử lại"));

        if (fileAttach.Length > maxFileSize)
            return Result.Failure(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / 1024 / 1024}MB"));

        try
        {
            var headerMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Mã công đoạn" , "AttributeCode" },
                { "Tên công đoạn", "AttributeName" },
                { "Đơn giá", "Quantity" },
                {"Nhóm công đoạn","ManufacturingStageBelongName"}
            };

            await using var stream = fileAttach.OpenReadStream();

            var rows = stream.Query(useHeaderRow: true)
                             .Cast<IDictionary<string, object>>()
                             .ToList();

            var list = new List<SmartProductAttributeByOrderDto>();

            foreach (var row in rows)
            {
                var item = new SmartProductAttributeByOrderDto();

                foreach (var kv in row)
                {
                    if (headerMap.TryGetValue(kv.Key.Trim(), out var propertyName))
                    {
                        var prop = typeof(SmartProductAttributeByOrderDto).GetProperty(propertyName);
                        if (prop != null && kv.Value != null)
                        {
                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = Convert.ChangeType(kv.Value, targetType);
                            prop.SetValue(item, safeValue);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(item.AttributeCode))
                    list.Add(item);
            }

            return Result.Success(list);
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("400", $"Lỗi xử lý file: {ex.Message}"),
                new List<CandidateListDto>());
        }
    }

    public async Task<Result> UploadFileExcelDynamic(IFormFile file, CancellationToken token)
    {
        const long maxFileSize = 20 * 1024 * 1024; // Giới hạn 20MB

        if (file.Length == 0)
            return Result.Failure(new Error("500", "File không hợp lệ"));

        if (file.Length > maxFileSize)
            return Result.Failure(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / (1024 * 1024)}MB"));
        try
        {
            await using var stream = file.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream, token); // Copy file vào MemoryStream
            memoryStream.Position = 0; // Đưa con trỏ về đầu stream 
            //var table = memoryStream.QueryAsDataTable(useHeaderRow: true,startCell:"A2");
 
            var rows = (await memoryStream.QueryAsync(useHeaderRow: true, startCell: "A2", cancellationToken: token)).ToList();
            return Result.Success(rows);

        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("500", $"Lỗi xử lý file: {ex.Message}"));
        }
    }

    public async Task<Result<List<SheetInfo>>> UploadFileExcel(IFormFile file)
    {
        const long maxFileSize = 20 * 1024 * 1024; // Giới hạn 20MB

        if (file == null || file.Length == 0)
            return Result.Failure<List<SheetInfo>>(new Error("500", "File không hợp lệ"));

        if (file.Length > maxFileSize)
            return Result.Failure<List<SheetInfo>>(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / (1024 * 1024)}MB"));

        try
        {
            await using var stream = file.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream); // Copy file vào MemoryStream
            memoryStream.Position = 0; // Đưa con trỏ về đầu stream

            var sheetNames = memoryStream.GetSheetNames(); // Lấy danh sách sheet
            var sheetInfos = new List<SheetInfo>();

            foreach (var sheet in sheetNames.Select((name, index) => new { name, index }))
            {
                memoryStream.Position = 0; // Reset vị trí đọc stream trước khi query
                var sheetData = (await memoryStream.QueryAsync()).ToList();
                int totalRows = sheetData.Count;

                sheetInfos.Add(new SheetInfo
                {
                    Index = sheet.index,
                    Name = sheet.name,
                    CountRow = totalRows,
                    MaxDataRow = totalRows > 0 ? totalRows - 1 : 0, // Nếu có dữ liệu, trừ 1 dòng header
                    RowHeaderUltil = 1 // Giả sử dòng đầu là header
                });
            }

            return Result<List<SheetInfo>>.Success(sheetInfos);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<SheetInfo>>(new Error("500", $"Lỗi xử lý file: {ex.Message}"));
        }
    }

    public async Task<Result<List<ObjectExcelDto>>> UploadFileExcelObj(IFormFile file)
    {
        const long maxFileSize = 20 * 1024 * 1024; 

        if (file == null || file.Length == 0)
            return Result.Failure<List<ObjectExcelDto>>(new Error("500", "File không hợp lệ"));

        if (file.Length > maxFileSize)
            return Result.Failure<List<ObjectExcelDto>>(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / (1024 * 1024)}MB"));

        try
        {
            await using var stream = file.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0; 

            var listData = memoryStream.Query<ObjectExcelDto>(startCell: "A9").ToList();

            if (listData.Count > 0)
            {
                listData.RemoveAll(x => string.IsNullOrWhiteSpace(x.CitizenIDNumber));
                int a = 0;

                //TODO: Xử lý check CCCD trong file Excel : Chưa test
                var response = await client.GetResponse<List<string>>(listData.Select(c => c.CitizenIDNumber), c =>
                {
                    c.UseExecute(q => q.Headers.Set("X-Tenant-Id", currentUser.TenantId));
                });
                var listExist = response.Message;
                if (listExist.Any())
                {
                    listData.ForEach(c =>
                    {
                        if (listExist.FirstOrDefault(q => q == c.CitizenIDNumber) is not null)
                        {
                            c.CitizenIDNumber = $"Đối tượng chứa căn cước '{c.CitizenIDNumber}' đã tồn tại trong hệ thống";
                        }
                    });
                }

            }
            return Result<List<ObjectExcelDto>>.Success(listData);

        }
        catch (Exception ex)
        {
            return Result.Failure<List<ObjectExcelDto>>(new Error("500", $"Lỗi xử lý file: {ex.Message}"));
        }
    }
}