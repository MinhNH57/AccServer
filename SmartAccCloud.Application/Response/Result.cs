using System.Text.Json.Serialization;

namespace SmartAccCloud.Application.Response;

public class Result<T>
{
    [JsonConstructor]
    public Result(bool isSuccess, Error? error, T? data)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    }

    // Thuộc tính chỉ đọc
    public bool IsSuccess { get; }
    public T? Data { get; }
    public Error? Error { get; }

    // Phương thức tạo kết quả thành công
    public static Result<T> Success(T? data) => new(true, Error.None, data);

    // Phương thức tạo kết quả thất bại
    public static Result<T> Failure(Error error) => new(false, error, default);
}