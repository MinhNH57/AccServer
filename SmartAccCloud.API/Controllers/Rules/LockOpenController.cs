using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Data;
using Lock = SmartAccCloud.Domain.Entity.Rules.Lock;


namespace SmartAccCloud.API.Controllers.Rules;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LockOpenController(ICrudServices crudServices, IApplicationDbContext context) : ResultControllerBase
{
    [HttpGet]
    [Route("{codeUnit}")]
    [HasPermission(CustomAction.AllowView, Resource.LockOpen)]
    public async Task<IActionResult> GetLockOpen(int codeUnit)
    {
        var lstData = await crudServices.ReadManyNoTracked<Lock>()
            .Where(c => c.LockType == "LOCK" && c.CodeUnit == codeUnit)
            .OrderBy(c => c.CodeMonth)
            .ToListAsync();

        return Ok(Result<List<Lock>>.Success(lstData));
    }

    [HttpPut]
    [Route("update-lock-open/{codeUnit}")]
    [HasPermission(CustomAction.AllowEdit, Resource.LockOpen)]
    public async Task<IActionResult> UpdateLockOpen(int codeUnit, List<Lock> request, CancellationToken token)
    {
        var lstData = await crudServices.ReadManyNoTracked<Lock>()
            .Where(c => c.LockType == request[0].LockType && c.CodeUnit == codeUnit)
            .OrderBy(c => c.CodeMonth)
            .ToListAsync();

        foreach (var item in request)
        {
            var entity = lstData.FirstOrDefault(c => c.CodeMonth == item.CodeMonth);
            if (entity is not null)
                entity.Locks = item.Locks;
        }

        context.Locks.UpdateRange(lstData);
        await context.SaveChangesAsync(token);

        return Ok(Result<bool>.Success(true));
    }

    [HttpGet]
    [Route("get-lock-open-unit/{codeUnit}")]
    public async Task<IActionResult> GetLockOpenUnit(int codeUnit)
    {
        var lstData = await crudServices.ReadManyNoTracked<Lock>()
            .Where(c => c.LockType == "LOCKU" && c.CodeUnit == codeUnit)
            .OrderBy(c => c.CodeMonth)
            .ToListAsync();

        return Ok(Result<List<Lock>>.Success(lstData));
    }

}
