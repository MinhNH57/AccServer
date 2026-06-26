using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.API.Controllers.Identites;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUserService userService) : ResultControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [Route("get-all-user")]
    public async Task<IActionResult> GetListUser(CancellationToken token)
    {
        var result = await userService.GetUserVm(token);
        return Ok(result);
    }

    [HttpGet("{codeUser}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogUsers)]
    public async Task<IActionResult> GetByCode(string codeUser, CancellationToken token)
    {
        var rsl = await userService.GetByCode(codeUser, token);

        return Ok(rsl);
    }

    [HttpPost]
    [Route("create-user")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogUsers)]
    public async Task<IActionResult> CreateUser(CreateUpdateUserRequest request, CancellationToken token)
    {
        var result = await userService.CreateUser(request, token);
        return Ok(result);
    }

    [HttpPut]
    [Route("update-user")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogUsers)]
    public async Task<IActionResult> UpdateUser(CreateUpdateUserRequest request, CancellationToken token)
    {
        var rsl = await userService.UpdateUser(request, token);

        return Ok(rsl);
    }

}
