using Microsoft.EntityFrameworkCore;
using SmartAccCloud.Application.Models.Catalogs.CatalogVoucherNumber;
using SmartAccCloud.Domain.Entity.Catalogs;


namespace SmartAccCloud.API.Controllers.Catalogs;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CatalogVoucherNumbersController(ICrudServices crudServices, IMapper mapper) : ResultControllerBase
{
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetListVoucherNumb(CancellationToken token)
    {
        var lstData = await crudServices.ReadManyNoTracked<CatalogVoucherNbVm>().ToListAsync(token);

        return Ok(Result<List<CatalogVoucherNbVm>>.Success(lstData));
    }
    [HttpGet]
    [Route("get-all-vm-for-reason")]
    public async Task<IActionResult> GetListVoucherNumbReason(CancellationToken token)
    {
        var lstData = await crudServices.ReadManyNoTracked<CatalogVoucherNumber>()
            .ProjectTo<CatalogVoucherNbForReasonVm>(mapper.ConfigurationProvider).ToListAsync(token);

        return Ok(lstData);
    }
    [HttpGet]
    [Route("get-by-id/{id}")]
    public IActionResult GetById(Guid id)
    {
        var entity = crudServices.ReadSingle<CatalogVoucherNbVm>(c => c.Id == id);
        if (entity is null)
            return Ok(Result<bool>.Failure(new Error("404", "Không tìm thấy")));

        return Ok(Result<CatalogVoucherNbVm>.Success(entity));
    }

    [HttpGet]
    [Route("get-by-coupon-type/{couponType}")]
    public async Task<IActionResult> GetByCouponType(string couponType, CancellationToken token)
    {
        var entity = await crudServices.ReadManyNoTracked<CatalogVoucherNumber>()
            .FirstOrDefaultAsync(c=>c.DataType == couponType, cancellationToken: token);
        if (entity is null)
            return Ok(Result<bool>.Failure(new Error("404", "Không tìm thấy")));
        
        return Ok(Result<CatalogVoucherNumber>.Success(entity));
    }
    [HttpPost]
    [Route("create")]
    public IActionResult CreateVoucherNumber(CatalogVoucherNumber request)
    {
        crudServices.CreateAndSave(request);
        return Ok(Result<Guid>.Success(request.Id));
    }

    [HttpPut]
    [Route("update")]
    public IActionResult UpdateVoucherNumber(CatalogVoucherNbVm request)
    {
        var entity = crudServices.ReadSingle<CatalogVoucherNumber>(c => c.Id == request.Id);
        if (entity is null)
            return Ok(Result<bool>.Failure(new Error("404", "Không tìm thấy")));

        mapper.Map(request, entity);

        crudServices.UpdateAndSave(entity);

        return Ok(Result<bool>.Success(true));
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult DeleteVoucherNumber(Guid id)
    {
        var entity = crudServices.ReadSingle<CatalogVoucherNumber>(c => c.Id == id);
        if (entity is null)
            return Ok(Result<bool>.Failure(new Error("404", "Không tìm thấy")));

        crudServices.DeleteAndSave<CatalogVoucherNumber>(id);

        return Ok(Result<bool>.Success(true));
    }
}
