namespace Voucher.Acc.Model;

public class VatPrintDto
{
    public string? TenNNT { get; set; }
    public string? MaSoThue { get; set; }

    // Chỉ tiêu - phần mua vào & chuyển kỳ
    public decimal? ct22 { get; set; }
    public decimal? ct23 { get; set; }
    public decimal? ct24 { get; set; }
    public decimal? ct23a { get; set; }
    public decimal? ct24a { get; set; }
    public decimal? ct25 { get; set; }

    // Bán ra
    public decimal? ct26 { get; set; }
    public decimal? ct29 { get; set; }
    public decimal? ct30 { get; set; }
    public decimal? ct31 { get; set; }
    public decimal? ct32 { get; set; }
    public decimal? ct33 { get; set; }
    public decimal? ct32a { get; set; }

    // Tổng hợp / phát sinh
    public decimal? ct27 { get; set; }   // computed
    public decimal? ct28 { get; set; }   // computed
    public decimal? ct34 { get; set; }   // computed
    public decimal? ct35 { get; set; }   // computed
    public decimal? ct36 { get; set; }   // computed

    // Điều chỉnh, dự án đầu tư, nghĩa vụ
    public decimal? ct37 { get; set; }
    public decimal? ct38 { get; set; }
    public decimal? ct39a { get; set; }
    public decimal? ct40a { get; set; }  // computed
    public decimal? ct40b { get; set; }
    public decimal? ct40 { get; set; }   // computed
    public decimal? ct41 { get; set; }   // computed
    public decimal? ct42 { get; set; }
    public decimal? ct43 { get; set; }   // computed

}
