using System.Net;

namespace SmartAccCloud.Application.Exceptions;

public class SmartException : Exception
{
    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public SmartException(string message, IEnumerable<string> errors,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public SmartException(string message) : base(message)
    {
        ErrorMessages = new List<string>();
    }
}