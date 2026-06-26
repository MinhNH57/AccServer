using SmartAccCloud.Application.Interfaces.ImpExp;
using SmartAccCloud.Application.Models.SmartData;


namespace SmartAccCloud.API.Controllers.Coupons;
[Route("api/[controller]")]
[ApiController]
public class CouponsController (IImpExpService impExpService): ResultControllerBase
{
    [HttpGet]
    [Route("get-by-datatype")]
    public async Task<IActionResult> GetListImpExp([FromQuery] GetCouponRequest request, CancellationToken token)
    {
        var result= await impExpService.GetImpOrExpCoupon(request, token);

        return Ok(result);
    }

    [HttpGet]
    [Route("get-detail/{idContent}")]
    public async Task<IActionResult> GetDetailCoupon(Guid idContent, CancellationToken token)
    {
        var result = await impExpService.GetDetailCoupon(idContent, token);

        return Ok(result);
    }
}
