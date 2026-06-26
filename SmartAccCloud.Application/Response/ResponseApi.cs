namespace SmartAccCloud.Application.Response;

public class ResponseApi<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public Error? Error { get; set; }
}