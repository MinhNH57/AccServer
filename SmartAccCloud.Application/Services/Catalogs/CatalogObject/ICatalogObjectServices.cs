using SmartAccCloud.Application.Models.Catalogs.Object;
using SmartAccCloud.Application.Pagination;

namespace SmartAccCloud.Application.Services.Catalogs.CatalogObject;

public interface ICatalogObjectServices
{
    Task<Result<bool>> CreateObject(Domain.Entity.Catalogs.CatalogObject param);
    Task<Result<bool>> EditObject(ObjectDto param);
    Task<Result<Domain.Entity.Catalogs.CatalogObject>> GetObjectByCccd(string cccd);
    Task<Result<ObjectViewMobileDetail>> GetObjectByCccdDetail(string cccd);
    Task<Result<bool>> RemoveObjectByCccd(string cccd);
    Task<Result<bool>> CreateObjectFund(ObjectDtoFundAction param);
    Task<Result<bool>> UpdateObjectFund(ObjectDtoFundAction param);

    Task<Result<Pagination.PagedResult<ObjectViewMobile>>> GetListMobile(PaginationRequest request,
        CancellationToken token);

}