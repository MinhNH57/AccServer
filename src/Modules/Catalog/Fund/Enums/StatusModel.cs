using System.ComponentModel;

namespace Catalog.Fund.Enums;

public enum StatusModel
{
    [Description("Thêm")]
    Add,
    [Description("Sửa")]
    Update,
    [Description("Xoá")]
    Delete,
}