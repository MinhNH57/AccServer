using SmartAccCloud.Application.Models.SmartData;

namespace SmartAccCloud.Application.Interfaces.ImpExp;

public interface ISmartVatListService
{
    Task<Response.Result<List<SmartVatTaxVm>>> GetListVatByCouponId(Guid idContent, CancellationToken token);
}