namespace AddOn.TonMyAnh.Mobile.Report.Models;

public class ProfitLossResult
{
    public string? MaChiTieu { get; set; }
    public string? ChiTieu { get; set; }
    public double TotalPayment { get; set; }
    public List<double> ArrAmount { get; set; }
    public double Percent { get; set; }
    public double TotalAmount { get; set; }
}