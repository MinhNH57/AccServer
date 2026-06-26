using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Models.Catalogs.Object;
using SmartAccCloud.Application.Pagination;
using SmartAccCloud.Application.Services.Catalogs.CatalogObject;
using SmartAccCloud.Domain.Entity.Catalogs;
using SmartAccCloud.Infrastructure.Caching;


namespace SmartAccCloud.API.Controllers.Catalogs;

[Route("api/catalog-object")]
[ApiController]
[Authorize]
public class ObjectController(
    ICrudServicesAsync services,
    IMapper mapper,
    IApplicationDbContext dbContext,
    ICatalogObjectServices objectServices,
    IDistributedCache cache) : ControllerBase
{

    [HttpGet]
    [Route("get-all")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]
    public async Task<IResult> GetAll()
    {
        var data = await cache.GetOrCreateAsync("tenant-123", () =>
        {
            var lstData = services.ReadManyNoTracked<CatalogObject>()
                .OrderByDescending(x => x.IdAsc).TakeLast(100).ToList();
            return Task.FromResult(lstData);
        });
        return Results.Ok(data);
    }
    [HttpGet]
    [Route("get-all-mobile")]
    [AllowAnonymous]
    public async Task<IResult> GetAllMobile([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] List<string> searchFieldNames,
        [FromQuery] List<string> searchValues, [FromQuery] SortModel sort, CancellationToken token)
    {
        var searchModels = searchFieldNames
            .Zip(searchValues, (field, value) => new SearchModel(field, value))
            .ToList();
        var query = new PaginationRequest()
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            SearchByFields = searchModels.Count > 0 ? searchModels : null,
            SortModels = string.IsNullOrEmpty(sort.SortField) ? null : [sort]
        };
        var result = await objectServices.GetListMobile(query, token);

        return Results.Ok(result);
    }



    //[HttpPost]
    //[Route("get-all-paging")]
    //[HasPermission(CustomAction.AllowView, Resource.CatalogObject)]
    //public async Task<IResult> GetAllPaging(ObjectQueryPaging query, CancellationToken token)
    //{
    //    ArgumentNullException.ThrowIfNull(query);

    //    var queryObj = context.CatalogObject
    //        .Distinct()
    //        .OrderByDescending(x => x.IdAsc)
    //        .AsNoTracking();

    //    // Tạo điều kiện lọc cho các thuộc tính được chọn
    //    if (query.IsStaff || query.IsSupplier || query.IsCustomer || query.IsBank || query.IsOther)
    //    {
    //        queryObj = queryObj.Where(x =>
    //            (!query.IsStaff || x.IsStaff) &&
    //            (!query.IsSupplier || x.IsSupplier) &&
    //            (!query.IsCustomer || x.IsCustomer) &&
    //            (!query.IsBank || x.IsBank) &&
    //            (!query.IsOther || x.IsOther)
    //        );
    //    }

    //    // Lọc thêm điều kiện riêng cho "Khách hàng và nhà cung cấp"
    //    if (query.IsCustomer && query.IsSupplier)
    //    {
    //        queryObj = queryObj.Where(x => x.IsCustomer && x.IsSupplier);
    //    }

    //    // Project và phân trang
    //    var queryResult = queryObj.ProjectTo<ObjectVm>(mapper.ConfigurationProvider);
    //    var pagedResult = await queryResult.PaginateAsync(query, token);

    //    return Results.Ok(Result<PagedResult<ObjectVm>>.Success(pagedResult));
    //}




    //[HttpPost]
    //[Route("get-all-paging-staff")]
    //[HasPermission(CustomAction.AllowView, Resource.CatalogObject)]
    //public async Task<IResult> GetAllPagingStaff(PaginationRequest query, CancellationToken token)
    //{
    //    ArgumentNullException.ThrowIfNull(query);
    //    var queryObj = context.CatalogObject
    //        .Where(x => x.IsStaff)
    //        .Distinct()
    //        .OrderByDescending(x => x.IdAsc)
    //        .AsNoTracking();
    //    // Project và phân trang
    //    var queryResult = queryObj.ProjectTo<ObjectStaffVm>(mapper.ConfigurationProvider);
    //    var pagedResult = await queryResult.PaginateAsync(query, token);

    //    return Results.Ok(Result<PagedResult<ObjectStaffVm>>.Success(pagedResult));
    //}


    [HttpGet]
    [Route("get-object/{codeObject}")]
    [AllowAnonymous]
    //[HasPermission(CustomAction.AllowView, Resource.CatalogObject)]
    public async Task<IResult> GetObject(string codeObject)
    {
        var objGet = await services.ReadSingleAsync<CatalogObject>(codeObject);
        if (objGet != null)
        {
            return Results.Ok(Result<CatalogObject>.Success(objGet));
        }

        return Results.BadRequest(Result<bool>.Failure(new Error(
            ((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(),
            "Mã đơn vị không đã tồn tại")));
    }

    [HttpGet]
    [Route("get-object-by-tax/{taxcode}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]
    public async Task<IResult> GetObjectByTax(string taxcode)
    {
        var objGet = await dbContext.CatalogObject.AsNoTracking().FirstOrDefaultAsync(x => x.TaxCode == taxcode);
        if (objGet != null)
        {
            return Results.Ok(Result<CatalogObject>.Success(objGet));
        }

        return Results.BadRequest(Result<bool>.Failure(new Error(
            ((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(),
            "Mã số thuế không đã tồn tại")));
    }


    [HttpPost]
    [Route("add-object")]
    [HasPermission(CustomAction.AllowInsert, Resource.CatalogObject)]
    public async Task<IResult> AddObject(CatalogObject param)
    {
        param.TrimStrings();
        var result = await objectServices.CreateObject(param);
        return Results.Ok(result);
    }


    [HttpPut]
    [Route("edit-object")]
    [HasPermission(CustomAction.AllowEdit, Resource.CatalogObject)]
    public async Task<IResult> EditObject(ObjectDto param)
    {
        param.TrimStrings();
        var result = await objectServices.EditObject(param);
        return Results.Ok(result);
    }


    [HttpDelete]
    [Route("delete-object/{codeObject}")]
    [HasPermission(CustomAction.AllowDelete, Resource.CatalogObject)]
    public async Task<IResult> DeleteObject(string codeObject)
    {
        var existsObject = await services.ReadSingleAsync<CatalogObject>(codeObject);
        if (existsObject != null)
        {
            await services.DeleteAndSaveAsync<CatalogObject>(codeObject);
            return Results.Ok(Result<bool>.Success(true));
        }

        return Results.NotFound(Result<bool>.Failure(new Error(
            ((int)ResponseEnum.ResponseStatus.DoesNotExist).ToString(),
            $"Xóa thất bại do mã đơn vị không đã tồn tại")));
    }

    [HttpGet]
    [Route("get-all-for-cbb")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]

    public async Task<IResult> GetAllForCbb()
    {
        //var lstData = await cache.GetOrCreateAsync("cateObj", async () =>
        //{

        //    return dataQuery;
        //});
        var dataQuery = await services.ReadManyNoTracked<CatalogObject>()
            .ProjectTo<ObjectForCbb>(mapper.ConfigurationProvider).ToListAsync();

        //var lstData = services.ReadManyNoTracked<CatalogObject>().ProjectTo<ObjectForCbb>(mapper.ConfigurationProvider);
        return Results.Ok(dataQuery);
    }

    [HttpGet]
    [Route("search-for-cbb")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]

    public async Task<IResult> SearchObj([FromQuery] string? value)
    {
        var lstData = await cache.GetOrCreateAsync("cateObj", async () =>
        {
            var dataQuery = await services.ReadManyNoTracked<CatalogObject>()
                .ProjectTo<ObjectForCbb>(mapper.ConfigurationProvider).ToListAsync();

            return dataQuery;
        });

        if (string.IsNullOrEmpty(value))
            return Results.Ok(lstData);

        var result = lstData.Where(c =>
                c.ObjCode.StartsWith(value, StringComparison.OrdinalIgnoreCase) ||
                c.ObjName!.Contains(value, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        return Results.Ok(result);
    }

    //[HttpPut]
    //[Route("get-staff-salary")]
    //[HasPermission(CustomAction.AllowView, Resource.CatalogObject)]

    //public async Task<IResult> GetStaffSalary(QueryObjectSalary query)
    //{
    //    var result = context.CatalogObject
    //            .Where(x => query.LstCodeRoom.Contains(x.CodeRoom) && x.IsStaff).Distinct()
    //            .AsNoTracking().ProjectTo<ObjectSalaryVm>(mapper.ConfigurationProvider);
    //    if (!query.StatusJob)
    //    {
    //        //"Là nhân viên chính thức", "Là nhân viên không chính thức","Đã nghỉ việc"
    //        result = result.Where(x => x.StatusJob == "Là nhân viên chính thức");
    //    }

    //    var lstData = await result.ToListAsync();
    //    return Results.Ok(lstData);
    //}
    [HttpGet]
    [Route("get-object-by-cccd/{ccid}")]
    [HasPermission(CustomAction.AllowView, Resource.CatalogObject)]
    public async Task<IResult> GetObjectByCccdDetail(string ccid)
    {
        var result = await objectServices.GetObjectByCccdDetail(ccid);

        if (result.IsSuccess)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }


    [HttpPost]
    [Route("add-object-fund")]
    public async Task<IResult> AddObjectFund(ObjectDtoFundAction param)
    {
        param.TrimStrings();
        var result = await objectServices.CreateObjectFund(param);
        if (result.IsSuccess)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }


    [HttpPut]
    [Route("edit-object-fund")]
    public async Task<IResult> EditObjectFund(ObjectDtoFundAction param)
    {
        param.TrimStrings();
        var result = await objectServices.UpdateObjectFund(param);
        if (result.IsSuccess)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    [HttpDelete]
    [Route("delete-object-fund/{cccd}")]
    public async Task<IResult> DeleteObjectFund(string cccd)
    {
        var result = await objectServices.RemoveObjectByCccd(cccd);
        if (result.IsSuccess)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
}
