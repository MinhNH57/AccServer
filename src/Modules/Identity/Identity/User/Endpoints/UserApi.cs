using Carter;
using Duende.IdentityServer.Validation;
using Identity.User.GetUserInfo;
using Identity.User.Models;
using Identity.User.ResetUserInfo;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Identity.User.Endpoints;

public class UserApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("User");

        //var api = vApi.MapGroup("api/v{api-version:apiVersion}/auths").HasApiVersion(1.0);
        var api = vApi.MapGroup("users").HasApiVersion(1.0).RequireAuthorization();

        api.MapGet("/get-all-user", GetAllUser)
            .WithName("get-all-user")
            .WithSummary("Danh sách User")
            .WithDescription("Lấy danh sách User.")
            .WithTags("User");

        api.MapGet("/get-user-by-unit", GetUserByUnit)
            .WithName("get-user-by-unit")
            .WithSummary("Danh sách User theo đơn vị đăng nhập")
            .WithDescription("Danh sách User theo đơn vị đăng nhập.")
            .WithTags("User");

        api.MapGet("/{codeUser}", GetByCode)
            .WithName("get-user")
            .WithSummary("Danh thông tin User")
            .WithDescription("Lấy danh sách User.")
            .WithTags("User");

        api.MapPost("/create-user", CreateUser)
            .WithName("create-user")
            .WithSummary("Tạo mới User")
            .WithDescription("Lấy danh sách User.")
            .WithTags("User");

        api.MapPut("/update-user", UpdateUser)
            .WithName("update-user")
            .WithSummary("Cập nhật User")
            .WithDescription("Lấy danh sách User.")
            .WithTags("User");

        api.MapGet("/get-user-token", GetUserToken)
            .WithSummary("Lấy danh sách token message của user")
            .WithTags("User");

        api.MapGet("/current-user-info", GetCurrentUserInfo)
            .WithSummary("Lấy thông tin User đang đăng nhập")
            .WithTags("User");

        api.MapGet("/detail/{codeUser}", GetDetailUser)
            .WithSummary("Danh thông tin chi tiết User cho quản trị")
            .WithTags("User");

        api.MapPost("/reset-info", ResetInfo)
            .WithName("reset-info")
            .WithSummary("Reset thông tin User")
            .WithTags("User");
        api.MapGet("/get-list-staff", GetStaffList)
            .WithName("get-staff")
            .WithSummary("Lấy danh sách nhân sự")
            .WithDescription("Lấy danh sách nhân sự.")
            .WithTags("User");

        api.MapDelete("/delete-user/{id}", DeleteUser)
            .WithSummary("Xóa user")
            .WithTags("User");
    }

    private async Task<IResult> DeleteUser([AsParameters] IdentityService service, string id)
    {
        var obj = new
        {
            Parameter = "Users",
            TableName = "",
            KeyData = id,
            DataPlus = "",
            MaUser = "",
            service.CurrentUser.CodeUnit,
        };
        var isSuccess = await service.SmartDataService.ExcuteNonQueryAsync("DeleteData", service.DbContext.Database.GetConnectionString()!, obj);

        if (isSuccess)
            return TypedResults.Ok(isSuccess);
        return TypedResults.BadRequest();
    }

    private async Task<IResult> GetCurrentUserInfo([AsParameters] IdentityService service, [AsParameters] GetUserInfoQuery query)
    {
        var resul = await service.Mediator.Send(query);
        return TypedResults.Ok(resul);
    }

    [AllowAnonymous]
    private Task GetUserToken([AsParameters] IdentityService service)
    {
        throw new NotImplementedException();
    }

    private async Task<IResult> UpdateUser(
        [AsParameters] IdentityService service,
        CreateUpdateUserRequest request,
        CancellationToken token)
    {
        var user = await service.DbContext.Users
            .FirstOrDefaultAsync(c => c.CodeUser == request.CodeUser, cancellationToken: token).ConfigureAwait(false);
        if (user is null)
            return Results.BadRequest(Result<bool>.Failure(new Error("404", "Not found User")));

        service.Mapper.Map(request, user);

        service.DbContext.Users.Update(user);

        if (request.LstRules is not null)
        {
            var listRule = await service.DbContext.RuleUser
                .Where(c => c.CodeUser == request.CodeUser)
                .ToListAsync(cancellationToken: token)
                .ConfigureAwait(false);

            foreach (var item in request.LstRules)
            {
                var existingRules = listRule.FirstOrDefault(c => c.KeyFuntion == item.KeyFuntion);
                if (existingRules is not null)
                    service.Mapper.Map(item, existingRules);
            }

            service.DbContext.RuleUser.UpdateRange(listRule);
        }

        await service.DbContext.SaveChangesAsync(token).ConfigureAwait(false);

        return Results.Ok(Result.Success(true));
    }

    private async Task<IResult> CreateUser(
        [AsParameters] IdentityService service,
        CreateUpdateUserRequest request,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool isExistsCodeUser = await service.DbContext.Users
            .AnyAsync(c => c.CodeUser == request.CodeUser.Trim(), cancellationToken: token)
            .ConfigureAwait(false);

        if (isExistsCodeUser)
            return Results.BadRequest(Result.Failure(new Error("400", "Mã user đã tồn tại")));

        var userCreate = new Users()
        {
            CodeUnit = service.CurrentUser.CodeUnit,
            CodeUser = request.CodeUser,
            NameUser = request.NameUser,
            IsActive = true,
            PassUser = !string.IsNullOrEmpty(request.Password)
                ? service.PasswordHasher.EncryptMd5(request.Password)
                : service.PasswordHasher.EncryptMd5("123"),
            Notes = request.Note
        };
        if (request.LstRules is not null)
        {
            foreach (var item in request.LstRules)
            {
                item.CodeUser = request.CodeUser;
                item.CodeUnit = request.CodeUnit ?? 100;
                item.Id = Guid.NewGuid();
            }

            var ruleCreate = request.LstRules.Adapt<List<RuleUser>>();
            service.DbContext.RuleUser.AddRange(ruleCreate);
        }

        await service.DbContext.Users.AddAsync(userCreate, token).ConfigureAwait(false);
        await service.DbContext.SaveChangesAsync(token).ConfigureAwait(false);

        return Results.Ok(Result.Success(userCreate.Id));
    }

    private async Task<IResult> GetByCode(
        [AsParameters] IdentityService service,
        string codeUser,
        CancellationToken token)
    {
        var user = await service.DbContext.Users.Where(c => c.CodeUser == codeUser)
            .ProjectToType<UserVm>()
            .FirstOrDefaultAsync(cancellationToken: token);

        if (user is null)
            return Results.NotFound(Result<UserVm>.Failure(new Error("404", "Không tìm thấy user")));

        return Results.Ok(Result<UserVm>.Success(user));
    }
    private async Task<IResult> GetStaffList(
        [AsParameters] IdentityService service,
        CancellationToken token)
    {
        var lstObject = await service.DbContext.CatalogObject.Where(c => c.IsStaff == true && c.IsActive == true).ToListAsync(cancellationToken: token);

        return Results.Ok(Result.Success(lstObject));
    }

    [AllowAnonymous]
    private async Task<IResult> GetAllUser(
        [AsParameters] IdentityService service,
        CancellationToken token)
    {

        string query = "Select * from View_UsersWithCataObj";
        var lst = await service.SmartDataService.GetListObject<object>(strQuery: query, strConnect: service.DbContext.Database.GetConnectionString()!, token);

        //var lst = await service.DbContext.Users
        //    .ProjectToType<UserVm>()
        //    .ToListAsync(cancellationToken: token);

        return Results.Ok(Result.Success(lst));
    }

    private async Task<IResult> GetUserByUnit([AsParameters] IdentityService service, CancellationToken token)
    {
        List<UserVm> listUser = new();
        if (service.CurrentUser.CodeUnit == 9999)
        {
            listUser = await service.DbContext.Users.AsNoTracking()
                .ProjectToType<UserVm>()
                .ToListAsync(cancellationToken: token);
            return Results.Ok(Result.Success(listUser));
        }
        listUser = await service.DbContext.Users.AsNoTracking()
            .Where(c => c.CodeUnit == service.CurrentUser.CodeUnit)
            .ProjectToType<UserVm>()
            .ToListAsync(cancellationToken: token);

        return Results.Ok(Result.Success(listUser));
    }
    private async Task<IResult> GetDetailUser([AsParameters] IdentityService service, string codeUser, CancellationToken clt)
    {
        var userInfoStore = new GetUserInfoStoreProduce("DetailUser", codeUser, 100);

        var user = await service.SmartDataService.GetSingleObject<UserDetails>(userInfoStore.StoredProcedureName,
        service.DbContext.Database.GetConnectionString()!, userInfoStore.Parameters);

        return TypedResults.Ok(user);
    }
    private async Task<IResult> ResetInfo([AsParameters] IdentityService service, TypeReset request)
    {
        var command = new ResetUserInfoCommand(request);
        var result = await service.Mediator.Send(command);
        return TypedResults.Ok(result);
    }


}