using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace FileHandle.Extensions;

public class TemplateExcel
{
    public static void FillReport(string filename, string templatefilename, DataSet data)
    {
        FillReport(filename, templatefilename, data, ["%", "%"]);
    }

    public static void FillReport(string filename, string templatefilename, DataSet data, string[] deliminator)
    {
        if (File.Exists(filename))
            File.Delete(filename);

        using var file = new FileStream(filename, FileMode.CreateNew);
        using var temp = new FileStream(templatefilename, FileMode.Open);
        using var xls = new ExcelPackage(file, temp);
        foreach (var n in xls.Workbook.Names)
        {
            FillWorksheetData(data, n.Worksheet, n, deliminator);
        }

        foreach (var ws in xls.Workbook.Worksheets)
        {
            foreach (var n in ws.Names)
            {
                FillWorksheetData(data, ws, n, deliminator);
            }
        }

        foreach (var ws in xls.Workbook.Worksheets)
        {
            foreach (var c in ws.Cells)
            {
                var s = "" + c.Value;
                if (s.StartsWith(deliminator[0]) == false &&
                    s.EndsWith(deliminator[1]) == false)
                    continue;

                s = s.Replace(deliminator[0], "").Replace(deliminator[1], "");
                var ss = s.Split('.');
                try
                {
                    c.Value = data.Tables[ss[0]]?.Rows[0][ss[1]];
                }
                catch { }
            }
        }

        xls.Save();
    }

    private static void FillWorksheetData(DataSet data, ExcelWorksheet ws, ExcelNamedRange namedRange, string[] deliminator)
    {
        if (!data.Tables.Contains(namedRange.Name))
            return;

        var dt = data.Tables[namedRange.Name];

        int row = namedRange.Start.Row;

        var colName = new string[namedRange.Columns];
        var st = new int[namedRange.Columns];
        for (int i = 0; i < namedRange.Columns; i++)
        {
            colName[i] = ((namedRange.Value) as object[,])?[0, i].ToString()?.Replace(deliminator[0], "").Replace(deliminator[1], "") ?? string.Empty;
            if (colName[i].Contains("."))
                colName[i] = colName[i].Split('.')[1];
            st[i] = ws.Cells[row, namedRange.Start.Column + i].StyleID;
        }

        foreach (DataRow r in dt.Rows)
        {
            for (int col = 0; col < namedRange.Columns; col++)
            {
                if (dt.Columns.Contains(colName[col]))
                    ws.Cells[row, namedRange.Start.Column + col].Value = r[colName[col]];
                ws.Cells[row, namedRange.Start.Column + col].StyleID = st[col];
            }
            row++;
        }

        // extend table formatting range to all rows
        foreach (var t in ws.Tables)
        {
            var a = t.Address;
            if (namedRange.Start.Row.Between(a.Start.Row, a.End.Row) &&
                namedRange.Start.Column.Between(a.Start.Column, a.End.Column))
            {
                ExtendRows(t, dt.Rows.Count - 1);
            }
        }
    }
    public static void ExtendRows(ExcelTable excelTable, int count)
    {
        var ad = new ExcelAddress(excelTable.Address.Start.Row,
                                  excelTable.Address.Start.Column,
                                  excelTable.Address.End.Row + count,
                                  excelTable.Address.End.Column);
        //Address = ad;

    }
}

public static class IntBetween
{
    public static bool Between(this int v, int a, int b)
    {
        return v >= a && v <= b;
    }
}