using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartAccCloud.Domain.Validation
{
    public class ValidationProblemDetailsResult : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var keys = context.ModelState.Keys;
            var dic = context.ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            var problemDetails = new
            {
                isSuccess = false,
                data = (object)null,
                error = new
                {
                    Code = StatusCodes.Status400BadRequest,
                    Description = dic
                }
            };

            var objectResult = new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.error.Code
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }

}
