namespace SmartAccCloud.Application.Response;

public class ResultAccount<TSuccess, TFailure>
{
    public bool IsSuccess { get; set; }
    public TSuccess SuccessValue { get; set; }
    public string FailureValue { get; set; }

    public TFailure Message { get; set; }
    public int? ErrorCode { get; set; }

    // Hàm dựng mặc định
    public ResultAccount(TSuccess successValue, TFailure message, string failureValue)
    {
        SuccessValue = successValue;
        Message = message;
        FailureValue = failureValue;
    }

    // Hàm dựng private để hỗ trợ việc tạo mới ResultAccount
    private ResultAccount(bool isSuccess, TSuccess successValue, TFailure message, string failureValue, int? errorCode)
    {
        IsSuccess = isSuccess;
        SuccessValue = successValue;
        Message = message;
        FailureValue = failureValue;
        ErrorCode = errorCode;
    }

    // Phương thức tạo mới một kết quả thành công
    public static ResultAccount<TSuccess, TFailure> Success(TSuccess successValue, TFailure message,
        string? failureValue, int? errorCode)
    {
        return new ResultAccount<TSuccess, TFailure>(true, successValue, message, failureValue ?? string.Empty,
            errorCode);
    }

    // Phương thức tạo mới một kết quả thất bại
    public static ResultAccount<TSuccess?, TFailure> Failure(TFailure Message, string? FailureValue, int? errorCode)
    {
        return new ResultAccount<TSuccess?, TFailure>(false, default, Message, FailureValue ?? string.Empty, errorCode);
    }
}