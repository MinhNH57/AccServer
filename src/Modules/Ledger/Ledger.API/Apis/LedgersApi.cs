using Dapper;
using Ledger.API.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Ledger.API.Apis;

public static class LedgersApi
{
    public static RouteGroupBuilder MapLedgerApiV1(this IEndpointRouteBuilder app)
    {
        var routeBuilder = app.NewVersionedApi("Sổ cái")
            .RequireAuthorization();

        var api = routeBuilder.MapGroup("api/ledger/v{apiVersion:apiVersion}")
            .HasApiVersion(1.0);

        api.MapPost("/ledger/fa_ledger", PostLedgerFaLedgerAsync)
            .WithName("PostLedgerFALedger")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Sổ cái");

        api.MapPost("/ledger/post", PostLedgerPostAsync)
            .WithName("PostLedgerPost")
            .WithSummary("Ghi sổ chứng từ")
            .WithDescription("Ghi sổ chứng từ")
            .WithTags("Sổ cái");

        api.MapPost("/ledger/unpost", PostLedgerUnpostAsync)
            .WithName("PostLedgerUnpost")
            .WithSummary("Bỏ ghi chứng từ")
            .WithDescription("Bỏ ghi chứng từ")
            .WithTags("Sổ cái");

        api.MapGet("/ledger/fa_voucher_reference", GetFaVoucherReferenceAsync)
            .WithName("GetFAVoucherReference")
            .WithSummary("Lấy chứng từ nguồn gốc hình thành cho TSCĐ")
            .WithDescription("Lấy chứng từ nguồn gốc hình thành cho TSCĐ")
            .WithTags("Sổ cái");

        return api;
    }

    private static async Task<Ok<Response<Pagination<FALedger>>>> PostLedgerFaLedgerAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] LedgerServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var query = services.Context.FALedgers.AsNoTracking();

            if (request.Filter is { Count: > 0 })
                query = query.ApplyFilters(request.Filter);

            var queryString = query.ToQueryString();

            if (!string.IsNullOrWhiteSpace(request.Sort))
            {
                var sorts = JsonConvert.DeserializeObject<List<Sort>>(request.Sort);
                if (request.Sort.Length > 0) query = query.ApplySort(sorts);
            }

            var total = await query.CountAsync();
            Pagination<FALedger> pagination;
            switch (request.LoadMode)
            {
                case (int)EnumPagingDataType.All:

                    var pageData = await query
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync();

                    var summaryData =
                        await services.Context.GetSummaryAsync<FALedger>(queryString, request.SummaryColumns ?? []);

                    pagination = new Pagination<FALedger>
                    {
                        PageData = pageData,
                        SummaryData = summaryData,
                        Total = total,
                    };
                    break;
                case (int)EnumPagingDataType.Summary:
                    summaryData =
                        await services.Context.GetSummaryAsync<FALedger>(queryString, request.SummaryColumns ?? []);

                    pagination = new Pagination<FALedger>
                    {
                        SummaryData = summaryData,
                        Total = total,
                    };
                    break;
                default:
                    pageData = await query
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync();

                    pagination = new Pagination<FALedger>
                    {
                        PageData = pageData,
                        Total = total,
                    };
                    break;
            }

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<FALedger>>
            {
                Success = true,
                Data = pagination,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionId = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<FALedger>>
            {
                Success = false,
                Code = 99,
                SystemMessage =
                    $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionId}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionId
            };

            return TypedResults.Ok(response);
        }
    }

    private static async Task<Ok<Response<LedgerResponse>>> PostLedgerPostAsync(
        [FromBody] LedgerRequest request,
        [AsParameters] LedgerServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var message = await services.Context.Database
                .GetDbConnection()
                .QueryFirstAsync<string>("LedgerPost", new { request.BranchId, request.RefId, request.RefType });

            var ledgerResponse = new LedgerResponse
            {
                RefId = request.RefId,
                RefType = request.RefType,
                TableName = request.TableName,
                IsSuccess = true,
            };

            if (!string.IsNullOrWhiteSpace(message))
            {
                ledgerResponse.IsSuccess = false;
                ledgerResponse.MessageType = "Error";
                ledgerResponse.Message = message;
            }

            var response = new Response<LedgerResponse>
            {
                Success = ledgerResponse.IsSuccess,
                Data = ledgerResponse,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionId = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<LedgerResponse>
            {
                Success = false,
                Code = 99,
                SystemMessage =
                    $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionId}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionId
            };

            return TypedResults.Ok(response);
        }
    }

    private static async Task<Ok<Response<LedgerResponse>>> PostLedgerUnpostAsync(
        [FromBody] LedgerRequest request,
        [AsParameters] LedgerServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var message = await services.Context.Database
                .GetDbConnection()
                .QueryFirstAsync<string>("LedgerUnpost", new { request.BranchId, request.RefId, request.RefType });

            var ledgerResponse = new LedgerResponse
            {
                RefId = request.RefId,
                RefType = request.RefType,
                TableName = request.TableName,
                IsSuccess = true,
            };

            if (!string.IsNullOrWhiteSpace(message))
            {
                ledgerResponse.IsSuccess = false;
                ledgerResponse.MessageType = "Error";
                ledgerResponse.Message = message;
            }

            var response = new Response<LedgerResponse>
            {
                Success = ledgerResponse.IsSuccess,
                Data = ledgerResponse,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionId = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<LedgerResponse>
            {
                Success = false,
                Code = 99,
                SystemMessage =
                    $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionId}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionId
            };

            return TypedResults.Ok(response);
        }
    }

    private static async Task<Ok<Response<List<FAVoucherReference>>>> GetFaVoucherReferenceAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] LedgerServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var bytes = Convert.FromBase64String(req);
            var queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFAVoucherReferenceQuery>(queryString);

            var references = await services.Context.Database
                .GetDbConnection()
                .QueryAsync<FAVoucherReference>("GetFAVoucherReference",
                    new
                    {
                        queryParams.RefType, queryParams.FromDate, queryParams.ToDate, queryParams.Times,
                        queryParams.FixedAssetId, queryParams.ListAccount
                    });

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FAVoucherReference>>
            {
                Success = true,
                Data = [..references],
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1)
            };

            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            var exceptionId = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<List<FAVoucherReference>>
            {
                Success = false,
                Code = 99,
                SystemMessage =
                    $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionId}",
                UserMessage = ex.Message,
                ServerTime = serverTime,
                RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                ExceptionID = exceptionId
            };

            return TypedResults.Ok(response);
        }
    }
}