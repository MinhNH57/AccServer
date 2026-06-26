using Systems.Infrastructure.Entities;
using Systems.Models.HRM;

namespace Systems.Apis;

public class RuleUnitApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Systems");
        var api = vApi.MapGroup("rule-unit").HasApiVersion(1.0);

        api.MapGet("/get-user-by-code-unit/{codeUnit}", GetUserByUnit)
            .WithName("get-user-by-code-unit")
            .WithTags("RuleUnit")
            .AllowAnonymous();

        api.MapGet("/get-by-user", GetRuleUnitByUser)
            .WithName("get-by-user")
            .WithSummary("Lấy danh sách quyền truy cập đơn vị của 1 User")
            .WithTags("RuleUnit")
            .AllowAnonymous();

        api.MapPut("/update-rule-unit", UpdateRuleUnit)
            .WithName("update-rule-unit")
            .WithSummary("Cập nhật quyền truy cập đơn vị của 1 User")
            .WithTags("RuleUnit");

    //    api.MapGet("/get-all-rules/{codeUser}", GetRuleUser)
    //.WithName("get-all-rules")
    //.WithSummary("Lấy quyền chức năng của User")
    //.WithDescription("Lấy quyền chức năng của User")
    //.WithTags("RuleUnit");

        api.MapPut("/update-rule-role" , UpdateRuleForRole)
             .WithName("update-rule-role")
            .WithSummary("Cập nhật quyền cho Role")
            .WithTags("RuleRole");

        api.MapPut("/get-rule-role-by-name-role",GetRuleRoleByNameRole)
     .WithName("get-rule-role")
    .WithSummary("Lấy ra cấu hình truy cập theo tên quyền")
    .WithTags("RuleRole");

    }

    //public static RouteGroupBuilder MapRulesUnitApiV1(this IEndpointRouteBuilder app)
    //{
    //    var vApi = app.NewVersionedApi("Systems");
    //    var api = vApi.MapGroup("api/system").HasApiVersion(1.0);

    //    api.MapGet("/get-user-by-code-unit/{codeUnit}", GetUserByUnit)
    //        .WithName("get-user-by-code-unit")
    //        //.WithSummary("")
    //        //.WithDescription("Lấy access token.")
    //        .WithTags("Systems");

    //    api.MapGet("/get-by-user", GetRuleUnitByUser)
    //        .WithName("get-by-user")
    //        //.WithSummary("")
    //        //.WithDescription("Lấy access token.")
    //        .WithTags("Systems");

    //    api.MapPut("/update-rule-unit", UpdateRuleUnit)
    //        .WithName("update-rule-unit")
    //        //.WithSummary("")
    //        //.WithDescription("Lấy access token.")
    //        .WithTags("Systems");
    //    return api;
    //}

    public static async Task<IResult> GetUserByUnit(
        [AsParameters] SystemService service,
        [FromHeader(Name = TenantConstant.TenantIdHeader)][Required]
        string tenantId,
        int codeUnit,
        CancellationToken token)
    {
        var rulesUnitQuery = service.DbContext.RuleUnit
            .AsNoTracking()
            .Where(c => c.CodeUnit == codeUnit && c.IsAllow);

        var userQuery = service.DbContext.Users
            .AsNoTracking()
            .Where(c => c.CodeUser == "ADMIN")
            .Select(c => new UserVm()
            {
                CodeUser = c.CodeUser,
                NameUser = c.NameUser,
                CodeUnit = c.CodeUnit
            });

        var rulesUnitQueryWithCondition = (from r in rulesUnitQuery
                                           join u in service.DbContext.Users.AsNoTracking() on r.CodeUser equals u.CodeUser
                                           where u.IsActive
                                           select new UserVm()
                                           {
                                               CodeUnit = u.CodeUnit,
                                               CodeUser = u.CodeUser,
                                               NameUser = u.NameUser,
                                           });

        var unionResult = await userQuery.Concat(rulesUnitQueryWithCondition).ToListAsync(token);
        return Results.Ok(Result.Success(unionResult));

    }

    public static async Task<IResult> GetRuleUnitByUser(
        [AsParameters] SystemService service,
        string codeUser, string codeUnit,
        CancellationToken token)
    {
        //var rsl = await createRuleFucnAndReportService.CreateRuleFucnAndReportAsync(new CreateRuleFucnAndReportRequest("RuleUnit", codeUser, codeUnit), token);

        var data = await service.DbContext
            .Database
            .SqlQueryRaw<RuleUnit>("EXEC CreateRuleFucnAndReport {0}, {1}, {2}", "RuleUnitWeb", codeUser, codeUnit)
            .ToListAsync(token);

        return Results.Ok(Result<List<RuleUnit>>.Success(data));
    }

    public static async Task<IResult> UpdateRuleUnit(
        [AsParameters] SystemService service,
         RulesUnitUpdate request,
        CancellationToken token)
    {
        var lstRuleUnit = await service.DbContext.RuleUnit
            .Where(c => c.CodeUser == request.CodeUser)
            .ToListAsync(cancellationToken: token);

        foreach (var item in request.LstRuleUnits!)
        {
            var rule = lstRuleUnit.FirstOrDefault(c => c.CodeUnit == item.CodeUnit);
            if (rule is not null)
                rule.IsAllow = item.IsAllow;
        }
        service.DbContext.RuleUnit.UpdateRange(lstRuleUnit);
        await service.DbContext.SaveChangesAsync(token);

        return Results.Ok(Result<bool>.Success(true));
    }

    public static async Task<IResult> UpdateRuleForRole(
        [AsParameters] SystemService service,
        RuleForRoleUpdate request,
        CancellationToken token)
    {
        // Lấy tất cả config của role này
        var configs = await service.DbContext.HRM_UsageConfiguration
            .Where(x => x.NameRole == request.NameRole)
            .ToListAsync(cancellationToken: token);

        if (configs.Count == 0)
        {
            return Results.NotFound(
                Result<bool>.Failure(new Error("NOT_FOUND", "Không tìm thấy role cần cập nhật"))
            );
        }

        // Cập nhật flag theo request
        foreach (var config in configs)
        {
            config.IsNotification = request.IsNotification;
            config.IsAssign = request.IsAssign;
            config.IsWork = request.IsWork;
            config.IsManageRequest = request.IsManageRequest;
            config.IsTimekeeping = request.IsTimekeeping;
            config.IsWhoWorking = request.IsWhoWorking;
            config.IsWeeklyWorkSchedule = request.IsWeeklyWorkSchedule;
            config.IsSchedule = request.IsSchedule;
            config.IsStaff = request.IsStaff;
            config.IsAdd = request.IsAdd;
            config.IsReport = request.IsReport;
            config.IsLicenseManage = request.IsLicenseManage;
            config.IsSalarySlip = request.IsSalarySlip;
            config.IsKPIManage = request.IsKPIManage;
            config.IsEndRow = request.IsEndRow;
            config.IsOnlineTrainding = request.IsOnlineTrainding;

            config.ModifyBy = request.ModifyBy;
            config.ModifyDate = DateTime.Now;
        }

        service.DbContext.HRM_UsageConfiguration.UpdateRange(configs);
        await service.DbContext.SaveChangesAsync(token);

        return Results.Ok(Result<bool>.Success(true));
    }

    public async Task GetRuleRoleByNameRole([AsParameters] SystemService service,
        string nameRole,
        CancellationToken token)
    {
        throw new NotImplementedException();
    }


    //private static IResult GetRuleUser(
    //[AsParameters] SystemService service,
    //string codeUser)
    //{
    //    var lstPermis = service.DbContext.Database
    //        .SqlQueryRaw<RuleUser>("EXEC CreateRuleFucnAndReport {0}, {1}, {2}", "RuleFunctionv2", codeUser, service.CurrentUser.CodeUnit).ToList();

    //    return Results.Ok(Result.Success(lstPermis));
    //}
}