using AddOn.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using Microsoft.EntityFrameworkCore;

namespace AddOn.TonMyAnh.Mobile.CashBook;



public class CashBookQueryHandler(SmartDataServices smartDataServices, AddOnDbContext dbContext) : IQueryHandler<CashBookQuery, Result>
{
    public async Task<Result> Handle(CashBookQuery query, CancellationToken cancellationToken)
    {
        var param = new
        {
            hien = 0,
            ThamSo = query.Parameter,
            PathImages = "",
            date1 = query.BeginDate.ToString("MM-dd-yyyy"),
            date2 = query.EndDate.ToString("MM-dd-yyyy"),
            MaDonVi = "",
            tmptblOK = ""
        };
        var data = await smartDataServices.GetListObject<CashBookStoreReslt>("VT_SoChiTietCongNo", dbContext.Database.GetConnectionString()!, param);

        return Result.Success(MapData(data));
    }
    private CashBookResult MapData(List<CashBookStoreReslt> data)
    {
        CashBookSummary cashBookSummary = new();
        List<CashBookList> cashBookLists = new();
        foreach (var item in data)
        {
            if (item.STT == 1) cashBookSummary.TonDauKy = item.SOTIENCON;
            else if (item.STT == item.MAXSTT) cashBookSummary.TonCuoiKy = item.SOTIENCON;
            else if (item.STT == item.MAXSTT - 1)
            {
                cashBookSummary.Thu = item.SOTIENNO;
                cashBookSummary.Chi = item.SOTIENCO;
            }
            else
            {
                var cashBookList = new CashBookList
                {
                    ID = item.ID,
                    SoChungTu = item.SOCHUNGTU,
                    NgayCt = item.NGAYCT,
                    DienGiai = item.DIENGIAI,
                    SoTienNo = item.SOTIENNO,
                    SoTienCo = item.SOTIENCO,
                    SoTienCon = item.SOTIENCON,
                    LoaiPhieu = item.LOAI
                };
              
                cashBookLists.Add(cashBookList);
            }
        }
        return new(cashBookSummary, cashBookLists);
    }
}