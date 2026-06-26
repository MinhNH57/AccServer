using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using AutoMapper;
using Finbuckle.MultiTenant.Abstractions;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Application.Pagination;
using SmartAccCloud.Application.Response;
using SmartAccCloud.Application.Services.Notifications;
using SmartAccCloud.Domain.SteelModelStores;

namespace SmartAccCloud.Application.Services.TonMyAnh;

public class SendConfirmVocherNoti
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string Title { get; set; } = "Smart Steel";
    public string Body { get; set; } = "Có phiếu cần duyệt";
    public Dictionary<string, string>? Data { get; set; }
}

public class ReportQuery
{
    public string Parameter { get; set; } = string.Empty;
    public string UserCode { get; set; } = string.Empty;
    public string? GroupCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}

public class CreateBalanceFluctuationRequest
{
    [JsonProperty(PropertyName = "codeUnit")]
    public int CodeUnit { get; set; }

    [JsonProperty(PropertyName = "amount")]
    public long Amount { get; set; }

    [JsonProperty(PropertyName = "amountBlance")]
    public long AmountBlance { get; set; }

    [JsonProperty(PropertyName = "fullContent")]
    public string? FullContent { get; set; }

    [JsonProperty(PropertyName = "remittanceContent")]
    public string? RemittanceContent { get; set; }

    [JsonProperty(PropertyName = "bankOfAccount")]
    public string? BankOfAccount { get; set; }

    [JsonProperty(PropertyName = "bankOfName")]
    public string? BankOfName { get; set; }

    [JsonProperty(PropertyName = "bankOfAccountReceive")]
    public string? BankOfAccountReceive { get; set; }

    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty(PropertyName = "notes")]
    public string? Notes { get; set; }
}

public class ProfitLossResult
{
    public string? MaChiTieu { get; set; }
    public string? ChiTieu { get; set; }
    public double TotalPayment { get; set; }
    public List<double> ArrAmount { get; set; }
    public double Percent { get; set; }
    public double TotalAmount { get; set; }
}

public class TongHopCongNoResult
{
    public decimal TongTienCoDk { get; set; }
    public decimal TongTienCoCk { get; set; }
    public decimal TongTienNo { get; set; }
    public decimal TongTienCo { get; set; }
    public decimal TongTienNoDk { get; set; }
    public decimal TongTienNoCk { get; set; }
    public decimal TongVuotHM { get; set; }
    public List<TongHopCongNo>? TongHopCongNo { get; set; }
}

public class LoiNhuanTheoHang
{
    public string MaKho { get; set; }
    public double SoLuong { get; set; }
    public string? MaHang { get; set; }
    public string? TenHang { get; set; }
    public double DoanhThu { get; set; }
    public double GiaVon { get; set; }
    public double LoiNhuanGop { get; set; }
    public double DoanhThuTheoThang { get; set; }
    public double DoanhThuTheoNam { get; set; }
    public double DoanhThuTheoNgay { get; set; }
}

public class LoiNhuanTheoHangResult
{
    public List<LoiNhuanTheoHang> LoiNhuanTheoHangs { get; set; }
    public double TongDoanhThu { get; set; }
    public double TongGiaVon { get; set; }
    public double TongLoiNhuanGop { get; set; }
    public double TongDoanhThuTheoThang { get; set; }
    public double TongDoanhThuTheoNam { get; set; }
    public double TongDoanhThuTheoNgay { get; set; }
}

public class ProfitByGrpProduct
{
    public string? MaHang { get; set; }
    public string? TenHang { get; set; }
    public string? MaNhom { get; set; }
    public string? TenNhom { get; set; }
    public double TrongKy { get; set; }
    public double TrongThang { get; set; }
    public double TrongNam { get; set; }
    public double TienVon { get; set; }
    public double LoiNhuan { get; set; }
}

public class ProfitByGrpProductResult
{
    public double TongTrongKy { get; set; }
    public double TongTrongThang { get; set; }
    public double TongTrongNam { get; set; }
    public double TongLoiNhuan { get; set; }
    public double TongTienVon { get; set; }
    public List<ProfitByGrpProduct> Data { get; set; }
}

public class BaoCaoLaiLoTheoMatHang
{
    public string? MAKHO { get; set; }
    public string? MASO { get; set; }
    public string? CHITIEU { get; set; }
    public string? CHITIEU1 { get; set; }
    public double SOTIEN01 { get; set; }
    public double SOTIEN02 { get; set; }
    public double SOTIEN03 { get; set; }
    public double SOTIEN04 { get; set; }
    public double SOTIEN05 { get; set; }
    public double SOTIEN06 { get; set; }
    public double SOTIEN07 { get; set; }
    public double SOTIEN08 { get; set; }
    public double SOTIEN09 { get; set; }
    public double SOTIEN10 { get; set; }
    public double SOTIEN11 { get; set; }
    public double SOTIEN12 { get; set; }
    public double SOTIENALL { get; set; }
}

public interface ITonMoblieService
{
    Task<Result<List<DataControlled>>> GetAll();
    Task<Result<PagedResult<DataControlled>>> GetPagination(PaginationRequest query, CancellationToken token);
    Task<Result<DataControlled>> Get(string numberOfVoucher);
    Task<Result<bool>> ConfirmVoucher(List<string> listNumberVoucher);
    Task<Result<bool>> PushNofitication(SendConfirmVocherNoti request);
    Task<Result<bool>> CreateBalanceFluctuation(string? checkSum, CreateBalanceFluctuationRequest request,
        CancellationToken token);
    Task<Result<bool>> CreateListBalanceFluctuation(string? checkSum, List<CreateBalanceFluctuationRequest> requests,
        CancellationToken token);

    Task<Result<TongHopCongNoResult>> GetReportDebt(ReportQuery query);
    Task<Result<List<ProfitLossResult>>> GetBaoCaoLaiLo(ReportQuery query);
    Task<Result<LoiNhuanTheoHangResult>> Getprofit(ReportQuery query);
    Task<Result<ProfitByGrpProductResult>> ReportProfitGrpProduct(ReportQuery query);
}

//Hiện tại các chức năng đang cho riêng Yêu cầu của tôn Mỹ Anh, Mỹ Ấn (Data cũ) sử dụng trên Mobile

public class TonMoblieService(
    IDataServices dataServices,
    IApplicationDbContext dbContext,
    IMultiTenantContextAccessor tenantContextAccessor,
    INotificationService notificationService,
    ICurrentUser currentUser,
    IConfiguration configuration) : ITonMoblieService
{
    private readonly TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
    private readonly string _secretKey = configuration.GetValue<string>("Cryptography:KeyHash")!;

    /// <summary>
    /// Lấy tất cả các phiếu 
    /// </summary>
    /// <returns></returns>
    public async Task<Result<List<DataControlled>>> GetAll()
    {
        var data = await dbContext.DataControlled
            .AsTracking()
            .ToListAsync();

        return Result<List<DataControlled>>.Success(data);
    }

    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<Result<PagedResult<DataControlled>>> GetPagination(PaginationRequest request,
        CancellationToken token)
    {
        //var decsription = currentUser.Description;
        //PaginationRequest query = new PaginationRequest()
        //{
        //    PageSize = request.PageSize,
        //    PageNumber = request.PageNumber,
        //    SearchByFields = request.SearchValue != null ? [new SearchModel(request.SearchFieldName, request.SearchValue)] : null,
        //    SortModels = request.SortField != null ? [new SortModel(request.SortField, request.SortDirection)] : null
        //};

        var data = await dbContext.DataControlled
            .AsNoTracking()
            .PaginateAsync(request, token);

        return Result<PagedResult<DataControlled>>.Success(data);
    }
    public async Task<Result<DataControlled>> Get(string numberOfVoucher)
    {
        var data = await dbContext.DataControlled
            .AsTracking()
            .FirstOrDefaultAsync(c => c.Id == numberOfVoucher);

        if (data is null)
            return Result<DataControlled>.Failure(new Error("404", "Không tìm thấy phiếu"));

        return Result<DataControlled>.Success(data);
    }

    /// <summary>
    /// Duyệt phiếu
    /// </summary>
    /// <param name="listNumberVoucher"></param>
    /// <returns></returns>
    public async Task<Result<bool>> ConfirmVoucher(List<string> listNumberVoucher)
    {
        var description = currentUser.Description;
        var strSplit = string.Join(';', listNumberVoucher);
        var param = new
        { Parameter = "ComfirmVoucher", TableName = description, KeyData = strSplit, DataPlus = "", MaUser = "", CodeUnit = 0 };
        var count = await dataServices.ExcuteNonQuery("StoreComfirmVoucher", _tenant.ConnectionString(), param);
        if (count > 0)
        {
            return Result<bool>.Success(true);
        }
        return Result<bool>.Failure(new("400", "Không có phiếu nào được duyệt"));
    }

    /// <summary>
    /// Đẩy thông báo duyệt phiếu lên Mobile
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Result<bool>> PushNofitication(SendConfirmVocherNoti request)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(c => c.CodeUser == request.UserName);

        if (user == null)
            return Result<bool>.Failure(new Error("400", "Sai tài khoản hoặc mật khẩu"));
        string? numOfVoucher = null;
        request.Data?.TryGetValue("Id", out numOfVoucher);
        var voucher = await dbContext.DataControlled.FirstOrDefaultAsync(c => c.Id == numOfVoucher);
        if (voucher?.Controlled == false || voucher is null)
        {
            return Result<bool>.Success(true);
        }
        await notificationService.SendNotification(new()
        {
            Notification = new Notification()
            {
                Title = request.Title,
                Body = request.Body
            },
            Topic = _tenant.Notes,
            Data = request.Data
        });

        return Result<bool>.Success(true);
    }

    /// <summary>
    /// Ghi lại biến động số dư tài khoản
    /// Sử dụng hàm băm sha256 để mã hóa
    /// </summary>
    /// <param name="checkSum"></param>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<Result<bool>> CreateBalanceFluctuation(string? checkSum, CreateBalanceFluctuationRequest request,
        CancellationToken token)
    {
        var jsonRequest = JsonConvert.SerializeObject(request);

        if (string.IsNullOrEmpty(checkSum) || !ValidateCheckSum(checkSum, jsonRequest))
            return Result<bool>.Failure(new Error("400", "Check sum không hợp lệ"));

        var isSuccess = await InsertData([request], token);

        if (isSuccess)
            return Result<bool>.Success(true);
        return Result<bool>.Failure(new Error("400", "Có lỗi khi insert"));
    }

    public async Task<Result<bool>> CreateListBalanceFluctuation(string? checkSum,
        List<CreateBalanceFluctuationRequest> requests, CancellationToken token)
    {
        var jsonRequest = JsonConvert.SerializeObject(requests[0]);

        //if (string.IsNullOrEmpty(checkSum) || !ValidateCheckSum(checkSum, jsonRequest))
        //    return Result<bool>.Failure(new Error("400", "Check sum không hợp lệ"));

        var isSuccess = await InsertData(requests, token);

        if (isSuccess)
            return Result<bool>.Success(true);

        return Result<bool>.Failure(new Error("400", "Có lỗi khi insert"));
    }

    public async Task<Result<TongHopCongNoResult>> GetReportDebt(ReportQuery query)
    {
        if (string.IsNullOrEmpty(query.UserCode))
        {
            var param = new
            {
                hien = 0,
                ThamSo = query.Parameter,
                date1 = query.StartDate.Date.ToString(CultureInfo.InvariantCulture),
                date2 = query.EndDate.Date.ToString(CultureInfo.InvariantCulture),
                MaUser = "",
                tmptblOK = ""
            };
            var data = await dataServices.GetListObject<TongHopCongNo>("VT_BangTongHopCongNo", _tenant.ConnectionString(), param);

            var result =
                new TongHopCongNoResult()
                {
                    TongHopCongNo = data,
                    TongTienCo = data.Sum(c => c.Sotienco) ?? 0,
                    TongTienNo = data.Sum(c => c.Sotienno) ?? 0,
                    TongTienCoDk = data.Sum(c => c.Sotiencodk) ?? 0,
                    TongTienCoCk = data.Sum(c => c.Sotiencock) ?? 0,
                    TongTienNoDk = data.Sum(c => c.Sotiennodk) ?? 0,
                    TongTienNoCk = data.Sum(c => c.Sotiennock) ?? 0,
                    TongVuotHM = data.Where(c => c.SoTien1 > 0).Sum(c => c.SoTien1) ?? 0,
                };

            return Result<TongHopCongNoResult>.Success(result);

        }
        //Chi tiết công nợ 1 đơn vị
        else
        {
            var param = new
            {
                hien = 0,
                ThamSo = query.Parameter,
                PathImages = "",
                date1 = query.StartDate.Date.ToString(CultureInfo.InvariantCulture),
                date2 = query.EndDate.Date.ToString(CultureInfo.InvariantCulture),
                MaDonVi = query.UserCode,
                tmptblOK = ""
            };
            var data = await dataServices.GetListObject<TongHopCongNo>("VT_SoChiTietCongNo", _tenant.ConnectionString(), param);
            if (data.Count < 3)
                return Result<TongHopCongNoResult>.Success(new());
            //Nợ cuối kỳ -- Tổng tiền có đầu kỳ (T_T)
            var result = new TongHopCongNoResult()
            {
                TongHopCongNo = data,
                TongTienCoDk = data.First().Sotiencon ?? 0,
                TongTienNoCk = data.Last().Sotiennock ?? 0,
                TongTienCoCk = data.Last().Sotiencon ?? 0,
                TongTienNo = data[^2].Sotienno ?? 0,
                TongTienCo = data[data.Count - 2].Sotienco ?? 0
            };

            return Result<TongHopCongNoResult>.Success(result);

        }
    }


    public async Task<Result<List<ProfitLossResult>>> GetBaoCaoLaiLo(ReportQuery query)
    {
        var param = new
        {
            hien = 0,
            ThamSo = "APPKQKDMATHANG",
            PathImages = "",
            MaUser = "ADMIN",
            date1 = query.StartDate.Date.ToString(CultureInfo.InvariantCulture),
            date2 = query.EndDate.Date.ToString(CultureInfo.InvariantCulture),
            //IdDonviOk = 4032,
            IdDonviOk = currentUser.CodeUnit,
            tmptblOK = ""
        };
        //var data = await dataServices.GetListObject<BaoCaoLaiLo>("VT_BaoCaoLaiLo", _tenant.ConnectionString(), param);

        if (query.Parameter == "sales")
        {
            var data = await dataServices.GetListObject<BaoCaoLaiLo>("VT_BaoCaoLaiLo", _tenant.ConnectionString(), param);

            var result = GetSalesReport(data);
            return Result<List<ProfitLossResult>>.Success(result);
        }

        if (query.Parameter == "performance")
        {
            var data = await dataServices.GetListObject<BaoCaoLaiLo>("VT_BaoCaoLaiLo", _tenant.ConnectionString(), param);
            var result = GetPerformanceReport(data);
            return Result<List<ProfitLossResult>>.Success(result);
        }

        if (query.Parameter == "index")
        {
            var result = GetIndexReport(query.StartDate, query.EndDate);

            return Result<List<ProfitLossResult>>.Success(result);
        }
        return Result<List<ProfitLossResult>>.Success(new());
    }

    public async Task<Result<LoiNhuanTheoHangResult>> Getprofit(ReportQuery query)
    {
        var param = new
        {
            hien = 0,
            ThamSo = "APPKQKDMATHANG",
            PathImages = "",
            MaUser = "ADMIN",
            date1 = query.StartDate.Date.ToString(CultureInfo.InvariantCulture),
            date2 = query.EndDate.Date.ToString(CultureInfo.InvariantCulture),
            //IdDonviOk = 4032,
            IdDonviOk = currentUser.CodeUnit,

            tmptblOK = ""
        };
        if (query.Parameter == "Product")
        {
            var data = await dataServices.GetListObject<LoiNhuanTheoHang>("VT_BaoCaoDoanhSoBanHangQuanTri", _tenant.ConnectionString(), param);
            return Result<LoiNhuanTheoHangResult>.Success(GetSalesReportByProduct(data));
        }

        if (query.Parameter == "ProductDetail")
        {
            var param1 = new
            {
                hien = 0,
                ThamSo = "KQKDMATHANGDETAIL",
                PathImages = query.GroupCode,
                MaUser = "ADMIN",
                date1 = query.StartDate.Date.ToString(CultureInfo.InvariantCulture),
                date2 = query.EndDate.Date.ToString(CultureInfo.InvariantCulture),
                //IdDonviOk = 4032,
                IdDonviOk = currentUser.CodeUnit,
                tmptblOK = ""
            };
            var data = await dataServices.GetListObject<LoiNhuanTheoHang>("VT_BaoCaoDoanhSoBanHangQuanTri", _tenant.ConnectionString(), param1);

            return Result<LoiNhuanTheoHangResult>.Success(GetSalesReportByProductDetail(data));
        }

        return Result<LoiNhuanTheoHangResult>.Success(new());
    }

    public async Task<Result<ProfitByGrpProductResult>> ReportProfitGrpProduct(ReportQuery query)
    {
        var param = new
        {
            hien = 0,
            ThamSo = "APPKQKDMATHANG1",
            PathImages = "",
            MaUser = "ADMIN",
            //date1 = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture),
            //date2 = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture),
            date1 = query.StartDate.Date.ToString(CultureInfo.InvariantCulture),
            date2 = query.EndDate.Date.ToString(CultureInfo.InvariantCulture),
            //IdDonviOk = 4032,
            IdDonviOk = currentUser.CodeUnit,

            tmptblOK = ""
        };
        var data = await dataServices
            .GetListObject<ProfitByGrpProduct>("VT_BaoCaoDoanhSoBanHangQuanTri", _tenant.ConnectionString(), param);

        var result = new ProfitByGrpProductResult()
        {
            Data = data,
            TongTrongKy = data.Sum(c => c.TrongKy),
            TongTrongThang = data.Sum(c => c.TrongThang),
            TongTrongNam = data.Sum(c => c.TrongNam)
        };

        return Result<ProfitByGrpProductResult>.Success(result);
    }

    private List<ProfitLossResult> GetPerformanceReport(List<BaoCaoLaiLo> data)
    {
        return
        [
            new ProfitLossResult
            {
                MaChiTieu = "Báo cáo kết quả hoạt động",
                ChiTieu = "Doanh thu",
                ArrAmount =
                [
                    data.First(c => c.MAKHO == "02").M1 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M1 ?? 0),
                    data.First(c => c.MAKHO == "02").M2 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M2 ?? 0),
                    data.First(c => c.MAKHO == "02").M3 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M3 ?? 0),
                    data.First(c => c.MAKHO == "02").M4 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M4 ?? 0),
                    data.First(c => c.MAKHO == "02").M5 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M5 ?? 0),
                    data.First(c => c.MAKHO == "02").M6 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M6 ?? 0),
                    data.First(c => c.MAKHO == "02").M7 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M7 ?? 0),
                    data.First(c => c.MAKHO == "02").M8 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M8 ?? 0),
                    data.First(c => c.MAKHO == "02").M9 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M9 ?? 0),
                    data.First(c => c.MAKHO == "02").M10 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M10 ?? 0),
                    data.First(c => c.MAKHO == "02").M11 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M11 ?? 0),
                    data.First(c => c.MAKHO == "02").M12 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M12 ?? 0)
                ]
            },
            new ProfitLossResult
            {
                MaChiTieu = "Báo cáo kết quả hoạt động",
                ChiTieu = "Chi phí",
                ArrAmount =
                [
                    data.First(c => c.MAKHO == "03").M1 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M1 ?? 0),
                    data.First(c => c.MAKHO == "03").M2 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M2 ?? 0),
                    data.First(c => c.MAKHO == "03").M3 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M3 ?? 0),
                    data.First(c => c.MAKHO == "03").M4 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M4 ?? 0),
                    data.First(c => c.MAKHO == "03").M5 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M5 ?? 0),
                    data.First(c => c.MAKHO == "03").M6 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M6 ?? 0),
                    data.First(c => c.MAKHO == "03").M7 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M7 ?? 0),
                    data.First(c => c.MAKHO == "03").M8 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M8 ?? 0),
                    data.First(c => c.MAKHO == "03").M9 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M9 ?? 0),
                    data.First(c => c.MAKHO == "03").M10 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M10 ?? 0),
                    data.First(c => c.MAKHO == "03").M11 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M11 ?? 0),
                    data.First(c => c.MAKHO == "03").M12 + (data.FirstOrDefault(c => c.MAKHO == "05")?.M12 ?? 0)
                ]
            },

            new ProfitLossResult
            {
                MaChiTieu = "Báo cáo kết quả hoạt động",
                ChiTieu = "Lợi nhuận ròng",
                ArrAmount =
                [
                    data.First(c => c.MAKHO == "06").M1,
                    data.First(c => c.MAKHO == "06").M2,
                    data.First(c => c.MAKHO == "06").M3,
                    data.First(c => c.MAKHO == "06").M4,
                    data.First(c => c.MAKHO == "06").M5,
                    data.First(c => c.MAKHO == "06").M6,
                    data.First(c => c.MAKHO == "06").M7,
                    data.First(c => c.MAKHO == "06").M8,
                    data.First(c => c.MAKHO == "06").M9,
                    data.First(c => c.MAKHO == "06").M10,
                    data.First(c => c.MAKHO == "06").M11,
                    data.First(c => c.MAKHO == "06").M12
                ]
            }
        ];
    }

    private List<ProfitLossResult> GetSalesReport(List<BaoCaoLaiLo> data)
    {
        return
        [
            new ProfitLossResult
            {
                MaChiTieu = "Báo cáo kết quả bán hàng",
                ChiTieu = "Doanh thu",
                ArrAmount =
                [
                    data.First(c => c.MAKHO == "02").M1 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M1 ?? 0),
                    data.First(c => c.MAKHO == "02").M2 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M2 ?? 0),
                    data.First(c => c.MAKHO == "02").M3 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M3 ?? 0),
                    data.First(c => c.MAKHO == "02").M4 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M4 ?? 0),
                    data.First(c => c.MAKHO == "02").M5 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M5 ?? 0),
                    data.First(c => c.MAKHO == "02").M6 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M6 ?? 0),
                    data.First(c => c.MAKHO == "02").M7 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M7 ?? 0),
                    data.First(c => c.MAKHO == "02").M8 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M8 ?? 0),
                    data.First(c => c.MAKHO == "02").M9 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M9 ?? 0),
                    data.First(c => c.MAKHO == "02").M10 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M10 ?? 0),
                    data.First(c => c.MAKHO == "02").M11 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M11 ?? 0),
                    data.First(c => c.MAKHO == "02").M12 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M12 ?? 0)
                ]
            },

            new()
            {
                MaChiTieu = "Báo cáo kết quả bán hàng",
                ChiTieu = "Giá vốn",
                ArrAmount =
                [
                    data.First(c => c.MAKHO == "03").M1,
                    data.First(c => c.MAKHO == "03").M2,
                    data.First(c => c.MAKHO == "03").M3,
                    data.First(c => c.MAKHO == "03").M4,
                    data.First(c => c.MAKHO == "03").M5,
                    data.First(c => c.MAKHO == "03").M6,
                    data.First(c => c.MAKHO == "03").M7,
                    data.First(c => c.MAKHO == "03").M8,
                    data.First(c => c.MAKHO == "03").M9,
                    data.First(c => c.MAKHO == "03").M10,
                    data.First(c => c.MAKHO == "03").M11,
                    data.First(c => c.MAKHO == "03").M12
                ]
            },

            new()
            {
                MaChiTieu = "Báo cáo kết quả bán hàng",
                ChiTieu = "Lợi nhuận gộp",
                ArrAmount = [
                    data.First(c => c.MAKHO == "02").M1 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M1 ?? 0) - data.First(c => c.MAKHO == "03").M1,
                    data.First(c => c.MAKHO == "02").M2 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M2 ?? 0)-data.First(c => c.MAKHO == "03").M2,
                    data.First(c => c.MAKHO == "02").M3 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M3 ?? 0)-data.First(c => c.MAKHO == "03").M3,
                    data.First(c => c.MAKHO == "02").M4 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M4 ?? 0)-data.First(c => c.MAKHO == "03").M4,
                    data.First(c => c.MAKHO == "02").M5 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M5 ?? 0)- data.First(c => c.MAKHO == "03").M5,
                    data.First(c => c.MAKHO == "02").M6 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M6 ?? 0)-data.First(c => c.MAKHO == "03").M6,
                    data.First(c => c.MAKHO == "02").M7 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M7 ?? 0)-data.First(c => c.MAKHO == "03").M7,
                    data.First(c => c.MAKHO == "02").M8 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M8 ?? 0)-data.First(c => c.MAKHO == "03").M8,
                    data.First(c => c.MAKHO == "02").M9 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M9 ?? 0)-data.First(c => c.MAKHO == "03").M9,
                    data.First(c => c.MAKHO == "02").M10 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M10 ?? 0)-data.First(c => c.MAKHO == "03").M10,
                    data.First(c => c.MAKHO == "02").M11 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M11 ?? 0)-data.First(c => c.MAKHO == "03").M11,
                    data.First(c => c.MAKHO == "02").M12 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M12 ?? 0)-data.First(c => c.MAKHO == "03").M12
                    ],
            },
        ];
    }

    private List<ProfitLossResult> GetIndexReport(DateTime startDate, DateTime endDate)
    {
        var result = dbContext.VTDATATHUCHI.Where(c => c.LOAI != "PHIEUTHU" && c.NGAYCT >= startDate && c.NGAYCT <= endDate);
        var totalAmount = result.Sum(c => c.SOTIENVND);
        var resultData = (from c in result
                          group c by new { c.MALYDO, c.TENLYDO }
            into grp
                          select new ProfitLossResult()
                          {
                              MaChiTieu = "Báo cáo chi phí theo khoản mục",
                              ChiTieu = grp.Key.TENLYDO,
                              TotalPayment = grp.Sum(c => c.SOTIENVND),
                              Percent = Math.Round(grp.Sum(c => c.SOTIENVND) / totalAmount * 100, 1),
                              //TotalAmount = totalAmount
                          }).ToList();
        return resultData;
    }


    private LoiNhuanTheoHangResult GetSalesReportByProduct(List<LoiNhuanTheoHang> data)
    {

        var list = new LoiNhuanTheoHangResult()
        {
            LoiNhuanTheoHangs = data,
            TongLoiNhuanGop = data.Sum(c => c.LoiNhuanGop),
            TongGiaVon = data.Sum(c => c.GiaVon),
            TongDoanhThu = data.Sum(c => c.DoanhThu)
        };
        return list;
    }

    private LoiNhuanTheoHangResult GetSalesReportByProductDetail(List<LoiNhuanTheoHang> data)
    {

        var list = new LoiNhuanTheoHangResult()
        {
            LoiNhuanTheoHangs = data,
            TongDoanhThu = data.Sum(c => c.DoanhThu),
            TongGiaVon = data.Sum(c => c.GiaVon),
            TongLoiNhuanGop = data.Sum(c => c.LoiNhuanGop)
        };
        return list;

    }


    private bool ValidateCheckSum(string? checkSum, string value)
    {
        var input = $"{{{_secretKey}}}+{{{value}}}";
        var hashString = HashData.ComputeSha256(input);

        return hashString == checkSum;
    }

    private async Task<bool> InsertData(List<CreateBalanceFluctuationRequest> balanceFluctuations, CancellationToken token)
    {
        foreach (var item in balanceFluctuations)
        {
            var dataBalanceFluctuations = new DataBalanceFluctuations()
            {
                CodeUnit = item.CodeUnit,
                AmountBlance = Convert.ToDouble(item.AmountBlance),
                Amount = Convert.ToDouble(item.Amount),
                BankOfAccount = item.BankOfAccount,
                BankOfAccountReceive = item.BankOfAccountReceive,
                BankOfName = item.BankOfName,
                FullContent = item.FullContent,
                RemittanceContent = item.RemittanceContent,
                Notes = item.Notes,
                RecordDate = item.Timestamp.ToDateTime()
            };
            await dbContext.DataBalanceFluctuations.AddAsync(dataBalanceFluctuations, token).ConfigureAwait(false);
        }

        return (await dbContext.SaveChangesAsync(token)) > 0;
    }

}