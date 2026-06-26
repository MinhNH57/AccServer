namespace AddOn.TonMyAnh.Mobile.Report.Models;

public class ProfitByGrpProductResult
{
    public double TongTrongKy { get; set; }
    public double TongTrongThang { get; set; }
    public double TongTrongNam { get; set; }
    public double TongLoiNhuan { get; set; }
    public double TongTienVon { get; set; }
    public List<ProfitByGrpProduct> Data { get; set; }
}