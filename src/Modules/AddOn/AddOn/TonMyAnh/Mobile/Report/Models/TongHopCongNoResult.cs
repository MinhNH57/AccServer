using AddOn.Data.Entities;

namespace AddOn.TonMyAnh.Mobile.Report.Models;

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