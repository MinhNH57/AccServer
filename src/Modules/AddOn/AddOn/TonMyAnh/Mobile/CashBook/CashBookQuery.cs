using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace AddOn.TonMyAnh.Mobile.CashBook;

public class CashBookStoreReslt
{
    public int ID { get; set; }
    public int STT { get; set; }
    public int MAXSTT { get; set; }
    public DateTime? NGAYCT { get; set; }
    public string? SOCHUNGTU { get; set; }
    public string? LOAI { get; set; }
    public string? DIENGIAI { get; set; }
    public decimal SOTIENNO { get; set; }
    public decimal SOTIENCO { get; set; }
    public decimal SOTIENCON { get; set; }
}

public class CashBookList
{
    public int ID { get; set; }
    public DateTime? NgayCt { get; set; }
    public string? SoChungTu { get; set; }
    public string? LoaiPhieu { get; set; }
    public string? DienGiai { get; set; }
    public decimal SoTienNo { get; set; }
    public decimal SoTienCo { get; set; }
    public decimal SoTienCon { get; set; }
}

public class CashBookSummary
{
    public decimal TonDauKy { get; set; }
    public decimal TonCuoiKy { get; set; }
    public decimal Thu { get; set; }
    public decimal Chi { get; set; }
}

public record CashBookResult(CashBookSummary CashBookSummary, List<CashBookList> CashBookLists);

public record CashBookQuery(string Parameter, DateTime BeginDate, DateTime EndDate) : IQuery<Result>;