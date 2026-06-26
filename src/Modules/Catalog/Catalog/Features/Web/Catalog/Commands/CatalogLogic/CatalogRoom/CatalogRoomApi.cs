using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Carter;
using Catalog.Base;
using MapsterMapper;
using Serilog;

namespace Catalog.Features.Web.Catalog.Commands.CatalogLogic.CatalogRoom;

public class CatalogRoomApi : ICarterModule
{ 
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");
        var api = vApi.MapGroup("catalog/").HasApiVersion(1.0);
        api.RequireAuthorization();

        api.MapPost("/create-catalog-room", CreateCatalogRoom)
            .WithName("create-catalog-room")
            .WithSummary("Tạo phòng ban")
            .WithDescription("Tạo mới danh mục phòng ban")
            .WithTags("Catalogs");

        api.MapPost("/update-catalog-room", UpdateCatalogRoom)
            .WithName("update-catalog-room")
            .WithSummary("Sửa phòng ban")
            .WithDescription("Cập nhật danh mục phòng ban")
            .WithTags("Catalogs");
    }

    /// <summary>
    /// Hàm này để tạo danh mục phòng ban
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="currentUser"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<IResult> CreateCatalogRoom(
    [AsParameters] CatalogService service,
    Base.Entities.CatalogRoom request,
    ICurrentUser currentUser,
    CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        // 1) Validate mã trùng
        bool exist = await service.DbContext.CatalogRoom
            .AsNoTracking()
            .AnyAsync(x => x.CodeRoom == request.CodeRoom, token);
        if (exist)
            return Results.BadRequest(Result.Failure(new Error("400", "Mã phòng ban đã tồn tại.")));

        // 2) Validate parent (nếu có)
        if (!string.IsNullOrWhiteSpace(request.ParentCodeRoom))
        {
            if (request.ParentCodeRoom == request.CodeRoom)
                return Results.BadRequest(Result.Failure(new Error("400", "ParentCodeRoom không được trùng CodeRoom.")));

            bool parentExists = await service.DbContext.CatalogRoom
                .AsNoTracking()
                .AnyAsync(x => x.CodeRoom == request.ParentCodeRoom, token);

            if (!parentExists)
                return Results.BadRequest(Result.Failure(new Error("400", "Phòng ban cha không tồn tại. -- ParentCodeRoom")));
        }
        await service.DbContext.CatalogRoom.AddAsync(request, token);
        if (await service.DbContext.SaveChangesAsync(token) > 0)
        {
            Log.Information($"[Catalog] User: {currentUser.CodeUser} created CatalogRoom: {request.CodeRoom}");
            return Results.Ok(Result.Success(true));
        }

        //Log.Error(ex, "[Catalog] CreateCodeRoom failed for {Acc}", request.CodeRoom);
        return Results.BadRequest(Result.Failure(new Error("400", "Có lỗi khi cất giữ.")));
    }


    public static async Task<IResult> UpdateCatalogRoom(
        [AsParameters] CatalogService service,
        IMapper mapper,
        Base.Entities.CatalogRoom request,
        ICurrentUser currentUser,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        // 1) Lấy bản ghi cũ
        var roomUpdate = await service.DbContext.CatalogRoom
            .FirstOrDefaultAsync(x => x.CodeRoom == request.CodeRoom, token);

        if (roomUpdate is null)
            return Results.BadRequest(Result.Failure(new Error("404", "Phòng ban không tồn tại.")));

        roomUpdate = mapper.Map(request, roomUpdate);
        service.DbContext.CatalogRoom.Update(roomUpdate);
        if (await service.DbContext.SaveChangesAsync(token) > 0)
        {
            Log.Information($"[Catalog] User: {currentUser.CodeUser} updated CatalogRoom: {request.CodeRoom}");
            return Results.Ok(Result.Success(true));
        }
        return Results.BadRequest(Result.Failure(new Error("404", "Có lỗi khi cập nhật thông tin phòng ban.")));
    } 
}

