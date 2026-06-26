using System.Text.Json.Serialization;

namespace BuildingBlocks.Response;

public class Result
{
    [JsonConstructor]
    public Result(bool isSuccess, Error? error)
    {
        //if (isSuccess && error != Error.None ||
        //    !isSuccess && error == Error.None)
        //{
        //    throw new ArgumentException("Invalid error", nameof(error));
        //}

        IsSuccess = isSuccess;
        Error = error;
    }

    // Thuộc tính chỉ đọc
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    // Phương thức tạo kết quả thành công
    public static Result<T> Success<T>(T? data, Error? error = null) => new(true, data, error);

    // Phương thức tạo kết quả thất bại
    public static Result<T> Failure<T>(Error error, T? data = default) => new(false, default, error);
}

public class Result<T> : Result
{
    private readonly T? _data;
    [JsonConstructor]
    public Result(bool isSuccess, T? data, Error? error) : base(isSuccess, error)
    {
        _data = data;
    }

    public T? Data => _data;

    //public static implicit operator Result<T>(T? value) => Create(value);
}