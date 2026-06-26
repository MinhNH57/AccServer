using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Domain.Entity.Rules;

namespace SmartAccCloud.API.Controllers.Rules;
[Route("api/[controller]")]
[ApiController]
public class RuleAccountsController(IApplicationDbContext context) : ResultControllerBase
{
    [MapToApiVersion(1)]
    [HttpGet]
    [Route("get-by-code-user")]
    public async Task<IActionResult> GetByCodeUser(string codeUser, string codeUnit, CancellationToken token)
    {
        var lstData = await context.Database.SqlQueryRaw<RuleAccount>("EXEC CreateRuleFucnAndReport {0}, {1}, {2}",
            "RuleAccountNumberWeb", codeUser, codeUnit).ToListAsync(token);

        return Ok(Result<List<RuleAccount>>.Success(lstData));
    }

    [HttpPut]
    [Route("update-rule-account/{codeUser}")]
    public async Task<IActionResult> UpdataRuleAccount(string codeUser, List<RuleAccount> lstData, CancellationToken token)
    {
        var lstDataInDb = await context.RuleAccountNumber.Where(c => c.CodeUser == codeUser).ToListAsync(cancellationToken: token);

        foreach (var item in lstData)
        {
            var entity = lstDataInDb.FirstOrDefault(c => c.AccountSymbol == item.AccountSymbol);
            if (entity is not null)
                entity.IsAllow = item.IsAllow;
        }

        context.RuleAccountNumber.UpdateRange(lstDataInDb);
        await context.SaveChangesAsync(token);

        return Ok(Result<bool>.Success(true));
    }

}
