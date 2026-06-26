namespace Systems.Models.ExcelRequest;

public class ExcelDataParameter
{
    /// <summary>
    /// Tên tham số
    /// </summary>
    public string Parameter { get; set; } = string.Empty;

    /// <summary>
    /// Kiểu dữ liệu (nvarchar, int, decimal, bool, v.v.)
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// Mã người dùng thực hiện
    /// </summary>
    public string UserCode { get; set; } = string.Empty;

    /// <summary>
    /// Mã đơn vị (Unit)
    /// </summary>
    public int CodeUnit { get; set; }

    /// <summary>
    /// Tên bảng dữ liệu Excel
    /// </summary>
    public string TableNameDataExcel { get; set; } = string.Empty;
}
