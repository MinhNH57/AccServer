using Auth.API.Data;
using Auth.API.Models.DTOs;
using Auth.API.Models.Entities;
using Auth.API.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Smart.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(ApplicationDbContext dbContext, ILogger<AuthController> logger) : ControllerBase
{

    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly ILogger<AuthController> _logger = logger;

    [HttpGet("register")]
    public async Task<IActionResult> GetUserRegistrationsAsync()
    {
        
        var currentTime = DateTime.Now;

        try
        {
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var registrations = await _dbContext.PendingUserRegistrations
                .AsNoTracking()
                .ToListAsync();

            var response = new Response<List<PendingUserRegistration>>()
            {
                Success = true,
                Data = registrations,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<PendingUserRegistration>>()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return Ok(response);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> PostManagementRegisterAsync([FromBody] RegisterRequest request)
    {
        var currentTime = DateTime.Now;

        try
        {
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var registration = await _dbContext.PendingUserRegistrations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            var response = new Response()
            {
                Success = true,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            if (registration != null)
            {
                // Gửi lại email
                response.SubCode = (int)RegisterEnum.ResentEmail;

                return Ok(response);
            }

            var pendingUserRegistration = new PendingUserRegistration()
            {
                CompanyName = request.CompanyName,
                TaxCode = request.TaxCode,
                AppName = string.Join(";", request.AppName),
                AppCode = request.AppCode,
                IDNumber = request.IDNumber,
                BusinessType = request.BusinessType,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Mobile = request.Mobile,
                JobPosition = request.JobPosition,
                SMARTSalerCode = request.SMARTSalerCode,
                Purpose = request.Purpose,
                Query = request.Query,
                ReturnURL = request.ReturnURL,
                QueryParam = request.QueryParam
            };

            await _dbContext.PendingUserRegistrations.AddAsync(pendingUserRegistration);
            await _dbContext.SaveChangesAsync();

            response.SubCode = (int)RegisterEnum.SentEmail;

            return Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response()
            {
                Success = false,
                Code = 99,
                SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionID
            };

            return Ok(response);
        }
    }
}
