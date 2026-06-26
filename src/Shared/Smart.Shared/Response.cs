namespace Smart.Shared;

public class Response<T> : Response
{
    public T? Data { get; set; }
}

public class Response
{
    public bool Success { get; set; }
    public int Code { get; set; }
    public int SubCode { get; set; }
    public string? UserMessage { get; set; }
    public string? SystemMessage { get; set; }
    public List<string> ErrorsMessage { get; set; } = [];
    public DateTime ServerTime { get; set; }
    public string? RequestTime { get; set; }
    public double TotalTime { get; set; }
    public string? ExceptionID { get; set; }
}