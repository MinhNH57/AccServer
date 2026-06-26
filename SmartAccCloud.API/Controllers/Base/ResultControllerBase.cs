namespace SmartAccCloud.API.Controllers.Base;

//[Route("api/[controller]")]
//[ApiController]
public class ResultControllerBase : ControllerBase
{
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
                "401" => base.Unauthorized(error),
                "403" => base.Forbid(),
                "404" => base.NotFound(result),
                _ => base.StatusCode(500),
            };
        }

        throw new Exception(result.Error?.Description);
    }

}
