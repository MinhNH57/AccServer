using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RefNo.API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefNo.API.Apis;

public static class RefNosApi
{
    public static RouteGroupBuilder MapRefNoApiV1(this IEndpointRouteBuilder app)
    {
        var routeBuilder = app.NewVersionedApi("Chứng từ")
            .RequireAuthorization();

        var api = routeBuilder.MapGroup("api/refno/v{apiVersion:apiVersion}")
            .HasApiVersion(1.0);

        api.MapGet("/refno/next_value", GetRefNoNextValueAsync)
            .WithName("GetRefNoNextValue")
            .WithSummary("Lấy số CT ghi tăng tiếp theo")
            .WithDescription("Lấy số CT ghi tăng tiếp theo")
            .WithTags("Số chứng từ");

        return api;
    }

    public static async Task<Ok<Response<Dictionary<string, string>>>> GetRefNoNextValueAsync(
        [FromQuery(Name = "categories")] int categories,
        [FromQuery(Name = "branch_id")] Guid? branchId,
        [AsParameters] RefNoServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var numberingRule = await services.Context.NumberingRules
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RefTypeCategory == categories);



            var response = new Response<Dictionary<string, string>>()
            {
                Success = true,
                Data = new Dictionary<string, string>()
                {
                    { categories.ToString(), $"{numberingRule.Prefix}{(numberingRule.Value + 1).ToString().PadLeft(numberingRule.LengthOfValue ?? 1, '0')}{numberingRule.Suffix}" }
                },
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Dictionary<string, string>>()
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

            return TypedResults.Ok(response);
        }
    }
}
