using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Models.Catalogs.CatalogUnit;
using SmartAccCloud.Domain.Entity.Catalogs;


namespace SmartAccCloud.API.Controllers.Catalogs;

[Route("api/catalog-units")]
[ApiController]
[Authorize]
public class CatalogUnitsController(ICrudServices crudServices) : ResultControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [Route("get-all")]
    public async Task<IActionResult> GetAllCatalogUnit(CancellationToken token)
    {
        var lst = await crudServices.ReadManyNoTracked<CatalogUnitVM>()
            .Where(c=>c.IsActive)
            .ToListAsync(cancellationToken: token);

        return Ok(Result<List<CatalogUnitVM>>.Success(lst));
    }

    [HttpGet]
    [Route("get-by-code/{codeUnit}")]
    public IActionResult GetByCode(int codeUnit)
    {
        var unit = crudServices.ReadSingle<CatalogUnit>(c => c.CodeUnit == codeUnit);

        if (unit is null)
            return Ok(Result<CatalogUnit>.Failure(new Error("404", "Không tìm thấy đơn vị")));

        return Ok(Result<CatalogUnit>.Success(unit));
    }

    [HttpPost]
    [Route("create-unit")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogUnit)]
    public IActionResult CreateUnit(CatalogUnit unit, CancellationToken token)
    {
        var isExistUnit = crudServices.ReadSingle<CatalogUnit>(c => c.CodeUnit == unit.CodeUnit);
        if (isExistUnit is not null)
            return Ok(Result<Guid>.Failure(new Error("400", "Mã đơn vị đã tồn tại")));
        unit.IdUnitOk = 888;
        crudServices.CreateAndSave(unit);
        
        return Ok(Result<Guid>.Success(unit.Id));
    }

    [HttpPut]
    [Route("update-unit")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogUnit)]
    public IActionResult UpdateUnit(CatalogUnit unit, CancellationToken token)
    {
        var isExistUnit = crudServices.ReadSingle<CatalogUnit>(c => c.CodeUnit == unit.CodeUnit);
        if (isExistUnit is null)
            return Ok(Result<bool>.Failure(new Error("400", "Mã đơn vị đã tồn tại")));

        isExistUnit.NameUnit = unit.NameUnit;
        isExistUnit.Address = unit.Address;
        isExistUnit.PositionDir = unit.PositionDir;
        isExistUnit.DirectorName = unit.DirectorName;
        isExistUnit.ByBatchNo = unit.ByBatchNo;
        isExistUnit.IsActive = unit.IsActive;

        crudServices.UpdateAndSave(isExistUnit);

        return Ok(Result<bool>.Success(true));
    }

    [HttpDelete]
    [Route("delete-unit/{codeUnit}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogUnit)]
    public IActionResult DeleteUnit(int codeUnit)
    {
        var isExistUnit = crudServices.ReadSingle<CatalogUnit>(c => c.CodeUnit == codeUnit);
        if (isExistUnit is null)
            return Ok(Result<bool>.Failure(new Error("400", "Mã đơn vị không tồn tại")));

        crudServices.DeleteAndSave<CatalogUnit>(codeUnit);

        return Ok(Result<bool>.Success(true));
    }

}