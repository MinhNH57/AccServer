using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Ref.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ref.API.Apis;

public static class RefsApi
{
    public static RouteGroupBuilder MapRefApiV1(this IEndpointRouteBuilder app)
    {
        var routeBuilder = app.NewVersionedApi("Tham chiếu")
            .RequireAuthorization();

        var api = routeBuilder.MapGroup("api/ref/v{apiVersion:apiVersion}")
            .HasApiVersion(1.0);

        api.MapPost("/reference/paging_filter", PostReferencePagingFilterAsync)
            .WithName("PostReferencePagingFilter")
            .WithSummary("Lấy chứng từ tham chiếu cho chứng từ")
            .WithDescription("Lấy chứng từ tham chiếu cho chứng từ")
            .WithTags("Tham chiếu");

        api.MapPost("/reference/{id:guid}", PostReferenceAsync)
            .WithName("PostReference")
            .WithSummary("Thêm tham chiếu cho chứng từ")
            .WithDescription("Thêm tham chiếu cho chứng từ")
            .WithTags("Tham chiếu");

        api.MapGet("/reference/{id:guid}", GetReferenceAsync)
            .WithName("GetReferenceAsync")
            .WithSummary("Lấy tham chiếu cho chứng từ")
            .WithDescription("Lấy tham chiếu cho chứng từ")
            .WithTags("Tham chiếu");

        return api;
    }

    public static async Task<Ok<Response<Pagination<ViewReference>>>> PostReferencePagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] RefServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(request.Parameters);
            string parameters = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<ParamsGetData>(parameters);

            var customFilter = new List<Filter>()
            {
                new()
                {
                    Property = (int)EnumFields.RefDate,
                    Operator = Operator.GreaterThanEquals,
                    Value = queryParams.PFromDate.ToString(),
                    DataType = DataType.DateTime,
                    Operand = Operand.And
                },
                new()
                {
                    Property = (int)EnumFields.RefDate,
                    Operator = Operator.LessThanEquals,
                    Value = queryParams.PToDate.ToString(),
                    DataType = DataType.DateTime,
                    Operand = Operand.And
                }
            };

            switch (queryParams.PSearchType)
            {
                case 1:
                    customFilter.Add(new()
                    {
                        Property = (int)EnumFields.ReferenceType,
                        Operator = Operator.Equals,
                        Value = queryParams.PSearchValue,
                        DataType = DataType.String,
                        Operand = Operand.And
                    });
                    break;
                case 2:
                case 3:
                    customFilter.Add(new()
                    {
                        Property = (int)EnumFields.RefNoFinance,
                        Operator = Operator.Equals,
                        Value = queryParams.PSearchValue,
                        DataType = DataType.String,
                        Operand = Operand.And
                    });
                    break;
                case 4:
                    customFilter.Add(new()
                    {
                        Property = (int)EnumFields.AccountObjectCode,
                        Operator = Operator.Equals,
                        Value = queryParams.PSearchValue,
                        DataType = DataType.String,
                        Operand = Operand.And
                    });
                    break;
            }

            if (request.Filter != null)
            {
                request.Filter.AddRange(customFilter);
            }
            else
            {
                request.Filter = customFilter;
            }

            var pagination = new Pagination<ViewReference>();

            var query = services.Context.ViewReferences.AsNoTracking();

            if (request.Filter is { Count: > 0 })
                query = query.ApplyFilters(request.Filter);

            var queryString = query.ToQueryString();

            if (!string.IsNullOrWhiteSpace(request.Sort))
            {
                List<Sort> sorts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sort>>(request.Sort);
                if (request.Sort.Length > 0) query = query.ApplySort(sorts);
            }

            var total = await query.CountAsync();

            switch (request.LoadMode)
            {
                case (int)EnumPagingDataType.All:
                    var pageData = await query
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync();

                    var summaryData = await services.Context.GetSummaryAsync<ViewReference>(queryString, request.SummaryColumns);

                    pagination = new Pagination<ViewReference>()
                    {
                        PageData = [.. pageData.Select(services.Mapper.Map<ViewReference>)],
                        SummaryData = summaryData,
                        Total = total,
                    };
                    break;
                case (int)EnumPagingDataType.Summary:
                    summaryData = await services.Context.GetSummaryAsync<ViewReference>(queryString, request.SummaryColumns);

                    pagination = new Pagination<ViewReference>()
                    {
                        SummaryData = summaryData,
                        Total = total,
                    };
                    break;
                case (int)EnumPagingDataType.Data:
                default:
                    pageData = await query
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync();

                    pagination = new Pagination<ViewReference>()
                    {
                        PageData = [.. pageData.Select(services.Mapper.Map<ViewReference>)],
                        Total = total,
                    };
                    break;
            }

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<ViewReference>>()
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
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<Pagination<ViewReference>>()
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

    public static async Task<Ok<Response>> PostReferenceAsync(
        [FromRoute] Guid id,
        [FromBody] List<ReferenceRequest> request,
        [AsParameters] RefServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var references = await services.Context.References
                .AsNoTracking()
                .Where(x => x.RefId1 == id)
                .ToListAsync();

            bool isModified = false;

            if (references.Count > 0)
            {
                isModified = true;

                services.Context.References.RemoveRange(references);
            }

            if (request is { Count: > 0 })
            {
                isModified = true;

                foreach (var item in request)
                {
                    var reference = new Reference()
                    {
                        RefType1 = item.RefType1,
                        RefNoFinance2 = item.RefNoFinance2,
                        RefNoManagement2 = item.RefNoManagement2,
                        RefId2 = item.RefId2,
                        RefType2 = item.RefType2,
                        ReferenceType = item.ReferenceType,
                        State = item.State,
                        RefId1 = item.RefId1,
                    };

                    await services.Context.References.AddAsync(reference);
                }
            }

            if (isModified)
                await services.Context.SaveChangesAsync();

            var response = new Response()
            {
                Success = true,
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

            return TypedResults.Ok(response);
        }
    }

    public static async Task<Ok<Response<List<Reference>>>> GetReferenceAsync(
        [FromRoute] Guid id,
        [AsParameters] RefServices services)
    {
        var currentTime = DateTime.Now;

        try
        {

            var references = await services.Context.References
                .AsNoTracking()
                .Where(x => x.RefId1 == id)
                .ToListAsync();

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<Reference>>()
            {
                Success = true,
                Data = references,
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
            var response = new Response<List<Reference>>()
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
