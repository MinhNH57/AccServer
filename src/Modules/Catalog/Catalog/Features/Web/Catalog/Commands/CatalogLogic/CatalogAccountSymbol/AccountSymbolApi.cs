using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Catalog.Base;
using MapsterMapper;
using Serilog;

namespace Catalog.Features.Web.Catalog.Commands.CatalogLogic.CatalogAccountSymbol;

public class AccountSymbolApi : ICarterModule
{ 
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");
        var api = vApi.MapGroup("catalog/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPost("/create-account-symbol", CreateAccountSymbolNew)
            .WithName("create-account-symbol")
            .WithSummary("Tạo danh mục tài khoản")
            .WithDescription("Tạo mới danh mục tài khoản")
            .WithTags("Catalogs");

        api.MapPost("/update-account-symbol", UpdateAccountSymbolNew)
            .WithName("update-account-symbol")
            .WithSummary("Cập nhật danh mục tài khoản")
            .WithDescription("Cập nhật danh mục tài khoản")
            .WithTags("Catalogs");
    }

    /// <summary>
    /// Hàm này để tạo danh mục tài khoản
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="currentUser"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<IResult> CreateAccountSymbolNew(
    [AsParameters] CatalogService service,
    Base.Entities.CatalogAccountSymbol request,
    ICurrentUser currentUser,
    CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        // 1) Validate mã trùng
        bool exist = await service.DbContext.CatalogAccountSymbol
            .AsNoTracking()
            .AnyAsync(x => x.AccountSymbol == request.AccountSymbol, token);
        if (exist)
            return Results.BadRequest(Result.Failure(new Error("400", "Mã tài khoản đã tồn tại.")));

        // 2) Validate parent (nếu có)
        if (!string.IsNullOrWhiteSpace(request.AccountParent))
        {
            if (request.AccountParent == request.AccountSymbol)
                return Results.BadRequest(Result.Failure(new Error("400", "AccountParent không được trùng AccountSymbol.")));

            bool parentExists = await service.DbContext.CatalogAccountSymbol
                .AsNoTracking()
                .AnyAsync(x => x.AccountSymbol == request.AccountParent, token);

            if (!parentExists)
                return Results.BadRequest(Result.Failure(new Error("400", "Tài khoản cha không tồn tại. -- AccountParent")));
        } 
        await service.DbContext.CatalogAccountSymbol.AddAsync(request, token);
        if (await service.DbContext.SaveChangesAsync(token) > 0)
        {
            Log.Information($"[Catalog] User: {currentUser.CodeUser} created Account: {request.AccountSymbol}");
            return Results.Ok(Result.Success(true));
        }

        //Log.Error(ex, "[Catalog] CreateAccountSymbol failed for {Acc}", request.AccountSymbol);
        return Results.BadRequest(Result.Failure(new Error("400", "Có lỗi khi cất giữ.")));
    }


    public static async Task<IResult> UpdateAccountSymbolNew(
        [AsParameters] CatalogService service,
        IMapper mapper,
        Base.Entities.CatalogAccountSymbol request,
        ICurrentUser currentUser,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        // 1) Lấy bản ghi cũ
        var accountUpdate = await service.DbContext.CatalogAccountSymbol
            .FirstOrDefaultAsync(x => x.AccountSymbol == request.AccountSymbol, token);

        if (accountUpdate is null)
            return Results.BadRequest(Result.Failure(new Error("404", "Tài khoản không tồn tại.")));

        accountUpdate = mapper.Map(request, accountUpdate);
        service.DbContext.CatalogAccountSymbol.Update(accountUpdate); 
        if (await service.DbContext.SaveChangesAsync(token) > 0)
        {
            Log.Information($"[Catalog] User: {currentUser.CodeUser} updated Account: {request.AccountSymbol}");
            return Results.Ok(Result.Success(true));
        }
        return Results.BadRequest(Result.Failure(new Error("404", "Có lỗi khi cập nhật thông tin tài khoản.")));
    }

    [Obsolete("Hiện tại đã dùng trigger [UpdateAccountNumberParent] nên không cần dùng hàm này nữa. Dùng UpdateAccountSymbolNew")]
    public static async Task<IResult> UpdateAccountSymbol(
    [AsParameters] CatalogService service,
    IMapper mapper,
    Base.Entities.CatalogAccountSymbol request,
    ICurrentUser currentUser,
    CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        // 1) Lấy bản ghi cũ
        var accountUpdate = await service.DbContext.CatalogAccountSymbol
            .FirstOrDefaultAsync(x => x.AccountSymbol == request.AccountSymbol, token);

        if (accountUpdate is null)
            return Results.BadRequest(Result.Failure(new Error("404", "Tài khoản không tồn tại.")));

        // 2) Nếu đổi mã tài khoản, kiểm tra trùng
        if (!string.Equals(accountUpdate.AccountSymbol, request.AccountSymbol, StringComparison.OrdinalIgnoreCase))
        {
            bool symbolExist = await service.DbContext.CatalogAccountSymbol
                .AsNoTracking()
                .AnyAsync(x => x.AccountSymbol == request.AccountSymbol, token);

            if (symbolExist)
                return Results.BadRequest(Result.Failure(new Error("400", "Mã tài khoản đã tồn tại.")));
        }

        // 3) Nếu đổi cha, validate cha mới
        string? oldParent = accountUpdate.AccountParent;
        string? newParent = request.AccountParent;

        if (!string.Equals(oldParent, newParent, StringComparison.OrdinalIgnoreCase))
        {
            if (!string.IsNullOrWhiteSpace(newParent))
            {
                if (newParent == request.AccountSymbol)
                    return Results.BadRequest(Result.Failure(new Error("400", "AccountParent không được trùng AccountSymbol.")));

                bool parentExists = await service.DbContext.CatalogAccountSymbol
                    .AsNoTracking()
                    .AnyAsync(x => x.AccountSymbol == newParent, token);

                if (!parentExists)
                    return Results.BadRequest(Result.Failure(new Error("400", "Tài khoản cha không tồn tại.")));
            }
        }

        await using var tx = await service.DbContext.Database.BeginTransactionAsync(token);
        try
        {
            // 4) Map dữ liệu mới vào entity cũ
            accountUpdate = mapper.Map(request, accountUpdate);
            service.DbContext.CatalogAccountSymbol.Update(accountUpdate);
            await service.DbContext.SaveChangesAsync(token);

            // 5) Nếu đổi cha → bật IsParent cha mới và tắt IsParent cha cũ nếu hết con
            if (!string.Equals(oldParent, newParent, StringComparison.OrdinalIgnoreCase))
            {
                // Cha mới
                if (!string.IsNullOrWhiteSpace(newParent))
                {
                    await service.DbContext.CatalogAccountSymbol
                        .Where(p => p.AccountSymbol == newParent && p.IsParent == false)
                        .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsParent, x => true), token);
                }

                // Cha cũ: nếu không còn con thì tắt
                if (!string.IsNullOrWhiteSpace(oldParent))
                {
                    bool hasChild = await service.DbContext.CatalogAccountSymbol
                        .AsNoTracking()
                        .AnyAsync(c => c.AccountParent == oldParent, token);

                    if (!hasChild)
                    {
                        await service.DbContext.CatalogAccountSymbol
                            .Where(p => p.AccountSymbol == oldParent)
                            .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsParent, x => false), token);
                    }
                }
            }

            // 6) Nếu dùng rule bật cha → tắt con (nếu không dùng trigger)
            if (accountUpdate.IsActive && !string.IsNullOrEmpty(accountUpdate.AccountSymbol))
            {
                await service.DbContext.CatalogAccountSymbol
                    .Where(c => c.AccountParent == accountUpdate.AccountSymbol)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsActive, x => false), token);
            }

            await tx.CommitAsync(token);

            Log.Information($"[Catalog] User: {currentUser.CodeUser} updated Account: {request.AccountSymbol}");
            return Results.Ok(Result.Success(true));
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync(token);
            Log.Error(ex, "[Catalog] UpdateAccountSymbol failed for {Acc}", request.AccountSymbol);
            return Results.BadRequest(Result.Failure(new Error("400", "Có lỗi khi cập nhật.")));
        }
    }

    /// <summary>
    /// Hàm này để tạo danh mục tài khoản
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="currentUser"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [Obsolete("Hiện tại đã dùng trigger [CreateAccountNumberParent] nên không cần dùng hàm này nữa. Dùng CreateAccountSymbolNew")]
    public static async Task<IResult> CreateAccountSymbol(
   [AsParameters] CatalogService service, Base.Entities.CatalogAccountSymbol request, ICurrentUser currentUser, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        // 1) Validate mã trùng
        bool exist = await service.DbContext.CatalogAccountSymbol
            .AsNoTracking()
            .AnyAsync(x => x.AccountSymbol == request.AccountSymbol, token);
        if (exist)
            return Results.BadRequest(Result.Failure(new Error("400", "Mã tài khoản đã tồn tại.")));

        // 2) Validate parent (nếu có)
        if (!string.IsNullOrWhiteSpace(request.AccountParent))
        {
            if (request.AccountParent == request.AccountSymbol)
                return Results.BadRequest(Result.Failure(new Error("400", "AccountParent không được trùng AccountSymbol.")));

            bool parentExists = await service.DbContext.CatalogAccountSymbol
                .AsNoTracking()
                .AnyAsync(x => x.AccountSymbol == request.AccountParent, token);

            if (!parentExists)
                return Results.BadRequest(Result.Failure(new Error("400", "Tài khoản cha không tồn tại.")));
        }

        // 4) Transaction để đảm bảo nhất quán
        await using var tx = await service.DbContext.Database.BeginTransactionAsync(token);
        try
        {
            await service.DbContext.CatalogAccountSymbol.AddAsync(request, token);
            await service.DbContext.SaveChangesAsync(token);

            // 5) Bật IsParent = true cho cha nếu có
            if (!string.IsNullOrWhiteSpace(request.AccountParent))
            {
                await service.DbContext.CatalogAccountSymbol
                    .Where(p => p.AccountSymbol == request.AccountParent && p.IsParent == false)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsParent, x => true), token);
            }

            await tx.CommitAsync(token);

            Log.Information($"[Catalog] User: {currentUser.CodeUser} created Account: {request.AccountSymbol}");
            return Results.Ok(Result.Success(true));
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync(token);
            //Log.Error(ex, "[Catalog] CreateAccountSymbol failed for {Acc}", request.AccountSymbol);
            return Results.BadRequest(Result.Failure(new Error("400", "Có lỗi khi cất giữ.")));
        }
    }


}
