using Microsoft.AspNetCore.Http.HttpResults;
using Smart.Shared;
using System.Text;
using System.Text.Json;

public static class FixedAssetsApi
{
    public static RouteGroupBuilder MapFixedAssetApiV1(this IEndpointRouteBuilder app)
    {
        var routeBuilder = app.NewVersionedApi("Tài sản cố định")
            .RequireAuthorization();

        var api = routeBuilder.MapGroup("api/fa/v{apiVersion:apiVersion}")
            .HasApiVersion(1.0);

        api.MapGet("/fixed_asset/next_value", GetFixedAssetNextValueAsync)
            .WithName("GetFixedAssetNextValue")
            .WithSummary("Lấy mã TSCĐ tiếp theo")
            .WithDescription("Lấy mã TSCĐ tiếp theo")
            .WithTags("Chứng từ");

        api.MapGet("/fixed_asset/get_fa_arising", GetFixedAssetGetFAArisingAsync)
            .WithName("GetFixedAssetGetFAArising")
            .WithSummary("Lấy chứng từ phát sinh của tài sản")
            .WithDescription("Lấy chứng từ phát sinh của tài sản")
            .WithTags("Chứng từ");

        api.MapGet("/fixed_asset/detail_full", GetFixedAssetDetailFullAsync)
            .WithName("GetFixedAssetDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Ghi tăng tài sản");

        api.MapPost("/fixed_asset/paging_filter", PostFixedAssetPagingFilterAsync)
            .WithName("PostFixedAssetPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Ghi tăng tài sản");

        api.MapPost("/fixed_asset/save_full", PostFixedAssetSaveFullAsync)
            .WithName("PostFixedAssetSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Ghi tăng tài sản");

        api.MapPut("/fixed_asset/save_full", PutFixedAssetSaveFullAsync)
            .WithName("PutFixedAssetSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Ghi tăng tài sản");

        api.MapDelete("/fixed_asset/batch_voucher", DeleteFixedAssetBatchVoucherAsync)
            .WithName("DeleteFixedAssetBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Ghi tăng tài sản");

        api.MapGet("/fa_decrement/detail_full", GetFADecrementDetailFullAsync)
            .WithName("GetFADecrementDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Ghi giảm tài sản");

        api.MapPost("/fa_decrement/paging_filter", PostFADecrementPagingFilterAsync)
            .WithName("PostFADecrementPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Ghi giảm tài sản");

        api.MapGet("/fa_decrement/get_available_fixed_asset", GetFADecrementGetAvailableFixedAssetAsync)
            .WithName("GetFADecrementGetAvailableFixedAsset")
            .WithSummary("Lấy tất cả TSCĐ có sẵn")
            .WithDescription("Lấy tất cả TSCĐ có sẵn")
            .WithTags("Ghi giảm tài sản");

        api.MapGet("/fa_decrement/su_original_fa_decrement", GetFADecrementSUOriginalFADecrementAsync)
            .WithName("GetFADecrementSUOriginalFADecrement")
            .WithSummary("Lấy tất cả TSCĐ ghi giảm làm nguồn gốc cho CCDC")
            .WithDescription("Lấy tất cả TSCĐ ghi giảm làm nguồn gốc cho CCDC")
            .WithTags("Ghi giảm tài sản");

        api.MapGet("/fa_decrement_get/select_su_from_fa_decrement", GetFADecrementGetSelectSUFromFADecrementAsync)
            .WithName("GetFADecrementGetSelectSUFromFADecrement")
            .WithSummary("Lấy tất cả TSCĐ ghi giảm làm nguồn gốc cho CCDC")
            .WithDescription("Lấy tất cả TSCĐ ghi giảm làm nguồn gốc cho CCDC")
            .WithTags("Ghi giảm tài sản");

        api.MapGet("/fa_decrement/get_custom_fixed_asset", GetFADecrementGetCustomFixedAssetAsync)
            .WithName("GetFADecrementGetCustomFixedAsset")
            .WithSummary("Lấy tất cả TSCĐ tùy chỉnh")
            .WithDescription("Lấy tất cả TSCĐ tùy chỉnh")
            .WithTags("Ghi giảm tài sản");

        api.MapPost("/fa_decrement/save_full", PostFADecrementSaveFullAsync)
            .WithName("PostFADecrementSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Ghi giảm tài sản");

        api.MapPut("/fa_decrement/save_full", PutFADecrementSaveFullAsync)
            .WithName("PutFADecrementSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Ghi giảm tài sản");

        api.MapDelete("/fa_decrement/batch_voucher", DeleteFADecrementBatchVoucherAsync)
            .WithName("DeleteFADecrementBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Ghi giảm tài sản");

        api.MapPost("/fa_transfer/paging_filter", PostFATransferPagingFilterAsync)
            .WithName("PostFATransferPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Điều chuyển tài sản");

        api.MapGet("/fa_transfer/get_available_fixed_asset", GetFATransferGetAvailableFixedAssetAsync)
            .WithName("GetFATransferGetAvailableFixedAsset")
            .WithSummary("Lấy tất cả TSCĐ có sẵn")
            .WithDescription("Lấy tất cả TSCĐ có sẵn")
            .WithTags("Điều chuyển tài sản");

        api.MapPost("/fa_transfer/save_full", PostFATransferSaveFullAsync)
            .WithName("PostFATransferSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Điều chuyển tài sản");

        api.MapPut("/fa_transfer/save_full", PutFATransferSaveFullAsync)
            .WithName("PutFATransferSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Điều chuyển tài sản");

        api.MapGet("/fa_transfer/detail_full", GetFATransferDetailFullAsync)
            .WithName("GetFATransferDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Điều chuyển tài sản");

        api.MapDelete("/fa_transfer/batch_voucher", DeleteFATransferBatchVoucherAsync)
            .WithName("DeleteFATransferBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Điều chuyển tài sản");

        api.MapPost("/fa_depreciation/paging_filter", PostFADepreciationPagingFilterAsync)
            .WithName("PostFADepreciationPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Tính khấu hao");

        api.MapGet("/fa_depreciation/check_exists_fa_depreciation", GetFADepreciationCheckExistsFADepreciationAsync)
            .WithName("GetFADepreciationCheckExistsFADepreciation")
            .WithSummary("Kiểm tra có khấu hao TSCĐ không")
            .WithDescription("Kiểm tra có khấu hao TSCĐ không")
            .WithTags("Tính khấu hao");

        api.MapGet("/fa_depreciation/fa_depreciation_detail", GetFADepreciationFADepreciationDetailAsync)
            .WithName("GetFADepreciationFADepreciationDetail")
            .WithSummary("Lấy chi tiết tính khấu hao")
            .WithDescription("Lấy chi tiết tính khấu hao")
            .WithTags("Tính khấu hao");

        api.MapGet("/fa_depreciation/detail_full", GetFADepreciationDetailFullAsync)
            .WithName("GetFADepreciationDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Tính khấu hao");

        api.MapPost("/fa_depreciation/save_full", PostFADepreciationSaveFullAsync)
            .WithName("PostFADepreciationSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Tính khấu hao");

        api.MapPut("/fa_depreciation/save_full", PutFADepreciationSaveFullAsync)
            .WithName("PutFADepreciationSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Điều chuyển tài sản");

        api.MapDelete("/fa_depreciation/batch_voucher", DeleteFADepreciationBatchVoucherAsync)
            .WithName("DeleteFADepreciationBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Tính khấu hao");

        api.MapPost("/fa_change_financial_leasing_to_owner/paging_filter", PostFAChangeFinancialLeasingToOwnerPagingFilterAsync)
            .WithName("PostFAChangeFinancialLeasingToOwnerPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Chuyển TS thuê tài chính thành TS sở hữu");

        api.MapGet("/fa_change_financial_leasing_to_owner/available_fixed_asset", GetFAChangeFinancialLeasingToOwnerAvailableFixedAssetAsync)
            .WithName("GetFAChangeFinancialLeasingToOwnerAvailableFixedAsset")
            .WithSummary("Lấy tất cả TSCĐ có sẵn")
            .WithDescription("Lấy tất cả TSCĐ có sẵn")
            .WithTags("Chuyển TS thuê tài chính thành TS sở hữu");

        api.MapPost("/fa_change_financial_leasing_to_owner/save_full", PostFAChangeFinancialLeasingToOwnerSaveFullAsync)
            .WithName("PostFAChangeFinancialLeasingToOwnerSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Chuyển TS thuê tài chính thành TS sở hữu");

        api.MapPut("/fa_change_financial_leasing_to_owner/save_full", PutFAChangeFinancialLeasingToOwnerSaveFullAsync)
            .WithName("PutFAChangeFinancialLeasingToOwnerSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Chuyển TS thuê tài chính thành TS sở hữu");

        api.MapGet("/fa_change_financial_leasing_to_owner/detail_full", GetFAChangeFinancialLeasingToOwnerDetailFullAsync)
            .WithName("GetFAChangeFinancialLeasingToOwnerDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Chuyển TS thuê tài chính thành TS sở hữu");

        api.MapDelete("/fa_change_financial_leasing_to_owner/batch_voucher", DeleteFAChangeFinancialLeasingToOwnerBatchVoucherAsync)
            .WithName("DeleteFAChangeFinancialLeasingToOwnerBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Chuyển TS thuê tài chính thành TS sở hữu");

        api.MapGet("/fa_adjustment/detail_full", GetFAAdjustmentDetailFullAsync)
            .WithName("GetFAAdjustmentDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Đánh giá lại");

        api.MapPost("/fa_adjustment/paging_filter", PostFAAdjustmentPagingFilterAsync)
            .WithName("PostFAAdjustmentPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Đánh giá lại");

        api.MapGet("/fa_adjustment/get_available_fixed_asset", GetFAAdjustmentGetAvailableFixedAssetAsync)
            .WithName("GetFAAdjustmentGetAvailableFixedAsset")
            .WithSummary("Lấy tất cả TSCĐ có sẵn")
            .WithDescription("Lấy tất cả TSCĐ có sẵn")
            .WithTags("Đánh giá lại");

        api.MapGet("/fa_adjustment/get_custom_fixed_asset", GetFAAdjustmentGetCustomFixedAssetAsync)
            .WithName("GetFAAdjustmentGetCustomFixedAsset")
            .WithSummary("Lấy tất cả TSCĐ tùy chỉnh")
            .WithDescription("Lấy tất cả TSCĐ tùy chỉnh")
            .WithTags("Đánh giá lại");

        api.MapPost("/fa_adjustment/save_full", PostFAAdjustmentSaveFullAsync)
            .WithName("PostFAAdjustmentSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Đánh giá lại");

        api.MapPut("/fa_adjustment/save_full", PutFAAdjustmentSaveFullAsync)
            .WithName("PutFAAdjustmentSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Đánh giá lại");

        api.MapDelete("/fa_adjustment/batch_voucher", DeleteFAAdjustmentBatchVoucherAsync)
            .WithName("DeleteFAAdjustmentBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Đánh giá lại");

        api.MapGet("/fa_audit/detail_full", GetFAAuditDetailFullAsync)
            .WithName("GetFAAuditDetailFull")
            .WithSummary("Lấy dữ liệu theo id")
            .WithDescription("Lấy dữ liệu theo id")
            .WithTags("Kiểm kê");

        api.MapPost("/fa_audit/paging_filter", PostFAAuditPagingFilterAsync)
            .WithName("PostFAAuditPagingFilter")
            .WithSummary("Paging lấy về danh sách có phân trang")
            .WithDescription("Paging lấy về danh sách có phân trang")
            .WithTags("Kiểm kê");

        api.MapGet("/fa_audit/get_available_fixedasset_for_audit", GetFAAuditGetAvailableFixedAssetAsync)
            .WithName("GetFAAuditGetAvailableFixedAsset")
            .WithSummary("Lấy tất cả TSCĐ có sẵn")
            .WithDescription("Lấy tất cả TSCĐ có sẵn")
            .WithTags("Kiểm kê");

        api.MapGet("/fa_audit/check_exist_fa_decrement_to_fa_audit", GetFAAuditCheckExistFADecrementToFAAuditAsync)
            .WithName("GetFAAuditCheckExistFADecrementToFAAudit")
            .WithSummary("Lấy tất cả TSCĐ có sẵn")
            .WithDescription("Lấy tất cả TSCĐ có sẵn")
            .WithTags("Kiểm kê");

        api.MapPost("/fa_audit/save_full", PostFAAuditSaveFullAsync)
            .WithName("PostFAAuditSaveFull")
            .WithSummary("Thực hiện thêm mới")
            .WithDescription("Thực hiện thêm mới")
            .WithTags("Kiểm kê");

        api.MapPut("/fa_audit/save_full", PutFAAuditSaveFullAsync)
            .WithName("PutFAAuditSaveFull")
            .WithSummary("Thực hiện cập nhật")
            .WithDescription("Thực hiện cập nhật")
            .WithTags("Kiểm kê");

        api.MapDelete("/fa_audit/batch_voucher", DeleteFAAuditBatchVoucherAsync)
            .WithName("DeleteFAAuditBatchVoucher")
            .WithSummary("Thực hiện xoá theo danh sách")
            .WithDescription("Thực hiện xoá theo danh sách")
            .WithTags("Kiểm kê");

        return api;
    }

    public static async Task<Ok<Response<Dictionary<string, string>>>> GetFixedAssetNextValueAsync(
        [FromQuery(Name = "categories")] int categories,
        [FromQuery(Name = "branch_id")] Guid? branchId,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var data = new Dictionary<string, string>();

            string nextValue = await services.Queries.GetFixedAssetNextValueAsync(categories, branchId);

            if (!string.IsNullOrWhiteSpace(nextValue))
            {
                data.Add(categories.ToString(), nextValue);
            }

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Dictionary<string, string>>()
            {
                Success = true,
                Data = data,
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

    public static async Task<Ok<Response<List<RelatedVoucher>>>> GetFixedAssetGetFAArisingAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFAArisingQuery>(queryString);
            var relatedVouchers = await services.Queries.GetFAArisingAsync(queryParams.FixedAssetId, 250, queryParams.DisplayOnBook);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<RelatedVoucher>>()
            {
                Success = true,
                Data = relatedVouchers,
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
            var response = new Response<List<RelatedVoucher>>()
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

    public static async Task<Ok<Response<FixedAssetResponse>>> GetFixedAssetDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var fixedAsset = await services.Queries.GetFixedAssetDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<FixedAssetResponse>()
            {
                Success = true,
                Data = fixedAsset,
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
            var response = new Response<FixedAssetResponse>()
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

    public static async Task<Ok<Response<Pagination<FixedAssetDto>>>> PostFixedAssetPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetFixedAssetsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<FixedAssetDto>>()
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
            var response = new Response<Pagination<FixedAssetDto>>()
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

    public static async Task<Results<Ok<Response<FAIncrementCreateResponse>>, BadRequest<string>>> PostFixedAssetSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FixedAssetCreateRequest fixedAsset = JsonSerializer.Deserialize<FixedAssetCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FixedAssetDetailAllocationCreateRequest> fixedAssetDetailAllocations = [];
                List<FixedAssetDetailSourceCreateRequest> fixedAssetDetailSources = [];
                List<FixedAssetDetailCreateRequest> fixedAssetDetails = [];
                List<FixedAssetDetailAccessoryCreateRequest> fixedAssetDetailAccessories = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fixed_asset_detail_allocation":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FixedAssetDetailAllocationCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fixedAssetDetailAllocations.Add(obj);
                                }
                            }

                            break;
                        case "fixed_asset_detail_source":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FixedAssetDetailSourceCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fixedAssetDetailSources.Add(obj);
                                }
                            }

                            break;
                        case "fixed_asset_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FixedAssetDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fixedAssetDetails.Add(obj);
                                }
                            }

                            break;
                        case "fixed_asset_detail_accessory":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FixedAssetDetailAccessoryCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fixedAssetDetailAccessories.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createFixedAssetCommand = new CreateFixedAssetCommand(fixedAssetDetailAllocations, fixedAssetDetailSources, fixedAssetDetails, fixedAssetDetailAccessories, fixedAsset, auditingLog);

                var requestCreateFixedAsset = new IdentifiedCommand<CreateFixedAssetCommand, FAIncrementCreateResponse>(createFixedAssetCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateFixedAsset.GetGenericTypeName(),
                    nameof(requestCreateFixedAsset.Id),
                    requestCreateFixedAsset.Id,
                    requestCreateFixedAsset);

                var result = await services.Mediator.Send(requestCreateFixedAsset);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateFixedAssetCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FAIncrementCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateFixedAssetCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FAIncrementCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FAIncrementCreateResponse>()
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

    public static async Task<Results<Ok<Response<FAIncrementUpdateResponse>>, BadRequest<string>>> PutFixedAssetSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FixedAssetUpdateRequest fixedAsset = JsonSerializer.Deserialize<FixedAssetUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FixedAssetDetailAllocationUpdateRequest> fixedAssetDetailAllocations = [];
                List<FixedAssetDetailSourceUpdateRequest> fixedAssetDetailSources = [];
                List<FixedAssetDetailUpdateRequest> fixedAssetDetails = [];
                List<FixedAssetDetailAccessoryUpdateRequest> fixedAssetDetailAccessories = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fixed_asset_detail_allocation":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FixedAssetDetailAllocationUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fixedAssetDetailAllocations.Add(obj);
                                }
                            }

                            break;
                        case "fixed_asset_detail_source":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FixedAssetDetailSourceUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fixedAssetDetailSources.Add(obj);
                                }
                            }

                            break;
                        case "fixed_asset_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FixedAssetDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fixedAssetDetails.Add(obj);
                                }
                            }

                            break;
                        case "fixed_asset_detail_accessory":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FixedAssetDetailAccessoryUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fixedAssetDetailAccessories.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var updateFixedAssetCommand = new UpdateFixedAssetCommand(fixedAssetDetailAllocations, fixedAssetDetailSources, fixedAssetDetails, fixedAssetDetailAccessories, fixedAsset, auditingLog);

                var requestUpdateFixedAsset = new IdentifiedCommand<UpdateFixedAssetCommand, FAIncrementUpdateResponse>(updateFixedAssetCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateFixedAsset.GetGenericTypeName(),
                    nameof(requestUpdateFixedAsset.Id),
                    requestUpdateFixedAsset.Id,
                    requestUpdateFixedAsset);

                var result = await services.Mediator.Send(requestUpdateFixedAsset);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateFixedAssetCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FAIncrementUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateFixedAssetCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FAIncrementUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FAIncrementUpdateResponse>()
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

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteFixedAssetBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<FixedAssetDto> request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteFixedAssetCommand = new DeleteFixedAssetCommand(request);

                var requestDeleteFixedAsset = new IdentifiedCommand<DeleteFixedAssetCommand, BatchVoucherResponse>(deleteFixedAssetCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteFixedAsset.GetGenericTypeName(),
                    nameof(requestDeleteFixedAsset.Id),
                    requestDeleteFixedAsset.Id,
                    requestDeleteFixedAsset);

                var result = await services.Mediator.Send(requestDeleteFixedAsset);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteFixedAssetCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteFixedAssetCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Tài sản cố định đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
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


    public static async Task<Ok<Response<FADecrementResponse>>> GetFADecrementDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var fADecrement = await services.Queries.GetFADecrementDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<FADecrementResponse>()
            {
                Success = true,
                Data = fADecrement,
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
            var response = new Response<FADecrementResponse>()
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

    public static async Task<Ok<Response<Pagination<FADecrementDto>>>> PostFADecrementPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetFADecrementsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<FADecrementDto>>()
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
            var response = new Response<Pagination<FADecrementDto>>()
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

    public static async Task<Ok<Response<List<FADecrementAvailableFixedAsset>>>> GetFADecrementGetAvailableFixedAssetAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFADecrementAvailableFixedAssetsQuery>(queryString);

            var availableFixedAssets = await services.Queries.GetFADecrementAvailableFixedAssetsAsync(queryParams.PostedDate, queryParams.BranchID, queryParams.IsManagementBook);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FADecrementAvailableFixedAsset>>()
            {
                Success = true,
                Data = availableFixedAssets,
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
            var response = new Response<List<FADecrementAvailableFixedAsset>>()
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

    public static async Task<Ok<Response<List<GetDataOriginalResponse>>>> GetFADecrementSUOriginalFADecrementAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetDataOriginalRequest>(queryString);

            var originals = await services.Queries.GetFADecrementSUOriginalFADecrementAsync(
                queryParams.FromDate,
                queryParams.ToDate,
                queryParams.RefType,
                queryParams.ListRefId
                );

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<GetDataOriginalResponse>>()
            {
                Success = true,
                Data = originals,
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
            var response = new Response<List<GetDataOriginalResponse>>()
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

    public static async Task<Ok<Response<List<SelectSUFromFADecrement>>>> GetFADecrementGetSelectSUFromFADecrementAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetDataOriginalRequest>(queryString);

            var sus = await services.Queries.GetFADecrementGetSelectSUFromFADecrementAsync(
                queryParams.FromDate,
                queryParams.ToDate
                );

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<SelectSUFromFADecrement>>()
            {
                Success = true,
                Data = sus,
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
            var response = new Response<List<SelectSUFromFADecrement>>()
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

    public static async Task<Ok<Response<List<FADecrementCustomFixedAsset>>>> GetFADecrementGetCustomFixedAssetAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFADecrementCustomFixedAssetsQuery>(queryString);

            var customFixedAssets = await services.Queries.GetFADecrementCustomFixedAssetsAsync(queryParams.FixedAssetIDs, queryParams.BranchID, queryParams.PostedDate, queryParams.IsManagementBook);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FADecrementCustomFixedAsset>>()
            {
                Success = true,
                Data = customFixedAssets,
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
            var response = new Response<List<FADecrementCustomFixedAsset>>()
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

    public static async Task<Results<Ok<Response<FADecrementCreateResponse>>, BadRequest<string>>> PostFADecrementSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FADecrementCreateRequest fADecrement = JsonSerializer.Deserialize<FADecrementCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FADecrementDetailCreateRequest> fADecrementDetails = [];
                List<FADecrementDetailPostCreateRequest> fADecrementDetailPosts = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_decrement_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADecrementDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADecrementDetails.Add(obj);
                                }
                            }

                            break;
                        case "fa_decrement_detail_post":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADecrementDetailPostCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADecrementDetailPosts.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createFADecrementCommand = new CreateFADecrementCommand(fADecrementDetails, fADecrementDetailPosts, fADecrement, auditingLog);

                var requestCreateFADecrement = new IdentifiedCommand<CreateFADecrementCommand, FADecrementCreateResponse>(createFADecrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateFADecrement.GetGenericTypeName(),
                    nameof(requestCreateFADecrement.Id),
                    requestCreateFADecrement.Id,
                    requestCreateFADecrement);

                var result = await services.Mediator.Send(requestCreateFADecrement);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateFADecrementCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FADecrementCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateFADecrementCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FADecrementCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FADecrementCreateResponse>()
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

    public static async Task<Results<Ok<Response<FADecrementUpdateResponse>>, BadRequest<string>>> PutFADecrementSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FADecrementUpdateRequest fADecrement = JsonSerializer.Deserialize<FADecrementUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FADecrementDetailUpdateRequest> fADecrementDetails = [];
                List<FADecrementDetailPostUpdateRequest> fADecrementDetailPosts = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_decrement_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADecrementDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADecrementDetails.Add(obj);
                                }
                            }

                            break;
                        case "fa_decrement_detail_post":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADecrementDetailPostUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADecrementDetailPosts.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createFADecrementCommand = new UpdateFADecrementCommand(fADecrementDetails, fADecrementDetailPosts, fADecrement, auditingLog);

                var requestUpdateFADecrement = new IdentifiedCommand<UpdateFADecrementCommand, FADecrementUpdateResponse>(createFADecrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateFADecrement.GetGenericTypeName(),
                    nameof(requestUpdateFADecrement.Id),
                    requestUpdateFADecrement.Id,
                    requestUpdateFADecrement);

                var result = await services.Mediator.Send(requestUpdateFADecrement);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateFADecrementCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FADecrementUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateFADecrementCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FADecrementUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FADecrementUpdateResponse>()
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

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteFADecrementBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<FADecrementDto> request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteFADecrementCommand = new DeleteFADecrementCommand(request);

                var requestDeleteFADecrement = new IdentifiedCommand<DeleteFADecrementCommand, BatchVoucherResponse>(deleteFADecrementCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteFADecrement.GetGenericTypeName(),
                    nameof(requestDeleteFADecrement.Id),
                    requestDeleteFADecrement.Id,
                    requestDeleteFADecrement);

                var result = await services.Mediator.Send(requestDeleteFADecrement);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteFADecrementCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteFADecrementCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
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

    public static async Task<Ok<Response<Pagination<FATransferDto>>>> PostFATransferPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetFATransfersAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<FATransferDto>>()
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
            var response = new Response<Pagination<FATransferDto>>()
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

    public static async Task<Results<Ok<Response<FATransferCreateResponse>>, BadRequest<string>>> PostFATransferSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FATransferCreateRequest fATransfer = JsonSerializer.Deserialize<FATransferCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FATransferDetailCreateRequest> fATransferDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_transfer_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FATransferDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fATransferDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }

                var createFATransferCommand = new CreateFATransferCommand(fATransferDetails, fATransfer, auditingLog);

                var requestCreateFATransfer = new IdentifiedCommand<CreateFATransferCommand, FATransferCreateResponse>(createFATransferCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateFATransfer.GetGenericTypeName(),
                    nameof(requestCreateFATransfer.Id),
                    requestCreateFATransfer.Id,
                    requestCreateFATransfer);

                var result = await services.Mediator.Send(requestCreateFATransfer);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateFATransferCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FATransferCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateFATransferCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FATransferCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FATransferCreateResponse>()
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

    public static async Task<Results<Ok<Response<FATransferUpdateResponse>>, BadRequest<string>>> PutFATransferSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FATransferUpdateRequest fATransfer = JsonSerializer.Deserialize<FATransferUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FATransferDetailUpdateRequest> fATransferDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_transfer_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FATransferDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fATransferDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }

                var createFATransferCommand = new UpdateFATransferCommand(fATransferDetails, fATransfer, auditingLog);

                var requestUpdateFATransfer = new IdentifiedCommand<UpdateFATransferCommand, FATransferUpdateResponse>(createFATransferCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateFATransfer.GetGenericTypeName(),
                    nameof(requestUpdateFATransfer.Id),
                    requestUpdateFATransfer.Id,
                    requestUpdateFATransfer);

                var result = await services.Mediator.Send(requestUpdateFATransfer);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateFATransferCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FATransferUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateFATransferCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FATransferUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FATransferUpdateResponse>()
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

    public static async Task<Ok<Response<List<FATransferAvailableFixedAsset>>>> GetFATransferGetAvailableFixedAssetAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFATransferAvailableFixedAssetsQuery>(queryString);

            var availableFixedAssets = await services.Queries.GetFATransferAvailableFixedAssetsAsync(queryParams.PostedDate, queryParams.BranchID, queryParams.IsManagementBook);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FATransferAvailableFixedAsset>>()
            {
                Success = true,
                Data = availableFixedAssets,
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
            var response = new Response<List<FATransferAvailableFixedAsset>>()
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

    public static async Task<Ok<Response<FATransferResponse>>> GetFATransferDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var fATransfer = await services.Queries.GetFATransferDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<FATransferResponse>()
            {
                Success = true,
                Data = fATransfer,
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
            var response = new Response<FATransferResponse>()
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

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteFATransferBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<FATransferDto> request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteFATransferCommand = new DeleteFATransferCommand(request);

                var requestDeleteFATransfer = new IdentifiedCommand<DeleteFATransferCommand, BatchVoucherResponse>(deleteFATransferCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteFATransfer.GetGenericTypeName(),
                    nameof(requestDeleteFATransfer.Id),
                    requestDeleteFATransfer.Id,
                    requestDeleteFATransfer);

                var result = await services.Mediator.Send(requestDeleteFATransfer);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteFATransferCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteFATransferCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
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

    public static async Task<Ok<Response<Pagination<FADepreciationDto>>>> PostFADepreciationPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetFADepreciationAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<FADepreciationDto>>()
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
            var response = new Response<Pagination<FADepreciationDto>>()
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

    public static async Task<Ok<Response<List<FADepreciationDto>>>> GetFADepreciationCheckExistsFADepreciationAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<CheckExistsFADepreciationQuery>(queryString);

            var fADepreciations = await services.Queries.CheckExistsFADepreciationAsync(queryParams.Month, queryParams.Year);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FADepreciationDto>>()
            {
                Success = true,
                Data = fADepreciations,
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
            var response = new Response<List<FADepreciationDto>>()
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

    public static async Task<Ok<Response<FADepreciationDetailResponse>>> GetFADepreciationFADepreciationDetailAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFADepreciationDetailQuery>(queryString);

            var fADepreciationDetail = await services.Queries.GetFADepreciationDetailAsync(queryParams.PostedDate);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<FADepreciationDetailResponse>()
            {
                Success = true,
                Data = fADepreciationDetail,
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
            var response = new Response<FADepreciationDetailResponse>()
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

    public static async Task<Ok<Response<FADepreciationResponse>>> GetFADepreciationDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var fADepreciation = await services.Queries.GetFADepreciationDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<FADepreciationResponse>()
            {
                Success = true,
                Data = fADepreciation,
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
            var response = new Response<FADepreciationResponse>()
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

    public static async Task<Results<Ok<Response<FADepreciationCreateResponse>>, BadRequest<string>>> PostFADepreciationSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FADepreciationCreateRequest fADepreciation = JsonSerializer.Deserialize<FADepreciationCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FADepreciationDetailCreateRequest> fADepreciationDetails = [];
                List<FADepreciationDetailAllocationCreateRequest> fADepreciationDetailAllocations = [];
                List<FADepreciationDetailPostCreateRequest> fADepreciationDetailPosts = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_depreciation_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADepreciationDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADepreciationDetails.Add(obj);
                                }
                            }

                            break;
                        case "fa_depreciation_detail_allocation":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADepreciationDetailAllocationCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADepreciationDetailAllocations.Add(obj);
                                }
                            }

                            break;
                        case "fa_depreciation_detail_post":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADepreciationDetailPostCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADepreciationDetailPosts.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createFADepreciationCommand = new CreateFADepreciationCommand(fADepreciationDetails, fADepreciationDetailAllocations, fADepreciationDetailPosts, fADepreciation, auditingLog);

                var requestCreateFADepreciation = new IdentifiedCommand<CreateFADepreciationCommand, FADepreciationCreateResponse>(createFADepreciationCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateFADepreciation.GetGenericTypeName(),
                    nameof(requestCreateFADepreciation.Id),
                    requestCreateFADepreciation.Id,
                    requestCreateFADepreciation);

                var result = await services.Mediator.Send(requestCreateFADepreciation);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateFADepreciationCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FADepreciationCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateFADepreciationCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FADepreciationCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FADepreciationCreateResponse>()
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

    public static async Task<Results<Ok<Response<FADepreciationUpdateResponse>>, BadRequest<string>>> PutFADepreciationSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FADepreciationUpdateRequest fADepreciation = JsonSerializer.Deserialize<FADepreciationUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FADepreciationDetailUpdateRequest> fADepreciationDetails = [];
                List<FADepreciationDetailAllocationUpdateRequest> fADepreciationDetailAllocations = [];
                List<FADepreciationDetailPostUpdateRequest> fADepreciationDetailPosts = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_depreciation_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADepreciationDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADepreciationDetails.Add(obj);
                                }
                            }

                            break;
                        case "fa_depreciation_detail_allocation":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADepreciationDetailAllocationUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADepreciationDetailAllocations.Add(obj);
                                }
                            }

                            break;
                        case "fa_depreciation_detail_post":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FADepreciationDetailPostUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fADepreciationDetailPosts.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var updateFADepreciationCommand = new UpdateFADepreciationCommand(fADepreciationDetails, fADepreciationDetailAllocations, fADepreciationDetailPosts, fADepreciation, auditingLog);

                var requestUpdateFADepreciation = new IdentifiedCommand<UpdateFADepreciationCommand, FADepreciationUpdateResponse>(updateFADepreciationCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateFADepreciation.GetGenericTypeName(),
                    nameof(requestUpdateFADepreciation.Id),
                    requestUpdateFADepreciation.Id,
                    requestUpdateFADepreciation);

                var result = await services.Mediator.Send(requestUpdateFADepreciation);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateFADepreciationCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FADepreciationUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateFADepreciationCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FADepreciationUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FADepreciationUpdateResponse>()
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

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteFADepreciationBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<FADepreciationDto> request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteFADepreciationCommand = new DeleteFADepreciationCommand(request);

                var requestDeleteFADepreciation = new IdentifiedCommand<DeleteFADepreciationCommand, BatchVoucherResponse>(deleteFADepreciationCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteFADepreciation.GetGenericTypeName(),
                    nameof(requestDeleteFADepreciation.Id),
                    requestDeleteFADepreciation.Id,
                    requestDeleteFADepreciation);

                var result = await services.Mediator.Send(requestDeleteFADepreciation);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteFADepreciationCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteFADepreciationCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
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

    public static async Task<Ok<Response<Pagination<FAChangeFinancialLeasingToOwnerDto>>>> PostFAChangeFinancialLeasingToOwnerPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetFAChangeFinancialLeasingToOwnersAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<FAChangeFinancialLeasingToOwnerDto>>()
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
            var response = new Response<Pagination<FAChangeFinancialLeasingToOwnerDto>>()
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

    public static async Task<Ok<Response<List<FAChangeFinancialLeasingToOwnerAvailableFixedAsset>>>> GetFAChangeFinancialLeasingToOwnerAvailableFixedAssetAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFAChangeFinancialLeasingToOwnerAvailableFixedAssetsQuery>(queryString);

            var availableFixedAssets = await services.Queries.GetFAChangeFinancialLeasingToOwnerAvailableFixedAssetsAsync(queryParams.ToDate, queryParams.FixedAssetId);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FAChangeFinancialLeasingToOwnerAvailableFixedAsset>>()
            {
                Success = true,
                Data = availableFixedAssets,
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
            var response = new Response<List<FAChangeFinancialLeasingToOwnerAvailableFixedAsset>>()
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

    public static async Task<Results<Ok<Response<FAChangeFinancialLeasingToOwnerCreateResponse>>, BadRequest<string>>> PostFAChangeFinancialLeasingToOwnerSaveFullAsync(
    [FromHeader(Name = "x-request-id")] Guid requestId,
    [FromBody] SaveFullRequest request,
    [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FAChangeFinancialLeasingToOwnerCreateRequest fAChangeFinancialLeasingToOwner = JsonSerializer.Deserialize<FAChangeFinancialLeasingToOwnerCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FAChangeFinancialLeasingToOwnerDetailCreateRequest> fAChangeFinancialLeasingToOwnerDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_change_financial_leasing_to_owner_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FAChangeFinancialLeasingToOwnerDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fAChangeFinancialLeasingToOwnerDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }

                var createFAChangeFinancialLeasingToOwnerCommand = new CreateFAChangeFinancialLeasingToOwnerCommand(fAChangeFinancialLeasingToOwnerDetails, fAChangeFinancialLeasingToOwner, auditingLog);

                var requestCreateFAChangeFinancialLeasingToOwner = new IdentifiedCommand<CreateFAChangeFinancialLeasingToOwnerCommand, FAChangeFinancialLeasingToOwnerCreateResponse>(createFAChangeFinancialLeasingToOwnerCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateFAChangeFinancialLeasingToOwner.GetGenericTypeName(),
                    nameof(requestCreateFAChangeFinancialLeasingToOwner.Id),
                    requestCreateFAChangeFinancialLeasingToOwner.Id,
                    requestCreateFAChangeFinancialLeasingToOwner);

                var result = await services.Mediator.Send(requestCreateFAChangeFinancialLeasingToOwner);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateFAChangeFinancialLeasingToOwnerCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FAChangeFinancialLeasingToOwnerCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateFAChangeFinancialLeasingToOwnerCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FAChangeFinancialLeasingToOwnerCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FAChangeFinancialLeasingToOwnerCreateResponse>()
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

    public static async Task<Results<Ok<Response<FAChangeFinancialLeasingToOwnerUpdateResponse>>, BadRequest<string>>> PutFAChangeFinancialLeasingToOwnerSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FAChangeFinancialLeasingToOwnerUpdateRequest fAChangeFinancialLeasingToOwner = JsonSerializer.Deserialize<FAChangeFinancialLeasingToOwnerUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FAChangeFinancialLeasingToOwnerDetailUpdateRequest> fAChangeFinancialLeasingToOwnerDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_change_financial_leasing_to_owner_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FAChangeFinancialLeasingToOwnerDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fAChangeFinancialLeasingToOwnerDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }

                var createFAChangeFinancialLeasingToOwnerCommand = new UpdateFAChangeFinancialLeasingToOwnerCommand(fAChangeFinancialLeasingToOwnerDetails, fAChangeFinancialLeasingToOwner, auditingLog);

                var requestUpdateFAChangeFinancialLeasingToOwner = new IdentifiedCommand<UpdateFAChangeFinancialLeasingToOwnerCommand, FAChangeFinancialLeasingToOwnerUpdateResponse>(createFAChangeFinancialLeasingToOwnerCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateFAChangeFinancialLeasingToOwner.GetGenericTypeName(),
                    nameof(requestUpdateFAChangeFinancialLeasingToOwner.Id),
                    requestUpdateFAChangeFinancialLeasingToOwner.Id,
                    requestUpdateFAChangeFinancialLeasingToOwner);

                var result = await services.Mediator.Send(requestUpdateFAChangeFinancialLeasingToOwner);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateFAChangeFinancialLeasingToOwnerCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FAChangeFinancialLeasingToOwnerUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateFAChangeFinancialLeasingToOwnerCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FAChangeFinancialLeasingToOwnerUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FAChangeFinancialLeasingToOwnerUpdateResponse>()
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


    public static async Task<Ok<Response<FAChangeFinancialLeasingToOwnerResponse>>> GetFAChangeFinancialLeasingToOwnerDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var fAChangeFinancialLeasingToOwner = await services.Queries.GetFAChangeFinancialLeasingToOwnerDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<FAChangeFinancialLeasingToOwnerResponse>()
            {
                Success = true,
                Data = fAChangeFinancialLeasingToOwner,
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
            var response = new Response<FAChangeFinancialLeasingToOwnerResponse>()
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

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteFAChangeFinancialLeasingToOwnerBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<FAChangeFinancialLeasingToOwnerDto> request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteFAChangeFinancialLeasingToOwnerCommand = new DeleteFAChangeFinancialLeasingToOwnerCommand(request);

                var requestDeleteFAChangeFinancialLeasingToOwner = new IdentifiedCommand<DeleteFAChangeFinancialLeasingToOwnerCommand, BatchVoucherResponse>(deleteFAChangeFinancialLeasingToOwnerCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteFAChangeFinancialLeasingToOwner.GetGenericTypeName(),
                    nameof(requestDeleteFAChangeFinancialLeasingToOwner.Id),
                    requestDeleteFAChangeFinancialLeasingToOwner.Id,
                    requestDeleteFAChangeFinancialLeasingToOwner);

                var result = await services.Mediator.Send(requestDeleteFAChangeFinancialLeasingToOwner);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteFAChangeFinancialLeasingToOwnerCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteFAChangeFinancialLeasingToOwnerCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
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

    public static async Task<Ok<Response<FAAdjustmentResponse>>> GetFAAdjustmentDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var fAAdjustment = await services.Queries.GetFAAdjustmentDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<FAAdjustmentResponse>()
            {
                Success = true,
                Data = fAAdjustment,
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
            var response = new Response<FAAdjustmentResponse>()
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

    public static async Task<Ok<Response<Pagination<FAAdjustmentDto>>>> PostFAAdjustmentPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetFAAdjustmentsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<FAAdjustmentDto>>()
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
            var response = new Response<Pagination<FAAdjustmentDto>>()
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

    public static async Task<Ok<Response<List<FAAdjustmentAvailableFixedAsset>>>> GetFAAdjustmentGetAvailableFixedAssetAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFAAdjustmentAvailableFixedAssetsQuery>(queryString);

            var availableFixedAssets = await services.Queries.GetFAAdjustmentAvailableFixedAssetsAsync(queryParams.PostedDate, queryParams.BranchID, queryParams.IsManagementBook);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FAAdjustmentAvailableFixedAsset>>()
            {
                Success = true,
                Data = availableFixedAssets,
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
            var response = new Response<List<FAAdjustmentAvailableFixedAsset>>()
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

    public static async Task<Ok<Response<List<FAAdjustmentCustomFixedAsset>>>> GetFAAdjustmentGetCustomFixedAssetAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFAAdjustmentCustomFixedAssetsQuery>(queryString);

            var customFixedAssets = await services.Queries.GetFAAdjustmentCustomFixedAssetsAsync(queryParams.FixedAssetIDs, queryParams.BranchID, queryParams.PostedDate, queryParams.IsManagementBook);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FAAdjustmentCustomFixedAsset>>()
            {
                Success = true,
                Data = customFixedAssets,
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
            var response = new Response<List<FAAdjustmentCustomFixedAsset>>()
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

    public static async Task<Results<Ok<Response<FAAdjustmentCreateResponse>>, BadRequest<string>>> PostFAAdjustmentSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FAAdjustmentCreateRequest fAAdjustment = JsonSerializer.Deserialize<FAAdjustmentCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FAAdjustmentDetailCreateRequest> fAAdjustmentDetails = [];
                List<FAAdjustmentDetailPostCreateRequest> fAAdjustmentDetailPosts = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_adjustment_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FAAdjustmentDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fAAdjustmentDetails.Add(obj);
                                }
                            }

                            break;
                        case "fa_adjustment_detail_post":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FAAdjustmentDetailPostCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fAAdjustmentDetailPosts.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createFAAdjustmentCommand = new CreateFAAdjustmentCommand(fAAdjustmentDetails, fAAdjustmentDetailPosts, fAAdjustment, auditingLog);

                var requestCreateFAAdjustment = new IdentifiedCommand<CreateFAAdjustmentCommand, FAAdjustmentCreateResponse>(createFAAdjustmentCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateFAAdjustment.GetGenericTypeName(),
                    nameof(requestCreateFAAdjustment.Id),
                    requestCreateFAAdjustment.Id,
                    requestCreateFAAdjustment);

                var result = await services.Mediator.Send(requestCreateFAAdjustment);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateFAAdjustmentCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FAAdjustmentCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateFAAdjustmentCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FAAdjustmentCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FAAdjustmentCreateResponse>()
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


    public static async Task<Results<Ok<Response<FAAdjustmentUpdateResponse>>, BadRequest<string>>> PutFAAdjustmentSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FAAdjustmentUpdateRequest fAAdjustment = JsonSerializer.Deserialize<FAAdjustmentUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FAAdjustmentDetailUpdateRequest> fAAdjustmentDetails = [];
                List<FAAdjustmentDetailPostUpdateRequest> fAAdjustmentDetailPosts = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_adjustment_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FAAdjustmentDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fAAdjustmentDetails.Add(obj);
                                }
                            }

                            break;
                        case "fa_adjustment_detail_post":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FAAdjustmentDetailPostUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fAAdjustmentDetailPosts.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createFAAdjustmentCommand = new UpdateFAAdjustmentCommand(fAAdjustmentDetails, fAAdjustmentDetailPosts, fAAdjustment, auditingLog);

                var requestUpdateFAAdjustment = new IdentifiedCommand<UpdateFAAdjustmentCommand, FAAdjustmentUpdateResponse>(createFAAdjustmentCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateFAAdjustment.GetGenericTypeName(),
                    nameof(requestUpdateFAAdjustment.Id),
                    requestUpdateFAAdjustment.Id,
                    requestUpdateFAAdjustment);

                var result = await services.Mediator.Send(requestUpdateFAAdjustment);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateFAAdjustmentCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FAAdjustmentUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateFAAdjustmentCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FAAdjustmentUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FAAdjustmentUpdateResponse>()
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

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteFAAdjustmentBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<FAAdjustmentDto> request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteFAAdjustmentCommand = new DeleteFAAdjustmentCommand(request);

                var requestDeleteFAAdjustment = new IdentifiedCommand<DeleteFAAdjustmentCommand, BatchVoucherResponse>(deleteFAAdjustmentCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteFAAdjustment.GetGenericTypeName(),
                    nameof(requestDeleteFAAdjustment.Id),
                    requestDeleteFAAdjustment.Id,
                    requestDeleteFAAdjustment);

                var result = await services.Mediator.Send(requestDeleteFAAdjustment);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteFAAdjustmentCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteFAAdjustmentCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
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

    public static async Task<Ok<Response<FAAuditResponse>>> GetFAAuditDetailFullAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<DetailFullRequest>(queryString);

            var fAAudit = await services.Queries.GetFAAuditDetailFullAsync(queryParams.Key, queryParams.RefType);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<FAAuditResponse>()
            {
                Success = true,
                Data = fAAudit,
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
            var response = new Response<FAAuditResponse>()
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

    public static async Task<Ok<Response<Pagination<FAAuditDto>>>> PostFAAuditPagingFilterAsync(
        [FromBody] PaginationRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            var pagination = await services.Queries.GetFAAuditsAsync(
                request.Sort,
                request.Filter,
                request.PageIndex,
                request.PageSize,
                request.UseSp,
                request.View,
                request.SummaryColumns,
                request.LoadMode);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<Pagination<FAAuditDto>>()
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
            var response = new Response<Pagination<FAAuditDto>>()
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

    public static async Task<Ok<Response<List<FAAuditAvailableFixedAsset>>>> GetFAAuditGetAvailableFixedAssetAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string queryString = Encoding.UTF8.GetString(bytes);
            var queryParams = JsonSerializer.Deserialize<GetFAAuditAvailableFixedAssetsQuery>(queryString);

            var availableFixedAssets = await services.Queries.GetFAAuditAvailableFixedAssetsAsync(queryParams.AuditDate);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FAAuditAvailableFixedAsset>>()
            {
                Success = true,
                Data = availableFixedAssets,
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
            var response = new Response<List<FAAuditAvailableFixedAsset>>()
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

    public static async Task<Ok<Response<List<FAAuditCheckExistFADecrementToFAAudit>>>> GetFAAuditCheckExistFADecrementToFAAuditAsync(
        [FromQuery(Name = "req")] string req,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            byte[] bytes = Convert.FromBase64String(req);
            string fixedAssetIds = Encoding.UTF8.GetString(bytes);

            var fAAuditCheckExistFADecrementToFAAudits = await services.Queries.CheckExistFADecrementToFAAuditAsync(fixedAssetIds);

            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;

            var response = new Response<List<FAAuditCheckExistFADecrementToFAAudit>>()
            {
                Success = true,
                Data = fAAuditCheckExistFADecrementToFAAudits,
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
            var response = new Response<List<FAAuditCheckExistFADecrementToFAAudit>>()
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

    public static async Task<Results<Ok<Response<FAAuditCreateResponse>>, BadRequest<string>>> PostFAAuditSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FAAuditCreateRequest fAAudit = JsonSerializer.Deserialize<FAAuditCreateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FAAuditDetailCreateRequest> fAAuditDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_audit_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FAAuditDetailCreateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fAAuditDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createFAAuditCommand = new CreateFAAuditCommand(fAAuditDetails, fAAudit, auditingLog);

                var requestCreateFAAudit = new IdentifiedCommand<CreateFAAuditCommand, FAAuditCreateResponse>(createFAAuditCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateFAAudit.GetGenericTypeName(),
                    nameof(requestCreateFAAudit.Id),
                    requestCreateFAAudit.Id,
                    requestCreateFAAudit);

                var result = await services.Mediator.Send(requestCreateFAAudit);

                if (result != null)
                {
                    services.Logger.LogInformation("CreateFAAuditCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FAAuditCreateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("CreateFAAuditCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FAAuditCreateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FAAuditCreateResponse>()
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


    public static async Task<Results<Ok<Response<FAAuditUpdateResponse>>, BadRequest<string>>> PutFAAuditSaveFullAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] SaveFullRequest request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId}",
                request.GetGenericTypeName(),
                nameof(request.Key),
                request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                FAAuditUpdateRequest fAAudit = JsonSerializer.Deserialize<FAAuditUpdateRequest>(request.Object.GetRawText());
                AuditingLog auditingLog = request.AuditingLog;
                List<FAAuditDetailUpdateRequest> fAAuditDetails = [];

                foreach (var detail in request.Details)
                {
                    switch (detail.Type)
                    {
                        case "fa_audit_detail":
                            foreach (var @object in detail.Objects)
                            {
                                var obj = JsonSerializer.Deserialize<FAAuditDetailUpdateRequest>(@object.GetRawText());
                                if (obj != null)
                                {
                                    fAAuditDetails.Add(obj);
                                }
                            }

                            break;
                    }
                }


                var createFAAuditCommand = new UpdateFAAuditCommand(fAAuditDetails, fAAudit, auditingLog);

                var requestUpdateFAAudit = new IdentifiedCommand<UpdateFAAuditCommand, FAAuditUpdateResponse>(createFAAuditCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestUpdateFAAudit.GetGenericTypeName(),
                    nameof(requestUpdateFAAudit.Id),
                    requestUpdateFAAudit.Id,
                    requestUpdateFAAudit);

                var result = await services.Mediator.Send(requestUpdateFAAudit);

                if (result != null)
                {
                    services.Logger.LogInformation("UpdateFAAuditCommand succeeded - RequestId: {RequestId}", requestId);
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<FAAuditUpdateResponse>()
                    {
                        Success = true,
                        Data = result,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1)
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("UpdateFAAuditCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<FAAuditUpdateResponse>()
                    {
                        Success = false,
                        Code = 99,
                        SystemMessage = $"Có một số lỗi xảy ra, vui lòng liên hệ với SMART để được trợ giúp. Mã lỗi: {exceptionID}",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<FAAuditUpdateResponse>()
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

    public static async Task<Results<Ok<Response<BatchVoucherResponse>>, BadRequest<string>>> DeleteFAAuditBatchVoucherAsync(
        [FromHeader(Name = "x-request-id")] Guid requestId,
        [FromBody] List<FAAuditDto> request,
        [AsParameters] FixedAssetServices services)
    {
        var currentTime = DateTime.Now;

        try
        {
            //services.Logger.LogInformation(
            //    "Sending command: {CommandName} - {IdProperty}: {CommandId}",
            //    request.GetGenericTypeName(),
            //    nameof(request.Key),
            //    request.Key);

            if (requestId == Guid.Empty)
            {
                services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
                return TypedResults.BadRequest("RequestId is missing.");
            }

            using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {

                var deleteFAAuditCommand = new DeleteFAAuditCommand(request);

                var requestDeleteFAAudit = new IdentifiedCommand<DeleteFAAuditCommand, BatchVoucherResponse>(deleteFAAuditCommand, requestId);

                services.Logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestDeleteFAAudit.GetGenericTypeName(),
                    nameof(requestDeleteFAAudit.Id),
                    requestDeleteFAAudit.Id,
                    requestDeleteFAAudit);

                var result = await services.Mediator.Send(requestDeleteFAAudit);

                if (result.TotalError == 0)
                {
                    services.Logger.LogInformation("DeleteFAAuditCommand succeeded - RequestId: {RequestId}", requestId);

                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;

                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = true,
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
                else
                {
                    services.Logger.LogWarning("DeleteFAAuditCommand failed - RequestId: {RequestId}", requestId);

                    var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
                    var serverTime = DateTime.Now;
                    var requestTime = serverTime - currentTime;
                    var response = new Response<BatchVoucherResponse>()
                    {
                        Success = false,
                        Code = 99,
                        UserMessage = "Chứng từ đã có phát sinh.",
                        SystemMessage = "ExistedConstrain",
                        ServerTime = serverTime,
                        RequestTime = requestTime.ToString(@"hh\:mm\:ss\.fffffff"),
                        TotalTime = Math.Round(requestTime.TotalSeconds, 1),
                        ExceptionID = exceptionID,
                        Data = result
                    };

                    return TypedResults.Ok(response);
                }
            }
        }
        catch (Exception ex)
        {
            var exceptionID = Guid.NewGuid().ToString("N").ToUpper();
            var serverTime = DateTime.Now;
            var requestTime = serverTime - currentTime;
            var response = new Response<BatchVoucherResponse>()
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