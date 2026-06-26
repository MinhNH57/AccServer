using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.QueryModels;
using SmartAccCloud.Application.StoreViewModels;
using MiniExcelLibs;
using SmartAccCloud.Application.Models.ExcelModel;
using SmartAccCloud.Application.Models.GetDatas;
using SmartAccCloud.Application.Services.Dynamic;
using SmartAccCloud.Application.Services.Catalogs.CatalogObject;

namespace SmartAccCloud.Application.Services.Extension;

public class ExtensionServices(IApplicationDbContext context, IHttpClientFactory httpClient, ICurrentUser currentUser, IDynamicCreateObjectServices dynamicCreateObjectServices, ICatalogObjectServices objectServices) : IExtensionServices
{
    /// <summary>
    /// Hàm lấy mã tự sinh của danh mục bằng mã nhóm
    /// </summary>
    /// <param name="query"> Truyền tên bảng, Mã nhóm, độ dài</param>
    /// <returns></returns>

    public async Task<Result<SmartCode>> GetCodeCatalogByGroupCode(QueryCodeCatalogByGroup query)
    {
        var result = await context.Database.SqlQueryRaw<SmartCode>(
        "SELECT dbo.GetCodeCatalogByGroupCode({0}, {1}, {2}) as SmartCode",
        query.TableName, query.GroupCode, query.Length).ToListAsync().ConfigureAwait(false);
        // Chuyển đổi kết quả từ kiểu object sang kiểu string
        return Result<SmartCode>.Success(result.ToList()[0]);
    }

    public async Task<Result<VietQrTaxCode.ReponseVietQr>> GetTaxCodeInfo(string taxCode)
    {
        var client = httpClient.CreateClient();
        var response = await client.GetAsync($"https://api.vietqr.io/v2/business/{taxCode}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<VietQrTaxCode.ReponseVietQr>(responseContent);
        if (response.IsSuccessStatusCode)
        {
            return Result<VietQrTaxCode.ReponseVietQr>.Success(result);
        }

        return Result<VietQrTaxCode.ReponseVietQr>.Failure(new Error($"{result.Code}", $"{result.Desc}"));
    }
    /// <summary>
    /// Gộp mã danh mục
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>


    public async Task<bool> GroupCodeCategory(GroupCodeQuery query)
    {
        if (string.IsNullOrWhiteSpace(query.SourceCode) || string.IsNullOrWhiteSpace(query.DestinationCode))
            return false;

        string sql = "EXEC dbo.SmartGroupCodeCatalog @Param1, @Param2, @Param3, @Param4, @Param5, @Param6, @Param7";
        var parameters = new[]
        {
            new SqlParameter("@Param1", query.Parameter ?? (object)DBNull.Value),
            new SqlParameter("@Param2", query.UserCode ?? (object)DBNull.Value),
            new SqlParameter("@Param3", query.CodeUnit ?? (object)DBNull.Value),
            new SqlParameter("@Param4", query.TableName ?? (object)DBNull.Value),
            new SqlParameter("@Param5", query.SourceCode ?? (object)DBNull.Value),
            new SqlParameter("@Param6", query.DestinationCode ?? (object)DBNull.Value),
            new SqlParameter("@Param7", query.IsDelete)
        };
        await context.Database.ExecuteSqlRawAsync(sql, parameters);


        return true;
    }


    /// <summary>
    /// Lấy ra số chứng từ cho việc tạo phiếu mới
    /// function sql : [dbo].[GenNoInv] (@UserCode nvarchar(20),@CodeUnit int,@IsDate bit,@Date nvarchar(10),@TableName nvarchar(50),@DataType nvarchar(20)) returns nvarchar(30)
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns>Số chứng từ mới </returns>
    public async Task<SmartCode> GetNoCoupon(GetNoCouponRequest request, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        int codeUnit = currentUser.CodeUnit;
        //string userCode = currentUser.CodeUser!;
        var result = await context.Database.SqlQueryRaw<SmartCode>(
            "SELECT dbo.GenNoInv({0}, {1},{2}, {3},{4},{5}) as SmartCode", "", codeUnit, 1, request.Date, request.TableName, request.DataType)
            .ToListAsync(token)
            .ConfigureAwait(false);
        var noCoupon = result[0];

        return noCoupon;
    }

    public async Task<Result<List<SheetInfo>>> UploadFileExcel(IFormFile file)
    {
        const long maxFileSize = 20 * 1024 * 1024; // Giới hạn 20MB

        if (file == null || file.Length == 0)
            return Result<List<SheetInfo>>.Failure(new Error("500", "File không hợp lệ"));

        if (file.Length > maxFileSize)
            return Result<List<SheetInfo>>.Failure(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / (1024 * 1024)}MB"));

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
            return Result<List<SheetInfo>>.Failure(new Error("500", $"Lỗi xử lý file: {ex.Message}"));
        }
    }

    public async Task<Result<List<ObjectExcelDto>>> UploadFileExcelObj(IFormFile file)
    {
        const long maxFileSize = 20 * 1024 * 1024; // Giới hạn 20MB

        if (file == null || file.Length == 0)
            return Result<List<ObjectExcelDto>>.Failure(new Error("500", "File không hợp lệ"));

        if (file.Length > maxFileSize)
            return Result<List<ObjectExcelDto>>.Failure(new Error("500", $"Dung lượng file quá lớn! Chỉ chấp nhận file <= {maxFileSize / (1024 * 1024)}MB"));

        try
        {
            await using var stream = file.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream); // Copy file vào MemoryStream
            memoryStream.Position = 0; // Đưa con trỏ về đầu stream

            // Tự động ánh xạ vào List<CatalogObject>
            var listData = memoryStream.Query<ObjectExcelDto>(startCell: "A9").ToList();

            if (listData.Count > 0)
            {
                listData.RemoveAll(x => string.IsNullOrWhiteSpace(x.CitizenIDNumber));
                int a = 0;
                foreach (var item in listData)
                {
                    var dataGet = await objectServices.GetObjectByCccd(item.CitizenIDNumber);
                    if (dataGet.IsSuccess)
                    {
                        listData[a].CitizenIDNumber = $"Đối tượng chứa căn cước '{item.CitizenIDNumber}' đã tồn tại trong hệ thống";
                    }
                    a++;
                }
            }
            return Result<List<ObjectExcelDto>>.Success(listData);

        }
        catch (Exception ex)
        {
            return Result<List<ObjectExcelDto>>.Failure(new Error("500", $"Lỗi xử lý file: {ex.Message}"));
        }
    }
}