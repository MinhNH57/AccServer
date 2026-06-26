using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.BillOfMaterials;
using SmartAccCloud.Application.Services.Catalogs.BillOfMaterials;
using SmartAccCloud.Domain.Entity.Catalogs;

namespace SmartAccCloud.API.Controllers.Catalogs;

[Route("api/bill-of-materials")]
[ApiController]
[Authorize]
public class BillOfMaterialsController(
    ICrudServicesAsync services, IMapper mapper,IBillOfMaterialsServices billOfMaterialsServices) : ControllerBase
{

  
    [HttpGet]
    [Route("get-all-bom-by-{productCode}")]
    [HasPermission(CustomAction.AllowView, Resource.BillOfMaterials)]

    public IResult GetAll(string productCode)
    {
        var lstData = services.ReadManyNoTracked<BillOfMaterials>().Where(x=>x.ProductCode == productCode)
            .ToList();
        return Results.Ok(lstData);
    }

  
    [HttpPost]
    [Route("add-bom")]
    [HasPermission(CustomAction.AllowInsert, Resource.BillOfMaterials)]
    public async Task<IResult> AddBom(BillOfMaterials param)
    {
        param.TrimStrings();
        var result = await services.CreateAndSaveAsync(param);
        return Results.Ok(Result<BillOfMaterials>.Success(result));
    }
  
    [HttpPut]
    [Route("edit-bom")]
    [HasPermission(CustomAction.AllowEdit, Resource.BillOfMaterials)]
    public async Task<IResult> EditBom(BillOfMaterialsDto param)
    {
        param.TrimStrings();
        var exitsGrp = await services.ReadSingleAsync<BillOfMaterials>(param.Id).ConfigureAwait(false);
        if (exitsGrp != null)
        {
            exitsGrp = mapper.Map(param, exitsGrp);
            await services.UpdateAndSaveAsync(exitsGrp);
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.Duplicate).ToString(),
            $"Sửa thất bại do mã không đã tồn tại")));
    }

  
    [HttpPost]
    [Route("add-bom-lst")]
    [HasPermission(CustomAction.AllowInsert, Resource.BillOfMaterials)]
    public async Task<IResult> AddBomLst(List<BillOfMaterialsDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await billOfMaterialsServices.CreateBillOfMaterials(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
  
    [HttpPut]
    [Route("edit-bom-lst")]
    [HasPermission(CustomAction.AllowEdit, Resource.BillOfMaterials)]
    public async Task<IResult> EditBomLst(List<BillOfMaterialsDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await billOfMaterialsServices.EditBillOfMaterials(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
  
    [HttpPut]
    [Route("delete-bom-lst")]
    [HasPermission(CustomAction.AllowEdit, Resource.BillOfMaterials)]
    public async Task<IResult> DeleteBomLst(List<BillOfMaterialsDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await billOfMaterialsServices.DeleteBillOfMaterials(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }


  
    [HttpDelete]
    [Route("delete-bom/{id}")]
    [HasPermission(CustomAction.AllowDelete, Resource.BillOfMaterials)]
    public async Task<IResult> DeleteBom(Guid id)
    {
        var exitsGrp = await services.ReadSingleAsync<BillOfMaterials>(id);
        if (exitsGrp != null)
        {
            await services.DeleteAndSaveAsync<BillOfMaterials>(id);
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã không đã tồn tại")));
    }

}
