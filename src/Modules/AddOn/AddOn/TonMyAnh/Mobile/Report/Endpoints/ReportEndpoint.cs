using System.Globalization;
using AddOn.Data;
using AddOn.Data.Entities;
using AddOn.TonMyAnh.Mobile.CashBook;
using AddOn.TonMyAnh.Mobile.Report.Models;
using BuildingBlocks.Pagination.Version2;
using BuildingBlocks.Response;
using Carter;
using Microsoft.EntityFrameworkCore;

namespace AddOn.TonMyAnh.Mobile.Report.Endpoints;

public class ReportEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Steel");
        
        var api = vApi.MapGroup("steels").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapGet("/get-pagination", GetPagination)
            .WithName("get-pagination")
            .WithSummary("Lấy dữ liệu danh sách phiếu cần duyệt");

        api.MapGet("/get/{numberOfVoucher}", Get)
            .WithName("get");

        api.MapPost("/confirm-voucher", ConfirmVoucher)
            .WithName("confirm-voucher")
            .WithSummary("Duyệt phía");

        //api.MapPost("/send-notification", SendNotification)
        //    .WithName("send-notification");

        api.MapGet("/performance-report", GetBaoCaoLaiLo)
            .WithName("performance-report")
            .WithSummary("Báo cáo lãi lỗ");

        api.MapGet("/debt-report", GetReportDebt)
            .WithName("debt-report")
            .WithSummary("Báo cáo công nợ");

        api.MapGet("/profit-report", Getprofit)
            .WithName("profit-report");

        api.MapGet("/profit-grp-product-report", ReportProfitGrpProduct)
            .WithName("profit-grp-product-report");

        api.MapGet("/cash-book", ReportCashBook)
            .WithSummary("Báo cáo sổ quỹ tiền mặt");
    }

    private async Task<IResult> ReportCashBook([AsParameters] AddOnService service, DateTime beginDate, DateTime endDate, string parameter)
    {
        var query = new CashBookQuery(parameter, beginDate, endDate);
        var result = await service.Mediator.Send(query);
        return TypedResults.Ok(result);
    }


    private async Task<IResult> ReportProfitGrpProduct(
        [AsParameters] AddOnService service,
        [AsParameters] ReportQuery query)
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
            IdDonviOk = service.CurrentUser.CodeUnit,

            tmptblOK = ""
        };
        var data = await service.SmartDataServices
            .GetListObject<ProfitByGrpProduct>("VT_BaoCaoDoanhSoBanHangQuanTri", service.DbContext.Database.GetConnectionString()!, param);

        var result = new ProfitByGrpProductResult()
        {
            Data = data,
            TongTrongKy = data.Sum(c => c.TrongKy),
            TongTrongThang = data.Sum(c => c.TrongThang),
            TongTrongNam = data.Sum(c => c.TrongNam)
        };

        return Results.Ok(Result.Success(result));
    }

    private async Task<IResult> Getprofit(
        [AsParameters] AddOnService service,
        [AsParameters] ReportQuery query)
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
            IdDonviOk = service.CurrentUser.CodeUnit,

            tmptblOK = ""
        };
        if (query.Parameter == "Product")
        {
            var data = await service.SmartDataServices.GetListObject<LoiNhuanTheoHang>("VT_BaoCaoDoanhSoBanHangQuanTri",
            service.DbContext.Database.GetConnectionString()!, param);

            return Results.Ok(Result.Success(GetSalesReportByProduct(data)));
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
                IdDonviOk = service.CurrentUser.CodeUnit,
                tmptblOK = ""
            };
            var data = await service.SmartDataServices.GetListObject<LoiNhuanTheoHang>("VT_BaoCaoDoanhSoBanHangQuanTri",
            service.DbContext.Database.GetConnectionString()!, param1);

            return Results.Ok(Result.Success<LoiNhuanTheoHangResult>(GetSalesReportByProductDetail(data)));
        }

        return Results.Ok(Result.Success<LoiNhuanTheoHangResult>(new()));
    }

    private async Task<IResult> GetReportDebt(
        [AsParameters] AddOnService service,
        [AsParameters] ReportQuery query)
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
            var data = await service
                .SmartDataServices.GetListObject<TongHopCongNo>("VT_BangTongHopCongNo", service.DbContext.Database.GetConnectionString()!, param);

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

            return Results.Ok(Result.Success(result));

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
            var data = await service.SmartDataServices.GetListObject<TongHopCongNo>("VT_SoChiTietCongNo", service.DbContext.Database.GetConnectionString()!,
            param);
            if (data.Count < 3)
                return Results.Ok(Result.Success<TongHopCongNoResult>(new()));

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

            return Results.Ok(Result.Success(result));

        }
    }

    private async Task<IResult> GetBaoCaoLaiLo(
        [AsParameters] AddOnService service,
        [AsParameters] ReportQuery query)
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
            IdDonviOk = service.CurrentUser.CodeUnit,
            tmptblOK = ""
        };
        //var data = await dataServices.GetListObject<BaoCaoLaiLo>("VT_BaoCaoLaiLo", _tenant.ConnectionString(), param);

        if (query.Parameter == "sales")
        {
            var data = await service.SmartDataServices.GetListObject<BaoCaoLaiLo>("VT_BaoCaoLaiLo",
            service.DbContext.Database.GetConnectionString()!, param);

            var result = GetSalesReport(data);
            return Results.Ok(Result.Success(result));
        }

        if (query.Parameter == "performance")
        {
            var data = await service.SmartDataServices.GetListObject<BaoCaoLaiLo>("VT_BaoCaoLaiLo",
            service.DbContext.Database.GetConnectionString()!, param);
            var result = GetPerformanceReport(data);
            return Results.Ok(Result.Success(result));
        }

        if (query.Parameter == "index")
        {
            var result = GetIndexReport(service.DbContext, query.StartDate, query.EndDate);

            return Results.Ok(Result.Success(result));
        }
        return Results.Ok(Result.Success<List<ProfitLossResult>>(new()));
    }


    private Task SendNotification(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private async Task<IResult> ConfirmVoucher(
        [AsParameters] AddOnService service,
        List<string> listNumberVoucher)
    {
        var description = service.CurrentUser.Description;
        var strSplit = string.Join(';', listNumberVoucher);
        var param = new
            { Parameter = "ComfirmVoucher", TableName = description, KeyData = strSplit, DataPlus = "", MaUser = "", CodeUnit = 0 };
        var count = await service.SmartDataServices.ExcuteNonQuery("StoreComfirmVoucher", service.DbContext.Database.GetConnectionString()!, param);
        if (count > 0)
        {
            return Results.Ok(Result.Success(true));
        }
        return Results.Ok(Result.Failure(new("400", "Không có phiếu nào được duyệt")));
    }

    private async Task<IResult> Get(
        [AsParameters] AddOnService service,
        string numberOfVoucher)
    {
        var data = await service.DbContext.DataControlled
            .AsTracking()
            .FirstOrDefaultAsync(c => c.Id == numberOfVoucher);

        if (data is null)
            return Results.BadRequest(Result.Failure(new Error("404", "Không tìm thấy phiếu")));

        return Results.Ok(Result.Success(data));
    }

    private async Task<IResult> GetPagination(
        [AsParameters] AddOnService service,
        int pageNumber, int pageSize, string[]? searchFieldNames,
        string[]? searchValues, [AsParameters] SortModel sort,
        CancellationToken token)
    {
        var searchModels = searchFieldNames
            .Zip(searchValues, (field, value) => new SearchModel(field, value))
            .ToList();

        var query = new PaginationRequest()
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            SearchByFields = searchModels.Count > 0 ? searchModels : null,
            SortModels = string.IsNullOrEmpty(sort.SortField) ? null : [sort]
        };

        var data = await service.DbContext.DataControlled
            .AsNoTracking()
            .PaginateAsync(query, token);

        return Results.Ok(Result.Success(data));
    }

    private Task GetAll(HttpContext context)
    {
        throw new NotImplementedException();
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
                ArrAmount =
                [
                    data.First(c => c.MAKHO == "02").M1 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M1 ?? 0) - data.First(c => c.MAKHO == "03").M1,
                    data.First(c => c.MAKHO == "02").M2 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M2 ?? 0) - data.First(c => c.MAKHO == "03").M2,
                    data.First(c => c.MAKHO == "02").M3 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M3 ?? 0) - data.First(c => c.MAKHO == "03").M3,
                    data.First(c => c.MAKHO == "02").M4 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M4 ?? 0) - data.First(c => c.MAKHO == "03").M4,
                    data.First(c => c.MAKHO == "02").M5 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M5 ?? 0) - data.First(c => c.MAKHO == "03").M5,
                    data.First(c => c.MAKHO == "02").M6 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M6 ?? 0) - data.First(c => c.MAKHO == "03").M6,
                    data.First(c => c.MAKHO == "02").M7 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M7 ?? 0) - data.First(c => c.MAKHO == "03").M7,
                    data.First(c => c.MAKHO == "02").M8 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M8 ?? 0) - data.First(c => c.MAKHO == "03").M8,
                    data.First(c => c.MAKHO == "02").M9 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M9 ?? 0) - data.First(c => c.MAKHO == "03").M9,
                    data.First(c => c.MAKHO == "02").M10 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M10 ?? 0) - data.First(c => c.MAKHO == "03").M10,
                    data.First(c => c.MAKHO == "02").M11 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M11 ?? 0) - data.First(c => c.MAKHO == "03").M11,
                    data.First(c => c.MAKHO == "02").M12 + (data.FirstOrDefault(c => c.MAKHO == "04")?.M12 ?? 0) - data.First(c => c.MAKHO == "03").M12
                ],
            },
        ];
    }

    private List<ProfitLossResult> GetIndexReport(AddOnDbContext dbContext, DateTime startDate, DateTime endDate)
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
}