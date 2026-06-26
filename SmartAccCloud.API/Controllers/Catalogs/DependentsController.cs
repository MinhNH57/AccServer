using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.CatalogDependents;
using SmartAccCloud.Application.Services.Catalogs.Dependents;
using SmartAccCloud.Domain.Entity.Catalogs;


namespace SmartAccCloud.API.Controllers.Catalogs;

[Route("api/catalog-dependents-for-object")]
[ApiController]
public class DependentsController(
    ICrudServicesAsync services,
    IMapper mapper,
    IApplicationDbContext context,
    IDependentsServices dependentsServices) : ControllerBase
{

 
    [HttpGet]
    [Route("get-all")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]
    public IResult GetAll()
    {
        var lstData = services.ReadManyNoTracked<CatalogDependents>()
            .ToList();
        return Results.Ok(lstData);
    }

 
    [HttpGet]
    [Route("get-dependents-for-object/{codeObject}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]

    public IResult GetDependents(string codeObject)
    {
        var lstBankAccount = services.ReadManyNoTracked<CatalogDependents>().Where(x => x.ObjCode == codeObject)
            .ToList();
        return Results.Ok(lstBankAccount);
    }
 
    [HttpPost]
    [Route("add-dependents-for-object")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogObject)]
    public async Task<IResult> AddDependents(List<DependentsDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await dependentsServices.CreateDependents(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }

 
    [HttpPut]
    [Route("edit-dependents-for-object")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogObject)]
    public async Task<IResult> EditDependents(List<DependentsDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await dependentsServices.EditDependents(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }

 
    [HttpPut]
    [Route("delete-dependents-for-object")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogObject)]
    public async Task<IResult> DeleteDependents(List<DependentsDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await dependentsServices.DeleteDependents(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }

 
    [HttpDelete]
    [Route("delete-all-dependents-/{codeObj}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogObject)]
    public async Task<IResult> DeleteDependents(string codeObj)
    {
        var lstBankObj = context.CatalogDependents.Where(x => x.ObjCode == codeObj)
            .AsNoTracking().ToList();
        context.CatalogDependents.RemoveRange(lstBankObj);
        int count = await context.SaveChangesAsync();
        if (count > 0)
            return Results.Ok();
        return Results.BadRequest();
    }
}