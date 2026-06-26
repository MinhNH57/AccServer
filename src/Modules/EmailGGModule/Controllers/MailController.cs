using System.Net;
using System.Net.Mail;
using EmailGGModule.Models;
using EmailGGModule.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmailGGModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("test-alive")] 
        public IActionResult TestAlive()
        {
                return Ok(new { Message = "EmailGGModule is alive!" });
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] MailRequest request)
        {
            try
            {
                await _emailService.SendEmailAsync(
                    request.UserEmail!,
                    request.MessageTitle!,
                    request.MessageBody!
                );
                return Ok(new { Message = "Email sent successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error: {ex.Message}" });
            }
        }
    }
}
