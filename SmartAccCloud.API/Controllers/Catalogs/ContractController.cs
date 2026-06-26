using Finbuckle.MultiTenant.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.Contract;
using SmartAccCloud.Domain.Entity.Catalogs;
using SmartAccCloud.Infrastructure.Caching;


namespace SmartAccCloud.API.Controllers.Catalogs;

[Route("api/catalog-contract")]
[ApiController]
[Authorize]
public class ContractController(
    ICrudServicesAsync services,
    IMapper mapper,
    IDistributedCache cache,
    IMultiTenantContextAccessor tenantContextAccessor) : ControllerBase
{
    private string _tenantId = tenantContextAccessor.MultiTenantContext.TenantInfo!.Identifier!;

    [HttpGet]
    [Route("get-all")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogContract)]
    public async Task<IResult> GetAll()
    {
        var lst = await cache.GetOrCreateAsync(GetContractCacheKey(_tenantId), () =>
        {
            var lstData = services.ReadManyNoTracked<CatalogContract>()
                .Distinct()
                .OrderByDescending(x => x.IdAsc)
                .ProjectTo<ContractVm>(mapper.ConfigurationProvider)
                .ToList();
            return Task.FromResult(lstData);
        });

        return Results.Ok(lst);
    }

    [HttpGet]
    [Route("get-contract/{code}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogContract)]

    public async Task<IResult> GetContract(string code)
    {
        var decodedParam = Uri.UnescapeDataString(code);

        var costSoldBase = await services.ReadSingleAsync<CatalogContract>(decodedParam);
        if (costSoldBase != null)
        {
            return Results.Ok(Result<CatalogContract>.Success(costSoldBase));
        }
        return Results.BadRequest(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(),
            "Mã hợp đồng không đã tồn tại")));
    }
    [HttpPost]
    [Route("add-contract")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogContract)]
    public async Task<IResult> AddContract(CatalogContract param)
    {
        param.TrimStrings();
        var result = await services.CreateAndSaveAsync(param);
        await cache.RemoveCacheAsync(GetContractCacheKey(_tenantId));
        return Results.Ok(Result<CatalogContract>.Success(result));
    }

    [HttpPut]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogContract)]
    [Route("edit-contract")]
    public async Task<IResult> EditContract(ContractDto param)
    {
        param.TrimStrings();
        var existGrp = await services.ReadSingleAsync<CatalogContract>(param.ContractNumber).ConfigureAwait(false);
        if (existGrp != null)
        {
            existGrp = mapper.Map(param, existGrp);
            await services.UpdateAndSaveAsync(existGrp);
            await cache.RemoveCacheAsync(GetContractCacheKey(_tenantId));
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.Duplicate).ToString(),
            $"Sửa thất bại do mã hợp đồng không đã tồn tại")));
    }

    [HttpDelete]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogContract)]
    [Route("delete-contract/{code}")]
    public async Task<IResult> DeleteContract(string code)
    {
        string decodedPram = Uri.UnescapeDataString(code);

        var existGrp = await services.ReadSingleAsync<CatalogContract>(decodedPram);
        if (existGrp != null)
        {
            await services.DeleteAndSaveAsync<CatalogContract>(decodedPram);
            await cache.RemoveCacheAsync(GetContractCacheKey(_tenantId));
            return Results.Ok(Result<bool>.Success(true));
        }
        return Results.NotFound(Result<bool>.Failure(new Error(((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(), $"Xóa thất bại do mã nhóm không đã tồn tại")));
    }

    private string GetContractCacheKey(string tenantId)
    {
        return $"Contract-{tenantId}";
    }
}
