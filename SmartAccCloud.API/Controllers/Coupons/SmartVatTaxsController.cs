using SmartAccCloud.Application.Interfaces.ImpExp;

namespace SmartAccCloud.API.Controllers.Coupons;
[Route("api/[controller]")]
[ApiController]
public class SmartVatTaxsController (ISmartVatListService smartVatListService) : ResultControllerBase
{
    [HttpGet]
    [Route("get-vat-by-coupon-id/{idContent}")]
    public async Task<IActionResult> GetVatByCouponId(Guid idContent, CancellationToken token)
    {
        var rsl = await smartVatListService.GetListVatByCouponId(idContent, token);

        return Ok(rsl);
    }
}
