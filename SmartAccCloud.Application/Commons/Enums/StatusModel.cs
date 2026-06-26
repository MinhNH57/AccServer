using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAccCloud.Application.Commons.Enums
{
    public enum StatusModel
    {
        [Description("Thêm")]
        Add,
        [Description("Sửa")]
        Update,
        [Description("Xoá")]
        Delete,
    }
}
