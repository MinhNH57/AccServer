using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        (string Detail, string Title, int StatusCode) detail = exception switch
        {
            InternalServerException => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),
            BadRequestException => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),
            ValidationException => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),
            NotFoundException => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound),
            _ => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),
        };

        var problemDetails = new ProblemDetails()
        {
            Title = detail.Title,
            Detail = detail.Detail,
            Status = detail.StatusCode,
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        if (exception is ValidationException validationException)
            problemDetails.Extensions.Add("validationErrors", string.Join("\n", validationException.Errors.Select(c => c.ErrorMessage)));
        // logger.LogError(exception, "Exception occurred: {Message}", problemDetails.Detail);
        //if (exception is ValidationException validationException)

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}