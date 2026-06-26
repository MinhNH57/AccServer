using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.CatalogMemberRankings;
using SmartAccCloud.Domain.Entity.Catalogs;

namespace SmartAccCloud.API.Controllers.Catalogs;
[Route("api/catalog-member-rankings")]
[ApiController]
[Authorize]
public class CatalogMemberRankingsController(
    ICrudServicesAsync services, IMapper mapper) : ControllerBase
{
   
    [HttpGet]
    [Route("get-all")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogMemberRankings)]

    public IResult GetAll()
    {
        var lstData = services.ReadManyNoTracked<CatalogMemberRankings>()
            .ToList();
        return Results.Ok(lstData);
    }

   
    [HttpGet]
    [Route("get-member-rankings/{codeCatalogMemberRankings}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogMemberRankings)]

    public async Task<IResult> GetCatalogMemberRankings(string codeCatalogMemberRankings)
    {
        var costSoldBase = await services.ReadSingleAsync<CatalogMemberRankings>(codeCatalogMemberRankings);
        if (costSoldBase != null)
        {
            return Results.Ok(Result<CatalogMemberRankings>.Success(costSoldBase));
        }
        return Results.Ok(Result<CatalogMemberRankings>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(),
            "Mã không đã tồn tại")));
    }
   
    [HttpPost]
    [Route("add-member-rankings")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogMemberRankings)]
    public async Task<IResult> AddCatalogMemberRankings(CatalogMemberRankings param)
    {
        param.TrimStrings();
        var result = await services.CreateAndSaveAsync(param);
        if (result != null)
            return Results.Ok(Result<bool>.Success(true));
        return Results.BadRequest(Result<bool>.Failure(new Error("400", "Thêm thất bại")));

    }
   
    [HttpPut]
    [Route("edit-member-rankings")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogMemberRankings)]
    public async Task<IResult> EditCatalogMemberRankings(CatalogMemberRankingsDto param)
    {
        param.TrimStrings();
        var exitsFdSource = await services.ReadSingleAsync<CatalogMemberRankings>(param.MemberRankingsCode).ConfigureAwait(false);
        if (exitsFdSource != null)
        {
            exitsFdSource = mapper.Map(param, exitsFdSource);
            await services.UpdateAndSaveAsync(exitsFdSource);
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.Duplicate).ToString(),
            $"Sửa thất bại do mã không đã tồn tại")));
    }
   
    [HttpDelete]
    [Route("delete-member-rankings/{codeCatalogMemberRankings}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogMemberRankings)]
    public async Task<IResult> DeleteCatalogMemberRankings(string codeCatalogMemberRankings)
    {
        var exitsFdSource = await services.ReadSingleAsync<CatalogMemberRankings>(codeCatalogMemberRankings);
        if (exitsFdSource != null)
        {
            await services.DeleteAndSaveAsync<CatalogMemberRankings>(codeCatalogMemberRankings);
            return Results.Ok(true);
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã không đã tồn tại")));
    }
}
