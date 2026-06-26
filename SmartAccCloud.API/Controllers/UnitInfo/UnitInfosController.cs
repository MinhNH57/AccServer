namespace SmartAccCloud.API.Controllers.UnitInfo;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UnitInfosController(ICrudServices crudServices) : ResultControllerBase
{
    [HttpGet]
    [Route("get-unit-info")]
    public IActionResult GetUnitInfo(int codeUnit, string codeUser)
    {
        var data = crudServices.ReadSingle<Domain.Entity.UnitInfo>(
            c => c.UnitCode == codeUnit && c.UserCode == codeUser);

        if (data is null)
        {
            return Ok(Result<Domain.Entity.UnitInfo>.Success(new Domain.Entity.UnitInfo()));
        }

        return Ok(Result<Domain.Entity.UnitInfo>.Success(data));
    }

    [HttpPost]
    [Route("create-update")]
    public IActionResult CreateOrUpdateUnitInfo(Domain.Entity.UnitInfo request)
    {
        var data = crudServices.ReadSingle<Domain.Entity.UnitInfo>(
            c => c.UnitCode == request.UnitCode && c.UserCode == request.UserCode);

        if (data is not null)
        {
            crudServices.CreateAndSave(data);
        }
        else
        {
            crudServices.UpdateAndSave(data);
        }

        return Ok(Result<bool>.Success(true));
    }
}
