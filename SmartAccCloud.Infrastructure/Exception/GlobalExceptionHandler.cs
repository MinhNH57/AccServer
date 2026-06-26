using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using SmartAccCloud.Application.Exceptions;

namespace SmartAccCloud.Infrastructure.Exception;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();
        if (exception is ValidationException fluentException)
        {
            problemDetails.Detail = "One or more validation errors occurred";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            List<string> validationErrors = new List<string>();
            foreach (var erorr in fluentException.Errors)
            {
                validationErrors.Add(erorr.ErrorMessage);
            }
            problemDetails.Extensions.Add("error", validationErrors);
        }
        else if (exception is SmartException e)
        {
            httpContext.Response.StatusCode = (int)e.StatusCode;
            problemDetails.Detail = e.Message;
            if (e.ErrorMessages.Any())
            {
                problemDetails.Extensions.Add("error", e.ErrorMessages);
            }
        }
        else
        {
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Title = "Server error";
            // problemDetails.Detail = exception.InnerException?.Message ?? exception.Message;
        }

        //Todo: Ghi log
        LogContext.PushProperty("StackTrace", exception.StackTrace);
        //var exMessage = exception.InnerException?.Message ?? exception.Message;

        logger.LogError(exception, "Exception occurred: {Message}", problemDetails.Detail);
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}