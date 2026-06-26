using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.ToKhai01;

public class VatFormDocument:IDocument
{
    private readonly VatPrintDto _m;

    public VatFormDocument(VatPrintDto model)
    {
        _m = model ?? new VatPrintDto();
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(20);
            page.DefaultTextStyle(x => x.FontSize(11));
            page.Content().Column(col =>
            {
                // Header
                col.Item().AlignCenter().Text("CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM").SemiBold();
                col.Item().AlignCenter().Text("Độc lập - Tự do - Hạnh phúc").Italic().FontSize(10);
                col.Item().PaddingTop(6);

                // Title
                col.Item().AlignCenter().Text("TỜ KHAI THUẾ GIÁ TRỊ GIA TĂNG (GTGT)").Bold().FontSize(16);
                col.Item().AlignCenter().Text("Mẫu số: 01/GTGT").FontSize(10);
                col.Item().PaddingTop(8);

                // Meta
                col.Item().Text(t =>
                {
                    t.Span("[04] Tên người nộp thuế: ").SemiBold();
                    t.Span(_m.TenNNT ?? string.Empty);
                });
                col.Item().Text(t =>
                {
                    t.Span("[05] Mã số thuế: ").SemiBold();
                    t.Span(_m.MaSoThue ?? string.Empty);
                });
                col.Item().Text("Đơn vị tiền: Đồng Việt Nam").Italic().FontSize(10);
                col.Item().PaddingTop(6);

                // Table
                col.Item().Element(BuildVatTable);
            });

            page.Footer().AlignRight().Text(txt =>
            {
                txt.Span("Trang ");
                txt.CurrentPageNumber();
                txt.Span("/");
                txt.TotalPages();
            });

        });
    }

    private void BuildVatTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(c =>
            {
                c.RelativeColumn(1);  // Mục (B/1/2...)
                c.RelativeColumn(8);  // Mô tả
                c.RelativeColumn(1);  // [mã]
                c.RelativeColumn(2);  // Giá trị
            });

            void Row(string col1, string col2, string code, decimal? val, bool bold = false, bool italic = false)
            {
                var style = TextStyle.Default;
                if (bold) style = style.Bold();
                if (italic) style = style.Italic();

                table.Cell().Border(1).Padding(4).AlignCenter().Text(col1).Style(style);
                table.Cell().Border(1).Padding(4).Text(col2).Style(style);
                table.Cell().Border(1).Padding(4).AlignCenter().Text(code).Style(style);
                table.Cell().Border(1).Padding(4).AlignRight().Text(Format(val)).Style(style);
            }

            // --- B
            Row("B", "Thuế GTGT còn được khấu trừ kỳ trước chuyển sang", "[22]", _m.ct22, bold: true);

            // --- C
            Row("C", "Kê khai thuế GTGT phải nộp ngân sách nhà nước", "", null, bold: true);

            // --- I
            Row("I", "Hàng hóa, dịch vụ mua vào trong kỳ", "", null, bold: true);
            Row("1", "Giá trị và thuế GTGT của hàng hóa, dịch vụ mua vào", "[23]", _m.ct23);
            Row("", "Trong đó: hàng hóa, dịch vụ nhập khẩu", "[23a]", _m.ct23a, italic: true);
            Row("", "", "[24a]", _m.ct24a, italic: true); // Đặt riêng một dòng nếu cần
            Row("", "Thuế GTGT của hàng hóa, dịch vụ mua vào (thuế)", "[24]", _m.ct24);
            Row("2", "Thuế GTGT hàng hóa, dịch vụ mua vào được khấu trừ kỳ này", "[25]", _m.ct25);

            // --- II
            Row("II", "Hàng hóa, dịch vụ bán ra trong kỳ", "", null, bold: true);
            Row("1", "Hàng hóa, dịch vụ bán ra không chịu thuế GTGT", "[26]", _m.ct26, bold: true);
            Row("2", "HHDV bán ra chịu thuế GTGT (tổng doanh thu) ([27]=[29]+[30]+[32]+[32a]); Thuế GTGT ([28]=[31]+[33])", "", null, bold: true);
            Row("", "Chỉ tiêu tổng doanh thu HHDV bán ra chịu thuế GTGT", "[27]", _m.ct27);
            Row("", "Chỉ tiêu thuế GTGT của HHDV bán ra chịu thuế", "[28]", _m.ct28);
            Row("a", "HHDV bán ra chịu thuế suất 0%", "[29]", _m.ct29, italic: true);
            Row("b", "HHDV bán ra chịu thuế suất 5% (doanh thu)", "[30]", _m.ct30, italic: true);
            Row("", "HHDV bán ra chịu thuế suất 5% (thuế)", "[31]", _m.ct31, italic: true);
            Row("c", "HHDV bán ra chịu thuế suất 10% (doanh thu)", "[32]", _m.ct32, italic: true);
            Row("", "HHDV bán ra chịu thuế suất 10% (thuế)", "[33]", _m.ct33, italic: true);
            Row("d", "Hàng hóa, dịch vụ bán ra không tính thuế", "[32a]", _m.ct32a, italic: true);

            // --- 3
            Row("3", "Tổng doanh thu và thuế GTGT HHDV bán ra ([34]=[26]+[27]; [35]=[28])", "", null, bold: true);
            Row("", "Tổng doanh thu", "[34]", _m.ct34);
            Row("", "Tổng thuế GTGT", "[35]", _m.ct35);

            // --- III
            Row("III", "Thuế GTGT phát sinh trong kỳ ([36]=[35]-[25])", "", null, bold: true);
            Row("", "Thuế GTGT phát sinh trong kỳ", "[36]", _m.ct36);

            // --- IV
            Row("IV", "Điều chỉnh tăng, giảm thuế GTGT còn được khấu trừ các kỳ trước", "", null, bold: true);
            Row("1", "Điều chỉnh giảm", "[37]", _m.ct37);
            Row("2", "Điều chỉnh tăng", "[38]", _m.ct38);

            // --- V
            Row("V", "Thuế GTGT nhận bàn giao được khấu trừ trong kỳ", "", null, bold: true);
            Row("", "Thuế GTGT nhận bàn giao", "[39a]", _m.ct39a, bold: true);

            // --- VI
            Row("VI", "Xác định nghĩa vụ thuế GTGT phải nộp trong kỳ", "", null, bold: true);
            Row("1", "Thuế GTGT phải nộp của hoạt động SXKD trong kỳ {[40a]=([36]-[22]+[37]-[38]-[39a]) ≥ 0}", "[40a]", _m.ct40a);
            Row("2", "Thuế GTGT mua vào của dự án đầu tư được bù trừ (≤ [40a])", "[40b]", _m.ct40b);
            Row("3", "Thuế GTGT mua vào của dự án đầu tư được bù trừ với thuế GTGT còn phải nộp của HĐ SXKD cùng kỳ ([40b]≤[40a])", "[40]", _m.ct40);
            Row("4", "Thuế GTGT chưa khấu trừ hết kỳ này {[41]=([36]-[22]+[37]-[38]-[39a]) ≤ 0}", "[41]", _m.ct41, bold: true);
            Row("4.1", "Thuế GTGT đề nghị hoàn ([42] ≤ [41])", "[42]", _m.ct42, italic: true);
            Row("4.2", "Thuế GTGT còn được khấu trừ chuyển kỳ sau ([43]=[41]-[42])", "[43]", _m.ct43, italic: true);
        });
    }

    private static string Format(decimal? v)
    {
        var n = v ?? 0m;
        return n.ToString("#,0");
    }
}
