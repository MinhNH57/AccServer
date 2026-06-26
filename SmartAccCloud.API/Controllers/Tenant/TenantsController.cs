using FirebaseAdmin.Messaging;
using SmartAccCloud.Application.Interfaces.Multitenant;
using SmartAccCloud.Application.Models.Tenant;
using SmartAccCloud.Application.Services.Notifications;
using SmartAccCloud.Infrastructure.Notification;


namespace SmartAccCloud.API.Controllers.Tenant;
[Route("api/tenants")]
[ApiController]
public class TenantsController(IMultitenantService multitenantService, INotificationService notificationService) : ResultControllerBase
{
    [HttpGet]
    [Route("get-by-id/{tenantId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<TenantDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result<TenantDto>))]
    public async Task<IActionResult> GetTenantById(string tenantId)
    {
        var data = await multitenantService.GetTenantInfoById(tenantId);
        return Ok(data);
    }

    [HttpGet]
    [Route("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<TenantDto>>))]
    public async Task<IActionResult> GetAllTenantActive()
    {
        var lst = await multitenantService.GetAllTenantActive();
        return Ok(lst);
    }

    [HttpGet]
    [Route("test-noti")]
    public IActionResult TestNoti()
    {
        notificationService.SendNotification(new()
        {
            Notification = new Notification()
            {
                Title = "SmartAccCloud",
                Body = "Test-push-notification"
            },
            //Data = new { VoucherNumber = "dkdsjfkdj" }
        });

        return Ok();
    }
}
