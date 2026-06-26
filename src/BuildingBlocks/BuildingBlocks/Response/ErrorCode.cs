namespace BuildingBlocks.Response;
public static class ErrorCode  
{
    // --- Lỗi Client (4xx) ---

    /// <summary>
    /// Yêu cầu không hợp lệ. Sử dụng khi dữ liệu đầu vào thiếu hoặc sai định dạng.
    /// </summary>
    public const string BadRequest = "400";

    /// <summary>
    /// Không được phép truy cập. Sử dụng khi người dùng chưa xác thực (chưa đăng nhập).
    /// </summary>
    public const string Unauthorized = "401";

    /// <summary>
    /// Bị cấm truy cập. Sử dụng khi người dùng đã xác thực nhưng không có quyền thực hiện hành động này.
    /// </summary>
    public const string Forbidden = "403";

    /// <summary>
    /// Không tìm thấy tài nguyên. Sử dụng khi tài nguyên được yêu cầu không tồn tại (ví dụ: truy vấn ID không có trong DB).
    /// </summary>
    public const string NotFound = "404";

    /// <summary>
    /// Xung đột. Sử dụng khi yêu cầu tạo mới tài nguyên có ID/mã định danh đã tồn tại.
    /// </summary>
    public const string Conflict = "409";

    /// <summary>
    /// Thực thể không thể xử lý. Sử dụng khi dữ liệu đầu vào hợp lệ về mặt cú pháp nhưng không hợp lệ về mặt ngữ nghĩa (ví dụ: số lượng sản phẩm âm).
    /// </summary>
    public const string UnprocessableEntity = "422";


    // --- Lỗi Server (5xx) ---

    /// <summary>
    /// Lỗi máy chủ nội bộ. Sử dụng cho các lỗi không lường trước xảy ra trên server (ngoại lệ, lỗi kết nối DB, v.v.).
    /// </summary>
    public const string InternalServerError = "500";
}