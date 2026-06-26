using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.BankAccountForObj;
using SmartAccCloud.Application.Services.Catalogs.BankAccountForObject;
using SmartAccCloud.Domain.Entity.Catalogs;


namespace SmartAccCloud.API.Controllers.Catalogs;

[Route("api/catalog-bank-account-for-object")]
[ApiController]
public class BankAccountForObjectController(
    ICrudServicesAsync services, IMapper mapper, IApplicationDbContext context
    , IBankAccountForObjectServices bankAccountForObjectServices) : ControllerBase
{

    [HttpGet]
    [Route("get-all")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]

    public IResult GetAll()
    {
        var lstData = services.ReadManyNoTracked<CatalogBankAccountForObject>()
            .ToList();
        return Results.Ok(lstData);
    }

    [HttpGet]
    [Route("get-bank-account-for-object/{codeObject}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]

    public IResult GetBankAccountForObject(string codeObject)
    {
        var lstBankAccount = services.ReadManyNoTracked<CatalogBankAccountForObject>().Where(x => x.ObjectCode == codeObject).ToList();
        return Results.Ok(lstBankAccount);
    }
    [HttpPost]
    [Route("add-bank-account-for-object")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogObject)]

    public async Task<IResult> AddBankAccountForObject(List<BankAccountForObjectDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await bankAccountForObjectServices.CreateBankAccountForObject(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }

    [HttpPut]
    [Route("edit-bank-account-for-object")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogObject)]

    public async Task<IResult> EditBankAccountForObject(List<BankAccountForObjectDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await bankAccountForObjectServices.EditBankAccountForObject(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    [HttpPut]
    [Route("delete-bank-account-for-object")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogObject)]

    public async Task<IResult> DeleteBankAccountForObject(List<BankAccountForObjectDto> param)
    {
        if (param.Count == 0) return Results.Ok(true);
        param.TrimStrings();
        var result = await bankAccountForObjectServices.DeleteBankAccountForObject(param);
        if (result)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    [HttpDelete]
    [Route("delete-all-bank-/{codeObj}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogObject)]

    public async Task<IResult> DeleteBankAccountForObject(string codeObj)
    {
        var lstBankObj = context.CatalogBankAccountForObject.Where(x => x.ObjectCode == codeObj)
            .AsNoTracking().ToList();
        context.CatalogBankAccountForObject.RemoveRange(lstBankObj);
        int count = await context.SaveChangesAsync();
        if (count > 0)
            return Results.Ok();
        return Results.BadRequest();
    }

}
