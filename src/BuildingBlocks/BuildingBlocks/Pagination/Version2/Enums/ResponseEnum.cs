using System.ComponentModel;

namespace BuildingBlocks.Pagination.Version2.Enums
{
    public class ResponseEnum
    {
        public enum ResponseStatus
        {
            [Description("Thành công")] Success = 200,

            [Description("Thất bại")] Error = 400,

            [Description("Trùng key")] Duplicate = 422,

            [Description("Không tồn tại")] DoesNotExist = 404
        }
    }
}