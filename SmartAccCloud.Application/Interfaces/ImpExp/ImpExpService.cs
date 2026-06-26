using SmartAccCloud.Application.Models.SmartData;

namespace SmartAccCloud.Application.Interfaces.ImpExp;

public interface IImpExpService
{
    Task<Result<List<SmartDataVm>>> GetImpOrExpCoupon(GetCouponRequest request, CancellationToken token);
    Task<Result<List<SmartContentDataVm>>> GetDetailCoupon(Guid idContent, CancellationToken token);
    
}