using Finbuckle.MultiTenant.Abstractions;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.RuleUnit;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Infrastructure.Permissions;

public class RulesUnitService(
    IApplicationDbContext context,
    IDistributedCache cache,
    IMultiTenantContextAccessor tenantContextAccessor) : IRuleUnitService
{
    private readonly string _tenantId = tenantContextAccessor.MultiTenantContext.TenantInfo!.Identifier!;
    public async Task<Result<List<UserVm>>> GetUserByUnit(int codeUnit, CancellationToken token)
    {
        var rulesUnitQuery = context.RuleUnit
            .AsNoTracking()
            .Where(c => c.CodeUnit == codeUnit && c.IsAllow);

        var userQuery = context.Users
            .AsNoTracking()
            .Where(c => c.CodeUser == "ADMIN")
            .Select(c => new UserVm()
            {
                CodeUser = c.CodeUser,
                NameUser = c.NameUser,
                CodeUnit = c.CodeUnit
            });

        var rulesUnitQueryWithCondition = (from r in rulesUnitQuery
                                           join u in context.Users.AsNoTracking() on r.CodeUser equals u.CodeUser
                                           where u.IsActive
                                           select new UserVm()
                                           {
                                               CodeUnit = u.CodeUnit,
                                               CodeUser = u.CodeUser,
                                               NameUser = u.NameUser,
                                           });

        var unionResult = await userQuery.Concat(rulesUnitQueryWithCondition).ToListAsync(token);
        return Result<List<UserVm>>.Success(unionResult);

    }

    public async Task<Result<List<RuleUnit>>> GetRuleUnitByUser(string codeUser, string codeUnit, CancellationToken token)
    {
        //var rsl = await createRuleFucnAndReportService.CreateRuleFucnAndReportAsync(new CreateRuleFucnAndReportRequest("RuleUnit", codeUser, codeUnit), token);
        var data = await context.Database
            .SqlQueryRaw<RuleUnit>("EXEC CreateRuleFucnAndReport {0}, {1}, {2}", "RuleUnitWeb", codeUser, codeUnit)
            .ToListAsync(token);

        return Result<List<RuleUnit>>.Success(data);
    }

    public async Task<Result<bool>> UpdateRuleUnit(RulesUnitUpdate request, CancellationToken token)
    {
        var lstRuleUnit = await context.RuleUnit
            .Where(c => c.CodeUser == request.CodeUser)
            .ToListAsync(cancellationToken: token);

        foreach (var item in request.LstRuleUnits!)
        {
            var rule = lstRuleUnit.FirstOrDefault(c => c.CodeUnit == item.CodeUnit);
            if (rule is not null)
                rule.IsAllow = item.IsAllow;
        }
        context.RuleUnit.UpdateRange(lstRuleUnit);
        await context.SaveChangesAsync(token);
        
        return Result<bool>.Success(true);
    }
}