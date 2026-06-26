using SmartAccCloud.Application.Pagination;
using SmartAccCloud.Application.Services.TonMyAnh;

namespace SmartAccCloud.API.Controllers.TonMyAnh;
[Route("api/steels")]
[ApiController]
[Authorize]
public class SteelsController(ITonMoblieService tonMoblieService) : ResultControllerBase
{
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await tonMoblieService.GetAll();

        return Ok(result);
    }

    [HttpGet]
    [Route("get-pagination")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetPagination([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] List<string> searchFieldNames,
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
        var result = await tonMoblieService.GetPagination(query, token);

        return Ok(result);
    }

    [HttpGet]
    [Route("get/{numberVoucher}")]
    //[AllowAnonymous]

    public async Task<IActionResult> Get(string numberVoucher)
    {
        var result = await tonMoblieService.Get(numberVoucher);

        return Ok(result);
    }

    [HttpPost]
    [Route("confirm-voucher")]
    public async Task<IActionResult> ConfirmVoucher(List<string> lstNumberVoucher)
    {
        var result = await tonMoblieService.ConfirmVoucher(lstNumberVoucher);

        return Ok(result);
    }

    [HttpPost]
    [Route("send-notification")]
    [AllowAnonymous]
    public async Task<IActionResult> SendNotification(SendConfirmVocherNoti request)
    {
        var result = await tonMoblieService.PushNofitication(request);

        return Ok(result);
    }

    [HttpPost]
    [Route("create-balance-fluctuation")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateBalanceFluctuation(CreateBalanceFluctuationRequest request, CancellationToken token)
    {
        HttpContext.Request.Headers.TryGetValue("check-sum", out var checkSum);
        var result = await tonMoblieService.CreateBalanceFluctuation(checkSum, request, token);

        return Ok(result);
    }

    [HttpPost]
    [Route("create-balance-fluctuations")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateBalanceFluctuations(List<CreateBalanceFluctuationRequest> request, CancellationToken token)
    {
        HttpContext.Request.Headers.TryGetValue("check-sum", out var checkSum);
        var result = await tonMoblieService.CreateListBalanceFluctuation(checkSum, request, token);

        return Ok(result);
    }

    [HttpGet]
    [Route("performance-report")]
    //[AllowAnonymous]
    public async Task<IActionResult> Report([FromQuery] ReportQuery query)
    {
        var result = await tonMoblieService.GetBaoCaoLaiLo(query);

        return Ok(result);
    }

    [HttpGet]
    [Route("debt-report")]
    //[AllowAnonymous]
    public async Task<IActionResult> Report1([FromQuery] ReportQuery query)
    {
        var result = await tonMoblieService.GetReportDebt(query);

        return Ok(result);
    }

    [HttpGet]
    [Route("profit-report")]
    //[AllowAnonymous]
    public async Task<IActionResult> Report2([FromQuery] ReportQuery query)
    {
        var result = await tonMoblieService.Getprofit(query);

        return Ok(result);
    }

    [HttpGet]
    [Route("profit-grp-product-report")]
   //[AllowAnonymous]
    public async Task<IActionResult> Report3([FromQuery] ReportQuery query)
    {
        var result = await tonMoblieService.ReportProfitGrpProduct(query);

        return Ok(result);
    }
}
