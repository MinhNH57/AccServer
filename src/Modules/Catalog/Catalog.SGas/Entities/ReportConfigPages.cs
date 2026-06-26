using System.ComponentModel.DataAnnotations;

namespace Catalog.SGas.Entities;
public class ReportConfigPages
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public Guid IdMenu { get; set; } = Guid.NewGuid();
    [Required(ErrorMessage = "Không được để trống Href web")]
    public string? HrefWeb { get; set; }
    [Required(ErrorMessage = "Không được để trống Menu Name")]
    public string? ReportMenuName { get; set; }
    [Required(ErrorMessage = "Không được để trống tên báo cáo")]
    public string? NameReport { get; set; }
    public string? ReportFileName { get; set; }
    public string? ExcelFileName { get; set; }
    [Required(ErrorMessage = "Không được để trống tên store")]
    public string? StoreName { get; set; }
    [Required(ErrorMessage = "Không được để trống tham số")]
    public string? ParamaterHead { get; set; }
    public string? TableNameHead { get; set; }
    public string? TableNameContent { get; set; }
    public bool ShowFilterDate { get; set; }
    public bool ShowReportSelected { get; set; }
    public bool ShowAccountNumber { get; set; }
    public bool ShowObjectSelected { get; set; }
    public bool ShowWarehouseSelected { get; set; }
    public bool IsDetail { get; set; }
    public string? StoreNameContent { get; set; }
    public string? ParamaterContent { get; set; }
    [Required(ErrorMessage = "Không được để trống cấu hình quản trị")]
    public string? CmsHead { get; set; }
    public string? CmsContent { get; set; }
}
