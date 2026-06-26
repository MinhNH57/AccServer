using Asp.Versioning;
using BuildingBlocks.Response;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Web;

[Route(BaseApiPath)]
[ApiController]
[ApiVersion("1.0")]
public abstract class BaseController : ControllerBase
{
    protected const string BaseApiPath = "api/v{version:apiVersion}";

    protected ActionResult Ok<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return base.Ok(result);
        }
        var error = result.Error;
        if (error != null)
        {
            return error.Code switch
            {
                "400" => base.BadRequest(result),
                "401" => base.Unauthorized(result),
                "403" => base.Forbid(),
                "404" => base.NotFound(result),
                _ => base.StatusCode(500)
            };
        }

        throw new Exception(result.Error?.Description);
    }
}